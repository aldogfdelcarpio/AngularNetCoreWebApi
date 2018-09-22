using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Dtos;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Servicios;
namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class CharlasDetalleController: Controller
    {
        private ICharlaDetalleServicio _charlaDetalleServicio;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public CharlasDetalleController(
            ICharlaDetalleServicio charlaDetalleServicio,
            IMapper mapper,
            IOptions<AppSettings> appSettings
        )
        {
            _charlaDetalleServicio = charlaDetalleServicio;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var charlasDetalle = _charlaDetalleServicio.Listar();
            var charlasDetalleDto = _mapper.Map<IList<CharlaDetalleDto>>(charlasDetalle);
            return Ok(charlasDetalleDto);
        }

        [HttpGet("MisCharlas/{id}")]
        public IActionResult ListarMisCharlas(int id)
        {
            var charlasDetalle = _charlaDetalleServicio.ListarMisCharlas(id);
            var charlasDetalleDto = _mapper.Map<IList<Charla>>(charlasDetalle);
            return Ok(charlasDetalleDto);
        }

        [HttpGet("CharlasDisponibles/{id}")]
        public IActionResult ListarCharlasDisponibles(int id)
        {
            var charla = _charlaDetalleServicio.ListarCharlasDisponibles(id);
            var charlasDto = _mapper.Map<IList<CharlaDto>>(charla);
            return Ok(charlasDto);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var charlaDetalle = _charlaDetalleServicio.obtenerId(id);
            var charlaDetalleDto = _mapper.Map<CharlaDetalleDto>(charlaDetalle);
            return Ok(charlaDetalleDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CharlaDetalleDto charlaDetalleDto)
        {
            var charlaDetalle = _mapper.Map<CharlaDetalle>(charlaDetalleDto);
            try
            {
                _charlaDetalleServicio.Create(charlaDetalle);
                return Ok();
            }
            catch (AppException Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpDelete("{idAsistente}/{idCharla}")]
        public IActionResult Delete(int idAsistente,int idCharla)
        {
            _charlaDetalleServicio.Delete(idAsistente,idCharla);
            return Ok();
        }
    }
}