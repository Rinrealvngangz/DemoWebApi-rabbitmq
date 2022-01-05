using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ResponseResult.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private readonly IRepositoryOrder _repositoryOrder;
        public CheckOutController(IRepositoryOrder repositoryOrder)
        {
            _repositoryOrder = repositoryOrder;
        }
        [HttpGet]
        public IActionResult GetOrders()
        {
           var orders =  _repositoryOrder.GetOrders();
           return Ok(orders);
        }
    }
}