using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using UsuariosApi.Api.Domain.Data.Dtos;

namespace UsuariosApi.Api.Application.Authorization
{
    public class Supervisor : IAuthorizationRequirement
    {
        public ETipoCargo eTipoCargo { get; set; }

        public Supervisor(ETipoCargo cargo)
        {
            eTipoCargo = cargo;
        }
    }
}