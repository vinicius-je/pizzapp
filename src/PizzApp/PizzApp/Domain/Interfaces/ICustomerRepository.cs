using PizzApp.Application.Features.OrderFeatures.Create;
using PizzApp.Domain.Entities;

namespace PizzApp.Domain.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer?> GetCustomerById(Guid id, CancellationToken cancellationToken);
    }
}
