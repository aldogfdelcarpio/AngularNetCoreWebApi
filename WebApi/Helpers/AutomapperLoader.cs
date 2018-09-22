using AutoMapper;
using WebApi.Models;
using WebApi.Dtos;
using System.Linq;
namespace WebApi.Helpers
{
    public class AutomapperLoader : Profile
    {
        public AutomapperLoader()
        {
            CreateMap<Usuario,UsuarioDto>()
            .ForMember(dest => dest.TipoUsuario, opt => opt.MapFrom(scr => scr.tipoUsuario.Nombre))
            .ForMember(dest => dest.LimiteCharlas, opt => opt.MapFrom(scr => scr.tipoUsuario.cantidadMaximaCharlas))
            .ForMember(dest => dest.CharlasMatriculadas, opt => opt.MapFrom(scr => scr.charlasAsistidas.Where(d => !d.Eliminado).Count()))
            // .ForMember(dest => dest.tiposUsuario, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.token, opt => opt.Ignore());

            CreateMap<UsuarioDto,Usuario>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
            .ForMember(dest => dest.tipoUsuario, opt => opt.Ignore())
            .ForMember(dest => dest.Eliminado, opt => opt.Ignore())
            .ForMember(dest => dest.charlasAsistidas, opt => opt.Ignore())
            .ForMember(dest => dest.charlasSpeaker, opt => opt.Ignore());

            CreateMap<Sala,SalaDto>();
            CreateMap<SalaDto,Sala>();

            CreateMap<Charla,CharlaDto>()
            .ForMember(dest => dest.Sala, opt => opt.MapFrom(scr => scr.Sala.Nombre))
            .ForMember(dest => dest.Speaker, opt => opt.MapFrom(scr => scr.Speaker.Nombres + " " + scr.Speaker.Apellidos))
            .ForMember(dest => dest.CantidadAsistentes, opt => opt.MapFrom(scr => scr.AsistentesCharla.Where(d => !d.Eliminado).Count()))
            .ForMember(dest => dest.CantidadDisponible, opt => opt.MapFrom(scr => scr.Sala.Capacidad - scr.AsistentesCharla.Where(d => !d.Eliminado).Count()))
            // .ForMember(dest => dest.ListaSalas,opt => opt.Ignore())
            // .ForMember(dest => dest.ListaSpeakers, opt => opt.Ignore())
            ;
            
            CreateMap<CharlaDto,Charla>()
            .ForMember(dest => dest.Sala, opt => opt.Ignore())
            .ForMember(dest => dest.Speaker, opt => opt.Ignore())
            .ForMember(dest => dest.AsistentesCharla , opt => opt.Ignore())
            .ForMember(dest => dest.Eliminado , opt => opt.Ignore());

            CreateMap<CharlaDetalle,CharlaDetalleDto>()
            .ForMember(dest => dest.Charla,opt => opt.MapFrom(scr => scr.Charla.Nombre))
            .ForMember(dest => dest.Asistente,opt => opt.MapFrom(scr => scr.Asistente.Nombres + " " + scr.Asistente.Apellidos));

            CreateMap<CharlaDetalleDto,CharlaDetalle>()
            .ForMember(dest => dest.Charla,opt => opt.Ignore())
            .ForMember(dest => dest.Asistente,opt => opt.Ignore())
            .ForMember(dest => dest.Eliminado,opt => opt.Ignore());

            CreateMap<TipoUsuario,TipoUsuarioDTO>();
            CreateMap<TipoUsuarioDTO,TipoUsuario>();
        }
    }
}