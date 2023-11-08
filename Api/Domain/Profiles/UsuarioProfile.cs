using AutoMapper;
using UsuariosApi.Api.Domain.Data.Dtos;
using UsuariosApi.Api.Domain.Models;

namespace UsuariosApi.Api.Domain.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
        }
    }
}
