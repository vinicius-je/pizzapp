using Microsoft.EntityFrameworkCore;
using PizzApp.Application.Features.OrderFeatures.Create;
using PizzApp.Domain.Entities;
using PizzApp.Domain.Interfaces;
using PizzApp.Infrastructure.Context;

namespace PizzApp.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Customer?> GetCustomerById(Guid id, CancellationToken cancellationToken)
        {
            return await this.GetById(id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
