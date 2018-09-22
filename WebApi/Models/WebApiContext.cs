using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class WebApiContext:DbContext
    {
        public WebApiContext(DbContextOptions<WebApiContext> opciones):base(opciones){}
        public DbSet<Usuario> Usuarios{get;set;}
        public DbSet<Sala> Salas{get;set;}
        public DbSet<Charla> Charlas{get;set;}
        public DbSet<CharlaDetalle> CharlaDetalles{get;set;}
        public DbSet<TipoUsuario> TipoUsuario{get;set;}
    }
}