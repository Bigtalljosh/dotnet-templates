using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MyProject.Api.Controllers
{
    public class ScopeController : ControllerBase
    {

        // This is a helper action. It allows you to easily view all the claims of the token. Don't include this in your project.
        [HttpGet("claims")]
        public IActionResult Claims()
        {
            return Ok(User.Claims.Select(c =>
                new
                {
                    c.Type,
                    c.Value
                }));
        }
    }
}
