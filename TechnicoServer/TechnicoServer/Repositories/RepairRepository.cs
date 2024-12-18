

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
        throw new NotImplementedException();
    }

    public Task<Repair?> GetAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Repair?> UpdateAsync(Repair t)
    {
        throw new NotImplementedException();
    }
}
