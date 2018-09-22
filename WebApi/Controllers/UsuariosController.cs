using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Servicios;
using WebApi.Dtos;
using AutoMapper;
using WebApi.Helpers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebApi.Models;
using Microsoft.AspNetCore.Authorization;
namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class UsuariosController : Controller
    {
        private IUsuarioServicio _usuarioServicio;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsuariosController(
            IUsuarioServicio usuarioServicio,
            IMapper mapper,
            IOptions<AppSettings> appSettings
        )
        {
            _usuarioServicio = usuarioServicio;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Autenticacion([FromBody]UsuarioDto usuarioDto)
        {
            var usuario = _usuarioServicio.Logeo(usuarioDto.Correo,usuarioDto.Password);
            if(usuario == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key =  Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name,usuario.Id.ToString())
                }),
                Expires =  DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var usuarioDtoResp = _mapper.Map<UsuarioDto>(usuario);
            usuarioDtoResp.token = tokenString;

            return Ok(usuarioDtoResp);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UsuarioDto usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            try
            {
                _usuarioServicio.Create(usuario,usuarioDto.Password);
                return Ok();
            }
            catch (AppException Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Listar()
        {
            var usuarios = _usuarioServicio.Listar();
            var usuariosDto = _mapper.Map<IList<UsuarioDto>>(usuarios);
            return Ok(usuariosDto);
        }

        [HttpGet("ListarTipoUsuario")]
        public IActionResult ListarTipoUsuario()
        {
            var tipousuarios = _usuarioServicio.ListarTipoUsuario();
            var tipoUsuarioDto = _mapper.Map<IList<TipoUsuarioDTO>>(tipousuarios);
            return Ok(tipoUsuarioDto);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var usuario = _usuarioServicio.obtenerId(id);
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            return Ok(usuarioDto);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id,[FromBody]UsuarioDto usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            usuario.Id = id;
            try
            {
                _usuarioServicio.Edit(usuario,usuarioDto.Password);
                return Ok();
            }
            catch (AppException Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _usuarioServicio.Delete(id);
            return Ok();
        }

    }
}