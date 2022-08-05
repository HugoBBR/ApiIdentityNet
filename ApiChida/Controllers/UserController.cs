using Microsoft.AspNetCore.Mvc;
using ApiChida.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ApiChida.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace ApiChida.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {




        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IClaimsManager _claimsManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

        private readonly IMapper _mapper;

        public UserController( IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,UserManager<ApplicationUser> userManager,IMapper mapper, RoleManager<ApplicationRole> roleManager,IClaimsManager claimsManager)
        {
            _userClaimsPrincipalFactory= userClaimsPrincipalFactory;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _claimsManager= claimsManager;

        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] InputUsertModel applicationUser)
        {
            var user=_mapper.Map<ApplicationUser>(applicationUser); 
            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            if (!result.Succeeded)
            {
                

                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponse { Errors = errors });


            }

            return StatusCode(201);
        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] OutputUserModel applicationUser)
        {
            var user = _userManager.FindByIdAsync(applicationUser.Id).Result;
            if (user != null)
            {

                user.UserName = applicationUser.UserName;
            
                user.PhoneNumber = applicationUser.PhoneNumber;
                user.Email = applicationUser.Email;
                user.FirstName = applicationUser.FirstName;
                user.LastName = applicationUser.LastName;
                user.IsEnabled = applicationUser.IsEnabled;

                if(applicationUser.PasswordHash != null)
                {
                    string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, resetToken, applicationUser.PasswordHash);
                }



            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {


                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponse { Errors = errors });


            }

            return StatusCode(201);
        }


        [HttpGet]
        public IEnumerable<OutputUserModel> Users()
        {
            var users = _userManager.Users;
            var output=users.Select(w=>new OutputUserModel()
            {
                Id = w.Id,
                FirstName = w.FirstName,
                LastName = w.LastName,
                UserName = w.UserName,
                PhoneNumber = w.PhoneNumber,
                Email = w.Email,
                IsEnabled= w.IsEnabled
            }).ToList();

            return output;
        }

        [HttpGet]
        public IEnumerable<ApplicationUser> UsersComplete()
        {
            var output = _userManager.Users;

            return output;
        }

        [HttpGet("{id}")]
        public OutputUserModel User(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var pass = _userManager.PasswordHasher.ToString();
            var output = new OutputUserModel()
            {
                Id = user.Id,
                FirstName=user.FirstName,
                LastName=user.LastName,
                UserName = user.UserName,
                PasswordHash=user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                IsEnabled = user.IsEnabled
            };

            return output;
        }

        [HttpGet("{id}")]
        public IEnumerable<IdentityRole> UserRoles(string id)
        {
            var roles=_userManager.GetRolesAsync(_userManager.FindByIdAsync(id).Result).Result;
            List<IdentityRole> roleU = new List<IdentityRole>();
            foreach (var role in roles)
            {
                var r = _roleManager.FindByNameAsync(role).Result;
                roleU.Add(new IdentityRole()
                {
                    Id=r.Id,
                    Name=r.Name,
                    NormalizedName=r.NormalizedName,
                    ConcurrencyStamp=r.ConcurrencyStamp,

                });
            }
            return roleU;
        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userManager.DeleteAsync(_userManager.FindByIdAsync(id).Result);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponse { Errors = errors });


            }
            return StatusCode(201);
        }
        [HttpPost("{idUser}")]
        public async Task<IActionResult> addUserRole([FromBody] string[] roles, string idUser)
        {
            
         

            var result = await _userManager.AddToRolesAsync(_userManager.FindByIdAsync(idUser).Result,roles);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponse { Errors = errors });


            }
            return StatusCode(201);
        }


        [HttpDelete("{id}/{roleName}")]
        public async Task<IActionResult> RemoveUserRole(string id,string roleName)
        {
            
            var result = await _userManager.RemoveFromRoleAsync(_userManager.FindByIdAsync(id).Result, roleName);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponse { Errors = errors });


            }
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        public IEnumerable<ApplicationRole> AviableRoles(string id)
        {
            string userSelector(ApplicationRole identityRole) => identityRole.Name;

            var userRoles = _userManager.GetRolesAsync(_userManager.FindByIdAsync(id).Result).Result;
            var result = _roleManager.Roles.ExceptBy(userRoles, userSelector).ToList();
            



            return result;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddUserClaim([FromBody] AspNetClaim[] claimsS,string id)
        {

            List<Claim> claims=new List<Claim> { };
            
            foreach(var claim in claimsS)
            {
                claims.Add(new Claim(claim.Type, claim.Value));
                
            }
            var user = await _userManager.FindByIdAsync(id);

            var result = await _userManager.AddClaimsAsync(user,claims);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponse { Errors = errors });
            }
            return StatusCode(201);

        }

        [HttpGet("{id}")]
        public  IEnumerable<AspNetClaim> UserClaims(string id)
        {

           
            
            var user = _userManager.FindByIdAsync(id).Result;
            var claims = _userManager.GetClaimsAsync(user).Result;
            var principal = _userClaimsPrincipalFactory.CreateAsync(user).Result;
            
            var list=claims.Select(w => new AspNetClaim()
            {
                Type = w.Type,
                Value = w.Value
            }).ToList();

            return list;

        }

    }
}