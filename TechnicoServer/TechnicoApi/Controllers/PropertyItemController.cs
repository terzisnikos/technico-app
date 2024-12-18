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
using static TechnicoDomain.Dtos.OwnerResponseDto;

namespace TechnicoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyItemsController : ControllerBase
    {
        private readonly TechnicoDbContext _context;

        public PropertyItemsController(TechnicoDbContext context)
        {
            _context = context;
        }

        // GET: api/PropertyItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyItemResponseDto>>> GetPropertyItems()
        {
            return await _context
                .PropertyItems
                .Select(properyItem => new PropertyItemResponseDto
                {
                    OwnerId = properyItem.OwnerId,
                    OwnerName = properyItem!.Owner!.Name,
                    Id = properyItem.Id,
                    Name = properyItem.Name,
                    PropertyIdentificationNumber = properyItem.PropertyIdentificationNumber,
                    Address = properyItem.Address,
                    YearOfConstruction = properyItem.YearOfConstruction,
                })
                .ToListAsync();
        }

        // GET: api/PropertyItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyItemResponseDto>> GetPropertyItem(long id)
        {
            var properyItem = await _context.PropertyItems
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (properyItem == null)
            {
                return NotFound();
            }

            return new PropertyItemResponseDto
            {
                OwnerId = properyItem.OwnerId,
                OwnerName = properyItem!.Owner!.Name,
                Id = properyItem.Id,
                Name = properyItem.Name,
                PropertyIdentificationNumber = properyItem.PropertyIdentificationNumber,
                Address = properyItem.Address,
                YearOfConstruction = properyItem.YearOfConstruction,
            };
        }

        // PUT: api/PropertyItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPropertyItem(long id, PropertyItem properyItem)
        {
            if (id != properyItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(properyItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyItemExists(id))
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

        // POST: api/PropertyItems
        [HttpPost]
        public async Task<ActionResult<PropertyItemResponseDto>> PostPropertyItem(PropertyItem properyItem)
        {
            _context.PropertyItems.Add(properyItem);
            await _context.SaveChangesAsync();

            return new PropertyItemResponseDto
            {
                OwnerId = properyItem.OwnerId,
                Id = properyItem.Id,
                Name = properyItem.Name,
                PropertyIdentificationNumber = properyItem.PropertyIdentificationNumber,
                Address = properyItem.Address,
                YearOfConstruction = properyItem.YearOfConstruction,
            };
        }

        // DELETE: api/PropertyItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyItem(long id)
        {
            var properyItem = await _context.PropertyItems.FindAsync(id);
            if (properyItem == null)
            {
                return NotFound();
            }

            _context.PropertyItems.Remove(properyItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PropertyItemExists(long id)
        {
            return _context.PropertyItems.Any(e => e.Id == id);
        }

        [HttpGet("owner/{id}")]
         public async Task<ActionResult<IEnumerable<PropertyItemResponseDto>>> GetPropertyItemsByOwnerId(long id)
                {
                    return await _context
                        .PropertyItems
                        .Where(properyItem => properyItem.OwnerId == id)
                        .Select(properyItem => new PropertyItemResponseDto
                        {
                            OwnerId = properyItem.OwnerId,
                            OwnerName = properyItem!.Owner!.Name,
                            Id = properyItem.Id,
                            Name = properyItem.Name,
                            PropertyIdentificationNumber = properyItem.PropertyIdentificationNumber,
                            Address = properyItem.Address,
                            YearOfConstruction = properyItem.YearOfConstruction,
                        })
                        .ToListAsync();
                }
            }
}  