using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace ProyectoAPI.Handlers
{
    public class AccionRequirement : AuthorizationHandler<AccionRequirement>, IAuthorizationRequirement
    {
        public string Accion { get; set; }

        public AccionRequirement(string accionName)
        {
            Accion = accionName;
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccionRequirement requirement)
        {
            //marca como Succeed la autorizacion cuando el usuario actual contiene un Claim con el Tipo y el Valor igual a Accion
            if (context.User.HasClaim(c => c.Type == Accion && c.Value == Accion))
            {
                context.Succeed(requirement);
            }
            else
            {
                //existen metodos que deben ser llamados por la aplicación mediante AJAX para realizar distintas acciones
                //para estos casos se pregunta si el usuario tiene un Claim de las acciones que llaman al metodo.
                switch (Accion)
                {
                    case "ListarAcciones":
                        if (context.User.HasClaim(c => c.Type == "AdicionarRol" && c.Value == "AdicionarRol" ||
                                                        c.Type == "ActualizarRol" && c.Value == "ActualizarRol"))
                            context.Succeed(requirement);
                        break;
                }
            }
            return Task.FromResult(0);
        }
    }
}
