namespace PizzApp.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }

        public OrderItem()
        {
        }

        public OrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
