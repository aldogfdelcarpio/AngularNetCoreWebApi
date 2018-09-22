using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Sala
    {
        public Sala(){
            Charlas = new HashSet<Charla>();
        }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public bool Eliminado { get; set; }

        public virtual ICollection<Charla> Charlas { get; set; }
    }
}