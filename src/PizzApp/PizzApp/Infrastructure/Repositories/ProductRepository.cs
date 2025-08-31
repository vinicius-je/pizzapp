using Microsoft.EntityFrameworkCore;
using PizzApp.Application.Features.OrderFeatures.Create;
using PizzApp.Domain.Entities;
using PizzApp.Domain.Interfaces;
using PizzApp.Infrastructure.Context;

namespace PizzApp.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Product?> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            return await this.GetById(id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
