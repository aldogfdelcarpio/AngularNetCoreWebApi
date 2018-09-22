using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.Helpers;
using Microsoft.EntityFrameworkCore;
namespace WebApi.Servicios
{
    public interface ICharlaServicio
    {
        IEnumerable<Charla> Listar();
        Charla obtenerId(int id);
        Charla Create(Charla sala);
        void Edit(Charla sala);
        void Delete(int id);
        IEnumerable<Usuario> ListarSpeakers();
    }
    public class CharlaServicio : ICharlaServicio
    {
        private WebApiContext _contexto;
        public CharlaServicio(WebApiContext contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<Charla> Listar()
        {
            return _contexto.Charlas
                    .Include(d => d.Sala)
                    .Include(d => d.Speaker)
                .Where(d => !d.Eliminado)
                .OrderByDescending(d => d.Fecha)
                .ThenByDescending(d => d.HoraInicio).ToList();
        }

        public IEnumerable<Usuario> ListarSpeakers()
        {
            return _contexto.Usuarios.Where(d => !d.Eliminado && d.tipoUsuario.Nombre.Equals("Speaker")).ToList();
        }

        public Charla obtenerId(int Id)
        {
            return _contexto.Charlas.Find(Id);
        }

        private void validarCharla(Charla charla)
        {
            if (string.IsNullOrWhiteSpace(charla.Nombre))
            {
                throw new AppException("Ingresar Nombre");
            }
            if (charla.Fecha == null)
            {
                throw new AppException("Ingresar fecha");
            }
            if (charla.HoraInicio == null)
            {
                throw new AppException("Ingresar Inicio");
            }
            if (charla.HoraFin == null)
            {
                throw new AppException("Ingresar Fin");
            }
            if (charla.idSala == 0)
            {
                throw new AppException("Seleccionar sala");
            }
            if (charla.idSpeaker == 0)
            {
                throw new AppException("Seleccionar speaker");
            }
            var salaOcupada = _contexto.Salas.Where(d => !d.Eliminado && d.Charlas.Any(e => !e.Eliminado && e.Fecha.Date == charla.Fecha.Date &&
               (e.HoraInicio.Ticks >= charla.HoraInicio.Ticks && e.HoraInicio.Ticks <= charla.HoraFin.Ticks ||
               e.HoraFin.Ticks >= charla.HoraInicio.Ticks && e.HoraFin.Ticks <= charla.HoraFin.Ticks)  )).ToList();

            var salaActual = salaOcupada.FirstOrDefault(d => d.Id == charla.idSala);
            if(salaActual != null)
                salaOcupada.Remove(salaActual);                
            if(salaOcupada.Count() > 0 )
            {
                throw new AppException("Sala Ocupada");
            }
            var speakerOcupado = _contexto.Usuarios.Where(d => d.tipoUsuario.Nombre.Equals("speaker") && !d.Eliminado && 
                d.charlasSpeaker.Any(e => !e.Eliminado && e.Fecha.Date == charla.Fecha.Date &&
                (e.HoraInicio.Ticks >= charla.HoraInicio.Ticks && e.HoraInicio.Ticks <= charla.HoraFin.Ticks ||
                e.HoraFin.Ticks >= charla.HoraInicio.Ticks && e.HoraFin.Ticks <= charla.HoraFin.Ticks ) )).ToList();
            
            var speakerActual = speakerOcupado.FirstOrDefault(d => d.Id == charla.idSpeaker);
            if(speakerActual != null)
                speakerOcupado.Remove(speakerActual);
            if(speakerOcupado.Count() > 0 )
            {
                throw new AppException("speaker Ocupado");
            }
        }

        public Charla Create(Charla charla)
        {
            validarCharla(charla);
            charla.Eliminado = false;
            _contexto.Charlas.Add(charla);
            _contexto.SaveChanges();

            return charla;
        }

        public void Edit(Charla charla)
        {
            var charlaDb = _contexto.Charlas.SingleOrDefault(d => d.Id == charla.Id && !d.Eliminado);
            if(charlaDb == null)
                throw new AppException("Charla no encontrada");
            validarCharla(charlaDb);
            
            charlaDb.Nombre = charla.Nombre;
            charlaDb.Fecha = charla.Fecha;
            charlaDb.HoraInicio = charla.HoraInicio;
            charlaDb.HoraFin = charla.HoraFin;
            charlaDb.idSala = charla.idSala;
            charlaDb.idSpeaker = charla.idSpeaker;

            _contexto.Charlas.Update(charlaDb);
            _contexto.SaveChanges();
        }

        public void Delete(int id)
        {
            var charlaDb = _contexto.Charlas.SingleOrDefault(d => d.Id == id && !d.Eliminado);
            if(charlaDb == null)
                throw new AppException("Charla no encontrada");

            charlaDb.Eliminado = true;
            _contexto.Charlas.Update(charlaDb);
            _contexto.SaveChanges();
        }
    }
}