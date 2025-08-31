using PizzApp.Domain.Interfaces;
using PizzApp.Infrastructure.Context;

namespace PizzApp.Infrastructure.EntitiesConfiguration
{
    public class UnitOfWork : IUnitOfWork
    {
        protected AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task Commit(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
