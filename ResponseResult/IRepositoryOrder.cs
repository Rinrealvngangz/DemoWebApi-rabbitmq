using System.Collections.Generic;

namespace ResponseResult
{
    public interface IRepositoryOrder
    {
        List<OrderResponse> GetOrders();
        void SetOrders(OrderResponse? order);
    }
}