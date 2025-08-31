using PizzApp.Domain.Entities;
using PizzApp.Domain.Interfaces;
using PizzApp.Infrastructure.Context;

namespace PizzApp.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
