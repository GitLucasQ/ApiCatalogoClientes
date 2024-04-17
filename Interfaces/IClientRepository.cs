using ApiCatalogoClientes.Domain.Entities;

namespace ApiCatalogoClientes.Interfaces
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task SoftDeleteById(int id);
        Task<IEnumerable<Client>> GetAllActives();
        Task<bool> ClientExistsByDocumentNumber(string  documentNumber);
        Task<Client?> GetByDocumentNumber(string documentNumber);
    }
}
