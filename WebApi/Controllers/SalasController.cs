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
    public class SalasController : Controller
    {
        private ISalaServicio _salaServicio;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public SalasController(
            ISalaServicio salaServicio,
            IMapper mapper,
            IOptions<AppSettings> appSettings
        )
        {
            _salaServicio = salaServicio;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var salas = _salaServicio.Listar();
            var salasDto = _mapper.Map<IList<SalaDto>>(salas);
            return Ok(salasDto);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var sala = _salaServicio.obtenerId(id);
            var salaDto = _mapper.Map<SalaDto>(sala);
            return Ok(salaDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SalaDto salaDto)
        {
            var sala = _mapper.Map<Sala>(salaDto);
            try
            {
                _salaServicio.Create(sala);
                return Ok();
            }
            catch (AppException Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id,[FromBody]SalaDto salaDto)
        {
            var sala = _mapper.Map<Sala>(salaDto);
            sala.Id = id;
            try
            {
                _salaServicio.Edit(sala);
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
            _salaServicio.Delete(id);
            return Ok();
        }
    }
}