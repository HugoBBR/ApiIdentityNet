using Microsoft.AspNetCore.Mvc;
using ApiChida.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiChida.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoleController : ControllerBase
    {

        private readonly RoleManager<ApplicationRole> _userRole;
        


        public RoleController( RoleManager<ApplicationRole> userRole)
        {
            _userRole = userRole;

        }
      

    

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] ApplicationRole applicationRole)
        {
            
            var result = await _userRole.CreateAsync(applicationRole);
            
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponse { Errors = errors });


            }
            

            return StatusCode(201);
        }
        [HttpGet]


        public IEnumerable<ApplicationRole> Roles()
        {
            return _userRole.Roles;
        }
 

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userRole.DeleteAsync(_userRole.FindByIdAsync(id).Result);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponse { Errors = errors });


            }
            return StatusCode(201);
        }
      

    }
}