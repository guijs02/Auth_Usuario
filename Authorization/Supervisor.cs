using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using UsuariosApi.Data.Dtos;

namespace UsuariosApi.Authorization
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