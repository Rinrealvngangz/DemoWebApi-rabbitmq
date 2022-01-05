using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace ResponseResult
{
    public class ResponseData : IHostedService
    {

        private readonly IRepositoryOrder _repositoryOrder;
        private readonly IConnectionProvider _connectionProvider;
        private readonly ISubscribe<OrderResponse> _subscribe;
        private readonly IModel _model;
        public ResponseData(IConnectionProvider connectionProvider,
                            ISubscribe<OrderResponse> subscribe,
                            IRepositoryOrder repositoryOrder)
        {
            _repositoryOrder = repositoryOrder;
            _subscribe = subscribe;
            _connectionProvider = connectionProvider;
         var conn =   _connectionProvider.GetConnection();
           _model =  conn.CreateModel();
           
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("started...");
            _subscribe.Subscribe(_model,"order-exchange","order-queue","order.*",Callback);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
          return Task.CompletedTask;
        }

        public void Callback(OrderResponse response)
        {
            _repositoryOrder.SetOrders(response);
        }


    }
}