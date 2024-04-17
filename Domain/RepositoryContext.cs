using ApiCatalogoClientes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoClientes.Domain
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<TypeDocument> TypeDocuments { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // DEFAULT VALUES
            modelBuilder.Entity<Client>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP()");

            // RELATIONS
            modelBuilder.Entity<Client>()
                .HasOne(cli => cli.TypeDocument)
                .WithMany(tdoc => tdoc.Clients)
                .HasForeignKey(cli => cli.IdTypeDocument);
        }
    }
}
