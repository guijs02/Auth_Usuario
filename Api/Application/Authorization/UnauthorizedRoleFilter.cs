using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UsuariosApi.Api.Application.Authorization
{
    public class UnauthorizedRoleFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.User.IsInRole("Supervisor")){
                context.Result = new StatusCodeResult(401);
            }
        }
    }
}