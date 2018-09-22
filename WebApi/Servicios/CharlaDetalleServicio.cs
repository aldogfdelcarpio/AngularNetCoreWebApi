using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.Helpers;
using Microsoft.EntityFrameworkCore;
namespace WebApi.Servicios
{
    public interface ICharlaDetalleServicio
    {
        IEnumerable<CharlaDetalle> Listar();
        IEnumerable<Charla> ListarMisCharlas(int id);
        IEnumerable<Charla> ListarCharlasDisponibles(int id);
        CharlaDetalle obtenerId(int id);
        CharlaDetalle Create(CharlaDetalle charlaDetalle);
        void Delete(int idAsistente, int idCharla);
    }
    public class CharlaDetalleServicio: ICharlaDetalleServicio
    {
        private WebApiContext _contexto;

        public CharlaDetalleServicio(WebApiContext contexto )
        {
            _contexto = contexto;
        }

        public IEnumerable<CharlaDetalle> Listar()
        {
            return _contexto.CharlaDetalles.Where(d => !d.Eliminado)
                .OrderByDescending(d => d.Charla.Fecha)
                .ThenByDescending(d => d.Charla.HoraInicio).ToList();
        }

        public IEnumerable<Charla> ListarMisCharlas(int idAsistente)
        {
            var misCharlas = _contexto.CharlaDetalles.Where(d => !d.Eliminado && d.idAsistente == idAsistente)
                .OrderByDescending(d => d.Charla.Fecha)
                .ThenByDescending(d => d.Charla.HoraInicio).Select(d => d.idCharla).ToList();
            return _contexto.Charlas.Where(d => !d.Eliminado && misCharlas.Contains(d.Id))
                .OrderByDescending(d => d.Fecha)
                .ThenByDescending(d => d.HoraInicio).ToList();
        }

        public IEnumerable<Charla> ListarCharlasDisponibles(int idAsistente)
        {
            var misCharlas = _contexto.CharlaDetalles.Where(d => !d.Eliminado && d.idAsistente == idAsistente)
                .OrderByDescending(d => d.Charla.Fecha)
                .ThenByDescending(d => d.Charla.HoraInicio).Select(d => d.idCharla).ToList();
            return _contexto.Charlas.Where(d => !d.Eliminado && !misCharlas.Contains(d.Id))
                .OrderByDescending(d => d.Fecha)
                .ThenByDescending(d => d.HoraInicio).ToList();
        }

        public CharlaDetalle obtenerId(int Id)
        {
            return _contexto.CharlaDetalles.Find(Id);
        }

        public CharlaDetalle Create(CharlaDetalle charlaDetalle)
        {
            var charla = _contexto.Charlas.Include(d => d.AsistentesCharla).Include(d => d.Sala).SingleOrDefault(d => d.Id == charlaDetalle.idCharla);
            int cantidadAsistentes = charla.AsistentesCharla.Where(d => !d.Eliminado).Count() + 1;
            if(charla.Sala.Capacidad < cantidadAsistentes)
            {
                throw new AppException("Sala llena, no se pudo llevar a cabo la inscripción");
            }
            var asistente = _contexto.Usuarios.Include(d => d.charlasAsistidas).Include(d => d.tipoUsuario).SingleOrDefault(d => d.Id == charlaDetalle.idAsistente);
            var cantidadCharlas = asistente.charlasAsistidas.Where(d => !d.Eliminado).Count() + 1;
            if(asistente.tipoUsuario.cantidadMaximaCharlas < cantidadCharlas)
            {
                throw new AppException("Supero la cantidad de charlas a las que puede asistir, no se pudo llevar a cabo la inscripción");
            }

            var query = from c in _contexto.Usuarios.Where(d => d.Id == charlaDetalle.idAsistente)
                .Select(d => new {
                    d,
                    charlasAsistidas = d.charlasAsistidas.Where(v => !v.Eliminado)
                })
                .AsEnumerable()
                .Select(x => x.d)
                .ToList()
                select c;

            var asistenteDb = query.FirstOrDefault();
            bool cruceCharlas = asistenteDb.charlasAsistidas.Any(e => !e.Eliminado && e.Charla.Fecha.Date == charla.Fecha.Date &&
                (e.Charla.HoraInicio.Ticks >= charla.HoraInicio.Ticks && e.Charla.HoraInicio.Ticks <= charla.HoraFin.Ticks ||
                e.Charla.HoraFin.Ticks >= charla.HoraInicio.Ticks && e.Charla.HoraFin.Ticks <= charla.HoraFin.Ticks ));

            if(cruceCharlas)
            {
                throw new AppException("Tienes un cruce con otra charla");
            }

            charlaDetalle.Eliminado = false;
            _contexto.CharlaDetalles.Add(charlaDetalle);
            _contexto.SaveChanges();
            return charlaDetalle;
        }

        public void Delete(int idAsistente, int idCharla)
        {
            var charlaDetalleDb = _contexto.CharlaDetalles.SingleOrDefault(d => d.idAsistente == idAsistente && d.idCharla == idCharla && !d.Eliminado);
            if(charlaDetalleDb == null)
                throw new AppException("Charla detalle no encontrado");

            charlaDetalleDb.Eliminado = true;
            _contexto.CharlaDetalles.Update(charlaDetalleDb);
            _contexto.SaveChanges();
        }
    }
}