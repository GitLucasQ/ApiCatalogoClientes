using ApiCatalogoClientes.Domain;
using ApiCatalogoClientes.Domain.Entities;
using ApiCatalogoClientes.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoClientes.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected RepositoryContext RepositoryContext;

        public BaseRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public async Task Create(T entity)
        {
            await RepositoryContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>().AsNoTracking();
        }

        public async Task<T?> FindByIdAsync(int id)
        {
            return await RepositoryContext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }
    }
}
