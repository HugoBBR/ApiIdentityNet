using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiChida;
using Microsoft.AspNetCore.Identity;
using ApiChida.Repository;
 
namespace ApiChida.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class ClaimController : ControllerBase
    {

        private readonly IClaimsManager _claimsManager;

        public ClaimController(IClaimsManager claimsManager )
        {
            _claimsManager= claimsManager;
        }




        // POST: ClaimController/Create
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AspNetClaim claim)
        {

            var result = await _claimsManager.AddClaim(claim);
            if (result==null)
            {
                return BadRequest(new RegistrationResponse { });

            }


            return StatusCode(201);

        }

        [HttpGet]
        public IEnumerable<AspNetClaim> Claims()
        {
            return _claimsManager.GetClaims().Result;
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var claimToDelete = await _claimsManager.GetClaim(id);

                if (claimToDelete == null)
                {
                    return NotFound($"Claim with Id = {id} not found");
                }
                else
                {
                    await _claimsManager.DeleteClaim(id);

                    return StatusCode(201);
                }

                 
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    

        [HttpGet("{id}")]
        public AspNetClaim Claim(int id)
        {
            var result = _claimsManager.GetClaim(id).Result;
            return result;
        }







    }
}
