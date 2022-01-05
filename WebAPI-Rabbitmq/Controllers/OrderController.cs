using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using WebAPI_Rabbitmq.DummyData;

namespace WebAPI_Rabbitmq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPublisher<Order> _publisher;
        private readonly DataOrder _data;
        private readonly IConnectionProvider _connection;
        private readonly IModel? conn;
        public OrderController(IPublisher<Order> publisher,
                               IConnectionProvider connection )
        {
            _connection = connection;
            _publisher = publisher;
            _data = new DataOrder();
            conn = _connection.GetConnection().CreateModel();
        }

        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return _data.GetData();
        }

        [HttpGet("id")]
        public IActionResult  GetById(int id)
        {
            var item = GetItem(id);
          return Ok(item);
        }
        [HttpPost]
        public IActionResult Order([FromBody] OrderRequest orderRequest)
        {
           var item = GetItem(orderRequest.id);
           var order = new Order
           {
               item = item,
               cumtomerName = orderRequest.cumtomerName,
               quantity = orderRequest.quantity
           };
           _publisher.SetData(order);
           _publisher.Publish(conn,"order-exchange","order.init",null);
           return Ok(order);
        }

        private Item GetItem(int id)
        {
            var item = _data.GetData().Where(x => x.id == id).FirstOrDefault();
            return item;
        }
    }
}