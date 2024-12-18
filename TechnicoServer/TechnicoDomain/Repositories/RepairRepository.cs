

using TechnicoDomain.Models;
using TechnicoDomain.Context;
using Microsoft.EntityFrameworkCore;

namespace TechnicoDomain.Repositories;

public class RepairRepository : IRepository<Repair, long>
{
    private readonly TechnicoDbContext _dbContext;

    public RepairRepository(TechnicoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Repair>> GetAsync(int pageCount, int pageSize)
    {
        return await _dbContext
            .Repairs
            .Include(repair => repair.PropertyItem!.Owner )
            .ToListAsync();
    }

    public async Task<Repair?> CreateAsync(Repair t)
    {
        _dbContext.Repairs.Add(t);
        await _dbContext.SaveChangesAsync();
        return t; 
    }

    public Task<bool> DeleteAsync(long id)
    {
        var repair = _dbContext.Repairs.Find(id);
        if (repair == null)
        {
            return Task.FromResult(false);
        }
        _dbContext.Repairs.Remove(repair);
        _dbContext.SaveChanges();
        return Task.FromResult(true);
    }

    public Task<Repair?> GetAsync(long id)
    {
        var repair = _dbContext.Repairs.Find(id);
        return Task.FromResult(repair);
    }

    public async Task<Repair?> UpdateAsync(Repair t)
    {
        _dbContext.Repairs.Update(t);
        await _dbContext.SaveChangesAsync();
        return t;
    }

    public Task FindByIdAsync(long id)
    {
        var repair = _dbContext.Repairs.Find(id);
        return Task.FromResult(repair);
    }

    public async Task<IEnumerable<object>> GetByOwnerIdAsync(long ownerId)
    {
        return await _dbContext
            .Repairs
            .Where(repair => repair.PropertyItem!.OwnerId == ownerId)
            .Include(repair => repair.PropertyItem!.Owner)
            .ToListAsync();
    }
}
