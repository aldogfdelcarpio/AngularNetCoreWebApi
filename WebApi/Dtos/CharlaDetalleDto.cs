namespace WebApi.Dtos
{
    public class CharlaDetalleDto
    {
        public int Id { get; set; }
        public int idCharla { get; set; }
        public string Charla { get; set; }
        public int idAsistente { get; set; }
        public string Asistente{get;set;}
    }
}