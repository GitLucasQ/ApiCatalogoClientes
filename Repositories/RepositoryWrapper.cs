using ApiCatalogoClientes.Domain;
using ApiCatalogoClientes.Interfaces;

namespace ApiCatalogoClientes.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _context;
        private IClientRepository _clientRepository;

        public RepositoryWrapper(RepositoryContext context)
        {
            _context = context;
        }

        public IClientRepository Clients
        {
            get
            {
                if (_clientRepository is null)
                {
                    _clientRepository = new ClientRepository(_context);
                }

                return _clientRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
