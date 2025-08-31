using PizzApp.Domain.Entities;

namespace PizzApp.Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product?> GetProductById(Guid id, CancellationToken cancellationToken);
    }
}
