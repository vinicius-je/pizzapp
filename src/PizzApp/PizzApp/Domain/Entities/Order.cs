namespace PizzApp.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public List<OrderItem> Items {  get; set; } = new List<OrderItem>();
        public decimal TotalPrice => Items.Sum(x => x.TotalPrice);

        public Order()
        {
        }

        public Order(Guid customerId, List<OrderItem> items)
        {
            CustomerId = customerId;
            Items = items;
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }
    }
}
