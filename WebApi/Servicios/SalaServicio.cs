using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.Helpers;
namespace WebApi.Servicios
{
    public interface ISalaServicio
    {
        IEnumerable<Sala> Listar();
        Sala obtenerId(int id);
        Sala Create(Sala sala);
        void Edit(Sala sala);
        void Delete(int id);
    }
    public class SalaServicio : ISalaServicio
    {
        private WebApiContext _contexto;
        public SalaServicio(WebApiContext contexto)
        {
            _contexto = contexto;
        }
        public IEnumerable<Sala> Listar()
        {
            return _contexto.Salas.Where(d => !d.Eliminado).ToList();
        }

        public Sala obtenerId(int Id)
        {
            return _contexto.Salas.Find(Id);
        }

        public Sala Create(Sala sala)
        {
            if (string.IsNullOrWhiteSpace(sala.Nombre))
            {
                throw new AppException("Ingresar Nombre");
            }

            if(sala.Capacidad <= 0)
            {
                throw new AppException("Capacidad debe ser mayor a cero(0)");
            }

            sala.Eliminado = false;
            _contexto.Salas.Add(sala);
            _contexto.SaveChanges();

            return sala;
        }

        public void Edit(Sala sala)
        {
            var salaDb = _contexto.Salas.SingleOrDefault(d => d.Id == sala.Id && !d.Eliminado);
            if(salaDb == null)
                throw new AppException("Sala no encontrada");

            salaDb.Nombre = sala.Nombre;
            salaDb.Capacidad = sala.Capacidad;

            _contexto.Salas.Update(salaDb);
            _contexto.SaveChanges();
        }

        public void Delete(int id)
        {
            var salaDb = _contexto.Salas.SingleOrDefault(d => d.Id == id && !d.Eliminado);
            if(salaDb == null)
                throw new AppException("Sala no encontrada");

            salaDb.Eliminado = true;
            _contexto.Salas.Update(salaDb);
            _contexto.SaveChanges();
        }
    }
}