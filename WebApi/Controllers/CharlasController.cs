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
    public class CharlasController: Controller
    {
        private ICharlaServicio _charlaServicio;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public CharlasController(
            ICharlaServicio charlaServicio,
            IMapper mapper,
            IOptions<AppSettings> appSettings
        )
        {
            _charlaServicio = charlaServicio;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var charlas = _charlaServicio.Listar();
            var charlasDto = _mapper.Map<IList<CharlaDto>>(charlas);
            return Ok(charlasDto);
        }

        [HttpGet("ListarSpeakers")]
        public IActionResult ListarSpeakers()
        {
            var speakers = _charlaServicio.ListarSpeakers();
            var speakerDto = _mapper.Map<IList<UsuarioDto>>(speakers);
            return Ok(speakerDto);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var charla = _charlaServicio.obtenerId(id);
            var charlaDto = _mapper.Map<CharlaDto>(charla);
            return Ok(charlaDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CharlaDto charlaDto)
        {
            var charla = _mapper.Map<Charla>(charlaDto);
            try
            {
                _charlaServicio.Create(charla);
                return Ok();
            }
            catch (AppException Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id,[FromBody]CharlaDto charlaDto)
        {
            var charla = _mapper.Map<Charla>(charlaDto);
            charla.Id = id;
            try
            {
                _charlaServicio.Edit(charla);
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
            _charlaServicio.Delete(id);
            return Ok();
        }
    }
}