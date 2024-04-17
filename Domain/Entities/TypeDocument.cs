using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCatalogoClientes.Domain.Entities
{
    [Table("tipo_documento")]
    public class TypeDocument
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("descripcion")]
        public string Description { get; set; }

        // RELATIONS
        public virtual List<Client> Clients { get; set; }
    }
}
