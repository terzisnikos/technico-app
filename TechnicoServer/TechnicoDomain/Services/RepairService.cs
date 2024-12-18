using TechnicoDomain.Dtos;
using TechnicoDomain.Models;
using TechnicoDomain.Repositories;

namespace TechnicoDomain.Services;

public class RepairService : IRepairService
{
    private readonly IRepository<Repair, long> _repairRepository;

    public RepairService(IRepository<Repair, long> repairRepository)
    {
        _repairRepository = repairRepository;
    }

    public async Task<RepairResponseDto?> CreateRepairAsync(RepairRequestDto repair)
    {
        var repairEntity = new Repair
        {
            Created = DateTime.Now,
            Type = repair.Type,
            PropertyItemId = repair.PropertyItemId,
            Description = repair.Description,
            Address = repair.Address,
            Cost = repair.Cost, 
            ScheduledDate = repair.ScheduledDate, 
            Status = repair.Status 
        };

        var repairdb = await _repairRepository.CreateAsync(repairEntity);
        if (repairdb == null) return null;

        return new RepairResponseDto
        {
            Id = repairdb.Id,
            Type = repairdb.Type,
            OwnerId = repairdb.PropertyItem?.OwnerId,
            OwnerName = repairdb.PropertyItem?.Owner?.Name,
            PropertyItemId = repairdb.PropertyItemId,
            PropertyItemTitle = repairdb.PropertyItem?.Name,
            Address = repairdb.Address,
            Cost = repairdb.Cost, 
            ScheduledDate = repairdb.ScheduledDate,
            Status = repairdb.Status, 
            Created = repairdb.Created, 
            Updated = repairdb.Updated 
        };
    }

    public async Task<bool> DeleteByIdAsync(long id)
    {
        return await _repairRepository.DeleteAsync(id);
    }

    public async Task<RepairResponseDto?> FindByIdAsync(long id)
    {
        var repair = await _repairRepository.GetAsync(id);
        if (repair == null) return null;

        return new RepairResponseDto
        {
            Id = repair.Id,
            Type = repair.Type,
            OwnerId = repair.PropertyItem?.OwnerId,
            OwnerName = repair.PropertyItem?.Owner?.Name,
            PropertyItemId = repair.PropertyItemId,
            PropertyItemTitle = repair.PropertyItem?.Name,
            Address = repair.Address,
            Cost = repair.Cost, 
            ScheduledDate = repair.ScheduledDate, 
            Status = repair.Status, 
            Created = repair.Created, 
            Updated = repair.Updated 
        };
    }

    public async Task<List<RepairResponseDto>> GetAllRepairAsync()
    {
        var repairs = await _repairRepository.GetAsync(1, 20); 
        return repairs.Select(item => new RepairResponseDto
        {
            Id = item.Id,
            Type = item.Type,
            OwnerId = item.PropertyItem?.Owner?.Id,
            OwnerName = item.PropertyItem?.Owner?.Name,
            PropertyItemId = item.PropertyItem?.Id,
            PropertyItemTitle = item.PropertyItem?.Name,
            Address = item.Address,
            Cost = item.Cost, 
            ScheduledDate = item.ScheduledDate, 
            Status = item.Status, 
            Created = item.Created, 
            Updated = item.Updated 
        }).ToList();
    }

    public async Task<RepairResponseDto?> UpdateRepairAsync(long id, RepairRequestDto repair)
    {
        var existingRepair = await _repairRepository.GetAsync(id);
        if (existingRepair == null) return null;

        existingRepair.Type = repair.Type;
        existingRepair.PropertyItemId = repair.PropertyItemId;
        existingRepair.Description = repair.Description;
        existingRepair.Address = repair.Address;
        existingRepair.Cost = repair.Cost; 
        existingRepair.ScheduledDate = repair.ScheduledDate; 
        existingRepair.Status = repair.Status; 
        existingRepair.Updated = DateTime.Now; 

        var updatedRepair = await _repairRepository.UpdateAsync(existingRepair);
        if (updatedRepair == null) return null;

        return new RepairResponseDto
        {
            Id = updatedRepair.Id,
            Type = updatedRepair.Type,
            OwnerId = updatedRepair.PropertyItem?.OwnerId,
            OwnerName = updatedRepair.PropertyItem?.Owner?.Name,
            PropertyItemId = updatedRepair.PropertyItemId,
            PropertyItemTitle = updatedRepair.PropertyItem?.Name,
            Address = updatedRepair.Address,
            Cost = updatedRepair.Cost, 
            ScheduledDate = updatedRepair.ScheduledDate, 
            Status = updatedRepair.Status, 
            Created = updatedRepair.Created, 
            Updated = updatedRepair.Updated 
        };
    }

    public async Task<List<RepairResponseDto>> GetRepairByOwnerId(long ownerId)
    {
        var repairs = await _repairRepository.GetAsync(1, 100); 
        var filteredRepairs = repairs.Where(r => r.PropertyItem?.OwnerId == ownerId).ToList();

        return filteredRepairs.Select(item => new RepairResponseDto
        {
            Id = item.Id,
            Type = item.Type,
            OwnerId = item.PropertyItem?.Owner?.Id,
            OwnerName = item.PropertyItem?.Owner?.Name,
            PropertyItemId = item.PropertyItem?.Id,
            PropertyItemTitle = item.PropertyItem?.Name,
            Address = item.Address,
            Cost = item.Cost, 
            ScheduledDate = item.ScheduledDate, 
            Status = item.Status, 
            Created = item.Created, 
            Updated = item.Updated 
        }).ToList();
    }

}
