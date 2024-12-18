using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnicoDomain.Context;
using TechnicoDomain.Models;
using TechnicoDomain.Dtos;

namespace TechnicoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly TechnicoDbContext _context;

        public OwnersController(TechnicoDbContext context)
        {
            _context = context;
        }

        // GET: api/Owners/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerResponseDto>>> GetOwners()
        {
            return await _context
                .Owners
                .Include(owner => owner.PropertyItems)
                .Select(owner => new OwnerResponseDto
                {
                    VATNumber = owner.VATNumber,
                    Email = owner.Email,
                    Id = owner.Id,
                    Name = owner.Name,
                    Surname = owner.Surname,
                    PhoneNumber = owner.PhoneNumber,
                })
                .ToListAsync();
        }

        // GET: api/Owners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerResponseDto>> GetOwner(long id)
        {
            var owner = await _context.Owners.FirstOrDefaultAsync(p => p.Id == id);

            if (owner == null)
            {
                return NotFound();
            }

            return new OwnerResponseDto
            {
                VATNumber = owner.VATNumber,
                Email = owner.Email,
                Password = owner.Password,
                Id = owner.Id,
                Name = owner.Name,
                Surname = owner.Surname,
                PhoneNumber = owner.PhoneNumber
            };
        }


        // PUT: api/Owners/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOwner(long id, Owner owner)
        {
            if (id != owner.Id)
            {
                return BadRequest();
            }

            _context.Entry(owner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Owners
        [HttpPost]
        public async Task<ActionResult<OwnerResponseDto>> PostOwner(Owner owner)
        {
            //set ownerType default value 1
            owner.OwnerType = OwnerType.Admin;

            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            return new OwnerResponseDto
            {
                Email = owner.Email,
                Id = owner.Id,
                Name = owner.Name,
                Owner = "owner",
            };
        }

        // DELETE: api/Owners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(long id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OwnerExists(long id)
        {
            return _context.Owners.Any(e => e.Id == id);
        }
    }
}
