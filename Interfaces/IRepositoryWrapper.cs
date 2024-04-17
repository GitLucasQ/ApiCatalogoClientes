namespace ApiCatalogoClientes.Interfaces
{
    public interface IRepositoryWrapper
    {
        IClientRepository Clients { get; }
        Task SaveAsync();
    }
}
