using IdentityService.Business.Commands.Authentication.SignIn;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityService.API.Controllers
{
    public class AuthenticationController : BaseController
    {
        /// <summary>
        /// Sign in 
        /// </summary>
        /// <remarks>
        /// Signs user in and retrieves JWT with claims
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> SignIn(string email, string password, bool rememberMe, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new SignInCommand(email, password, rememberMe), cancellationToken));
        }

    }
}
