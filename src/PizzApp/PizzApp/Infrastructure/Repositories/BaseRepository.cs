using Microsoft.EntityFrameworkCore;
using PizzApp.Domain.Entities;
using PizzApp.Domain.Interfaces;
using PizzApp.Infrastructure.Context;

namespace PizzApp.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbSet<T> DbSet;

        public BaseRepository(AppDbContext context)
        {
            DbSet = context.Set<T>();
        }

        public async Task Create(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            await Task.Run(() => DbSet.Remove(entity));
        }

        public async Task Delete(T entity)
        {
            await Task.Run(() => DbSet.Remove(entity));
        }

        public IQueryable<T> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public IQueryable<T> GetById(Guid id)
        {
            return DbSet.Where(x => x.Id == id).AsQueryable();
        }
    }
}
