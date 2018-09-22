using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class CharlaDetalle
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int idCharla { get; set; }
        [ForeignKey("idCharla")]
        public virtual Charla Charla { get; set; }
        public int idAsistente { get; set; }
        [ForeignKey("idAsistente")]
        public virtual Usuario Asistente{get;set;}

        public bool Eliminado { get; set; }
    }
}