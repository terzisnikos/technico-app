using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechnicoDomain.Context;
using TechnicoDomain.Models;
using TechnicoDomain.Repositories;

namespace TechnicoApi.Repositories
{
    public class PropertyItemRepository : IRepository<PropertyItem, long>
    {
        private readonly TechnicoDbContext _context;

        public PropertyItemRepository(TechnicoDbContext context)
        {
            _context = context;
        }

        public async Task<List<PropertyItem>> GetAsync(int pageCount, int pageSize)
        {
            return await _context.PropertyItems
                .Include(item => item.Owner) 
                .Skip((pageCount - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<PropertyItem?> CreateAsync(PropertyItem propertyItem)
        {
            _context.PropertyItems.Add(propertyItem);
            await _context.SaveChangesAsync();
            return propertyItem;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var propertyItem = await _context.PropertyItems.FindAsync(id);
            if (propertyItem == null)
            {
                return false;
            }

            _context.PropertyItems.Remove(propertyItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PropertyItem?> GetAsync(long id)
        {
            return await _context.PropertyItems
                .Include(item => item.Owner) 
                .FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<PropertyItem?> UpdateAsync(PropertyItem propertyItem)
        {
            _context.Entry(propertyItem).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return propertyItem;
            }
            catch (DbUpdateConcurrencyException)
            {
                return null; 
            }
        }

        public async Task FindByIdAsync(long id)
        {
            await _context.PropertyItems.FindAsync(id);
        }


        public async Task<IEnumerable<object>> GetByOwnerIdAsync(long ownerId)
        {
            return await _context.PropertyItems
                .Where(item => item.OwnerId == ownerId)
                .Include(item => item.Owner) 
                .Select(item => (object)item) 
                .ToListAsync();
        }

    }
}
