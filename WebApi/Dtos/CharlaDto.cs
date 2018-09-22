using System;
using System.Collections.Generic;

namespace WebApi.Dtos
{
    public class CharlaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha{get;set;}
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int idSala { get; set; }
        public string Sala { get; set; }
        //public List<SalaCombo> ListaSalas{get;set;}
        public int idSpeaker { get; set; }
        public string Speaker { get; set; }
        //public List<SpeakerCombo> ListaSpeakers{get;set;}
        public int CantidadAsistentes { get; set; }
        public int CantidadDisponible{get;set;}
    }
}