namespace PizzApp.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<Order> Orders { get; set; } = new List<Order>();

        public Customer()
        {
        }

        public Customer(string name, string email, string phoneNumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
