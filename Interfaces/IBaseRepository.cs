namespace ApiCatalogoClientes.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> FindAll();
        Task<T?> FindByIdAsync(int id);
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
