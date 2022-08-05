using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace ApiChida.Controllers
{
    [Route("identity")]

    public class IdentityController:ControllerBase
    {
        [HttpGet]
        public IActionResult get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
