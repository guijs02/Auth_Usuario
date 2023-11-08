using Microsoft.AspNetCore.Authorization;

namespace UsuariosApi.Api.Application.Authorization
{
    public class SupervisorAuthorization : AuthorizationHandler<Supervisor>
    {
        protected override async Task HandleRequirementAsync(
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
                await Task.CompletedTask;
                return;
            }
            context.Fail();
            //await Task.CompletedTask;
        }
    }
}
