using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Charla
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }

        public DateTime Fecha{get;set;}
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        
        public int idSala { get; set; }
        [ForeignKey("idSala")]
        public virtual Sala Sala { get; set; }

        public int idSpeaker { get; set; }
        [ForeignKey("idSpeaker")]
        public virtual Usuario Speaker { get; set; }

        public bool Eliminado { get; set; }

        public virtual ICollection<CharlaDetalle> AsistentesCharla { get; set; }
    }
}