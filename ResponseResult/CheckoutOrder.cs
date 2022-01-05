using System;
using System.Collections.Generic;
using System.Linq;

namespace ResponseResult
{
    public class CheckoutOrder : IRepositoryOrder
    {
        private static  List<OrderResponse> _orderResponses;

        static CheckoutOrder()
        {
            _orderResponses = new List<OrderResponse>();
        }
        public List<OrderResponse> GetOrders()
        {
            return _orderResponses;
        }

        public void SetOrders(OrderResponse? order)
        {
         
            if (order is not null)
            {
                _orderResponses.Add(order);
            }
           
           
        }

       
    }
}