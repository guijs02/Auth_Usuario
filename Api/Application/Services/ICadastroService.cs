using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Api.Domain.Data.Dtos;

namespace UsuariosApi.Api.Application.Services
{
    public interface IUsuarioService
    {
        Task Cadastro(CreateUsuarioDto dto);
        Task<string> Login(LoginUsuarioDto dto);
        Task Logout();
    }
}