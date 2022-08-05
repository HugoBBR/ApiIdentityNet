using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace ApiChida.Repository
{
    public class ClaimsManager:IClaimsManager
    {
        private readonly ApplicationDbContext _context;
        public ClaimsManager(ApplicationDbContext context)
        {
            this._context = context;
        }
        public virtual async Task<IEnumerable<AspNetClaim>> GetClaims()
        {
            return await _context.AspNetClaims.ToListAsync();
        }

        public async Task<AspNetClaim> GetClaim(int id)
        {
            return await _context.AspNetClaims.
                FirstOrDefaultAsync(c =>c.Id == id);
        }

        public virtual async Task<AspNetClaim> AddClaim(AspNetClaim claim)
        {
            var result= await _context.AspNetClaims.AddAsync(claim);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<AspNetClaim> UpdateClaim(AspNetClaim claim)
        {
            var result = await _context.AspNetClaims.
                FirstOrDefaultAsync(e=>e.Id==claim.Id);
            if (result != null)
            {
                result.Value = claim.Value;
                result.Type = claim.Type;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<AspNetClaim> DeleteClaim(int id)
        {
            var result = await _context.AspNetClaims.
                FirstOrDefaultAsync(c => c.Id == id);
            if (result != null)
            {
                _context.AspNetClaims.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
