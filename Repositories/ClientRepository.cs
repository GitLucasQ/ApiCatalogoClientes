using ApiCatalogoClientes.Domain;
using ApiCatalogoClientes.Domain.Entities;
using ApiCatalogoClientes.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoClientes.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<bool> ClientExistsByDocumentNumber(string documentNumber)
        {
            return await RepositoryContext
                            .Clients
                            .AnyAsync(c => c.DocumentNumber == documentNumber && c.IsActive);
        }

        public async Task<IEnumerable<Client>> GetAllActives()
        {
            return await RepositoryContext
                            .Clients
                            .Where(c => c.IsActive)
                            .Include(c => c.TypeDocument)
                            .ToListAsync();
        }

        public async Task<Client?> GetByDocumentNumber(string documentNumber)
        {
            return await RepositoryContext
                            .Clients
                            .Include(c => c.TypeDocument)
                            .FirstOrDefaultAsync(c => c.DocumentNumber.Equals(documentNumber));
        }

        public async Task SoftDeleteById(int id)
        {
            await RepositoryContext
                    .Clients
                    .Where(c => c.Id == id)
                    .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsActive, false));
        }
    }
}
