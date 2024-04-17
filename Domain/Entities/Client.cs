using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCatalogoClientes.Domain.Entities
{
    [Table("cliente")]
    public class Client
    {        
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nombres")]
        public string FirstName { get; set; }

        [Column("apellidos")]
        public string LastName { get; set; }

        [Column("fecha_nacimiento")]
        public DateTime BirthDate { get; set; }

        [Column("id_tipo_documento")]
        public int IdTypeDocument { get; set; }

        [Column("numero_documento")]
        public string DocumentNumber { get; set; }

        [Column("archivo_cv")]
        public string FileNameCV { get; set; }

        [Column("archivo_cv_server")]
        public string FileNameCVServer { get; set; }

        [Column("archivo_foto")]
        public string FileNamePhoto { get; set; }

        [Column("archivo_foto_server")]
        public string FileNamePhotoServer { get; set; }

        [Column("fecha_crea")]
        public DateTime CreatedAt { get; set; }

        [Column("fecha_modifica")]
        public DateTime? UpdatedAt { get; set; }

        [Column("activo")]
        public bool IsActive { get; set; } = true;

        // RELATIONS
        public virtual TypeDocument TypeDocument { get; set; }
    }
}
