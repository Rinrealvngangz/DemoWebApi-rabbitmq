namespace WebAPI_Rabbitmq
{
    public class Order
    {
        public Item? item { get; set; }
        public string cumtomerName {get; set; }
        public int quantity { get; set; }
    }
}