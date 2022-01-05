using System;
using System.Collections.Generic;
using RabbitMQ.Client;

namespace Core
{
    public interface IPublisher<T> : IDisposable  where T : class 
    {
        void Publish(IModel channel, string exchange,string routingKey, IDictionary<string, object>? argument);
        void SetData(T list);
    }
}