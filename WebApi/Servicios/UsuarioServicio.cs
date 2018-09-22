using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.Helpers;
using Microsoft.EntityFrameworkCore;
namespace WebApi.Servicios
{
    public interface IUsuarioServicio
    {
        Usuario Logeo(string correo, string password);
        IEnumerable<Usuario> Listar();

        IEnumerable<TipoUsuario> ListarTipoUsuario();
        Usuario obtenerId(int id);
        Usuario Create(Usuario usuario, string password);
        void Edit(Usuario usuario, string password = null);
        void Delete(int id);
    }
    public class UsuarioServicio: IUsuarioServicio
    {
        private WebApiContext _contexto;
        public UsuarioServicio(WebApiContext contexto)
        {
            _contexto = contexto;
        }

        public Usuario Logeo(string Correo, string Password)
        {
            if (string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(Password))
            {
                return null;
            }

            if (!Correo.EndsWith("@tektonlabs.com"))
            {
                return null;
            }

            var usuarioDb = _contexto.Usuarios
                .Include(u => u.tipoUsuario)
            .FirstOrDefault(d => d.Correo.Equals(Correo));

            if (usuarioDb == null)
            {
                return null;
            }

            if(!(usuarioDb.tipoUsuario.Nombre.StartsWith("Asistente")|| usuarioDb.tipoUsuario.Nombre.StartsWith("Admin")))
            {
                return null;
            }

            if (!VerificarPasswordHash(Password, usuarioDb.PasswordHash, usuarioDb.PasswordSalt))
            {
                return null;
            }

            return usuarioDb;
        }

        public IEnumerable<Usuario> Listar()
        {
            return _contexto.Usuarios.Include(u => u.tipoUsuario).Where(d => !d.Eliminado).ToList();
        }

        public IEnumerable<TipoUsuario> ListarTipoUsuario()
        {
            return _contexto.TipoUsuario.ToList();
        }

        public Usuario obtenerId(int Id)
        {
            return _contexto.Usuarios.Include(u => u.tipoUsuario).FirstOrDefault(d => d.Id == Id);
        }

        public Usuario Create(Usuario usuario, string Password)
        {
            if (string.IsNullOrWhiteSpace(Password))
            {
                throw new AppException("Ingresar Password");
            }

            if (_contexto.Usuarios.Any(d => d.Correo.Equals(usuario.Correo)))
            {
                throw new AppException("El usuario " + usuario.Correo + " ya existe");
            }

            byte[] passwordHash, passwordSalt;
            CrearPasswordHash(Password,out passwordHash,out passwordSalt);

            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;
            usuario.Eliminado = false;
            _contexto.Usuarios.Add(usuario);
            _contexto.SaveChanges();

            return usuario;
        }

        public void Edit(Usuario usuario, string Password = null)
        {
            var usuarioDb = _contexto.Usuarios.SingleOrDefault(d => d.Id == usuario.Id && !d.Eliminado);
            if(usuarioDb == null)
                throw new AppException("Usuario no encontrado");

            if(!usuario.Correo.Equals(usuarioDb.Correo))
            {
                if(_contexto.Usuarios.Any(d => d.Correo.Equals(usuario.Correo)))
                    throw new AppException("Usuario " + usuario.Correo + " en uso");
            }

            usuarioDb.Nombres = usuario.Nombres;
            usuarioDb.Apellidos = usuario.Apellidos;
            usuarioDb.Correo = usuario.Correo;

            if(!string.IsNullOrWhiteSpace(Password))
            {
                byte[] passwordHash, passwordSalt;
                CrearPasswordHash(Password,out passwordHash,out passwordSalt);
                usuarioDb.PasswordHash = passwordHash;
                usuarioDb.PasswordSalt = passwordSalt;
            }
            _contexto.Usuarios.Update(usuarioDb);
            _contexto.SaveChanges();
        }

        public void Delete(int id)
        {
            var usuarioDb = _contexto.Usuarios.SingleOrDefault(d => d.Id == id && !d.Eliminado);
            if(usuarioDb == null)
                throw new AppException("Usuario no encontrado");

            usuarioDb.Eliminado = true;
            _contexto.Usuarios.Update(usuarioDb);
            _contexto.SaveChanges();
        }
        private static bool VerificarPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if(password == null) throw new ArgumentNullException("password");
            if(string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("El valor no puede ser vacio o contener espacios en blanco, solo cadenas","password");
            if(storedHash.Length != 64) throw new ArgumentNullException("Longitud invalida de password hash - se esperaba 64 bytes","passwordHash");
            if(storedSalt.Length != 128) throw new ArgumentNullException("Longitud invalida de password Salt - se esperaba 128 bytes","passwordSalt");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private static void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if(password == null) throw new ArgumentNullException("password");
            if(string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("El valor no puede ser vacio o contener espacios en blanco, solo cadenas","password");
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt =  hmac.Key;
                passwordHash =  hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}