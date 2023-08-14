using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosApi.Authorization
{
    public class SupervisorAuthorization : AuthorizationHandler<Supervisor>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            Supervisor requirement
        )
        {
            var TipoCargoClaim =
                context.User.FindFirst(f => f.Type.ToString() == "tipoCargo")
                ?? throw new NullReferenceException("TipoCargoClaim está nulo");

            if (requirement.eTipoCargo.ToString() == TipoCargoClaim.Value)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            context.Fail();
            throw new UnauthorizedAccessException("Acesso não autorizado");
        }
    }
}
