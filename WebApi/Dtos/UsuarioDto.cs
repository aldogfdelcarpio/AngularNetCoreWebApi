using System.Collections.Generic;
using WebApi.Models;
namespace WebApi.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public int idTipoUsuario { get; set; }
        public string TipoUsuario{get;set;}

        //public List<TipoUsuario> tiposUsuario{get;set;}

        public int CharlasMatriculadas { get; set; }
        public int LimiteCharlas{get;set;}
        public string token { get; set; }
    }
}