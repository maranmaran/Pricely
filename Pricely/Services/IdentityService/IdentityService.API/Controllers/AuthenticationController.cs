using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityService.API.Controllers
{
    public class AuthenticationController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> SignIn(string username, string password, CancellationToken cancellationToken)
        {
            //return Ok(Mediator.Send(new ))
            return Ok();
        }

    }
}
