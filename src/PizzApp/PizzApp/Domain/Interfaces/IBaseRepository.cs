namespace PizzApp.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        IQueryable<T> GetById(Guid id);
        IQueryable<T> GetAll();
    }
}
