using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApi.Models
{
    public class TipoUsuario
    {
        public TipoUsuario(){
            usuarios = new HashSet<Usuario>();
        }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public int cantidadMaximaCharlas { get; set; }

        public virtual ICollection<Usuario> usuarios{get;set;}
    }
}