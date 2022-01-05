using System.Collections.Generic;

namespace WebAPI_Rabbitmq.DummyData
{
    public class DataOrder
    {
        private readonly List<Item> _list;
      
        public DataOrder()
        {
            _list = new List<Item>();
            InitData();
        }

        public List<Item> GetData()
        {
            return _list;
        }

        private void InitData()
        {
            for (int i = 0; i < 10; i++)
            {
                Item order = new Item
                {
                    id = i,
                    name = $"order{i}"
                }; 
                _list.Add(order);
            }

        }
    }
}