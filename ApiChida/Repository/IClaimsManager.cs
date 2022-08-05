using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace ApiChida.Repository
{
    public interface IClaimsManager
    {
        Task<IEnumerable<AspNetClaim>> GetClaims();
        Task<AspNetClaim> GetClaim(int id);
        Task<AspNetClaim> AddClaim(AspNetClaim claim);
        Task<AspNetClaim> UpdateClaim(AspNetClaim claim);
        Task<AspNetClaim> DeleteClaim(int id);
       
    }
}
