using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApi.Models
{
    public class Usuario
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public int idTipoUsuario { get; set; }
        [ForeignKey("idTipoUsuario")]
        public virtual TipoUsuario tipoUsuario { get; set; }
        public bool Eliminado { get; set; }

        public virtual ICollection<Charla> charlasSpeaker { get; set; }

        public virtual ICollection<CharlaDetalle> charlasAsistidas {get;set;}
    }
}