
using TechnicoDomain.Dtos;
using TechnicoDomain.Models;
using TechnicoDomain.Repositories;

namespace TechnicoDomain.Services;

/// <summary>
/// 
/// </summary>
public class RepairService : IRepairService
{
    private readonly IRepository<Repair, long> _repairRepository;

    public RepairService(IRepository<Repair, long> repairRepository)
    {
        _repairRepository = repairRepository;
    }

    public async Task<RepairResponseDto?> CreateRepairAsync(RepairRequestDto repair)
    {
         Repair? repairdb =     await _repairRepository.CreateAsync(new Repair { 
             Created=DateTime.Now,
             Type = repair.Type,
             PropertyItemId = repair.PropertyItemId,
             Description=repair.Description ,
         });
        return new RepairResponseDto { 
            Id=repairdb?.Id,
            Type = repairdb?.Type,
            OwnerId=repairdb?.PropertyItem?.OwnerId,
            OwnerName=repairdb?.PropertyItem?.Owner?.Name,
            ProperyItemId = repairdb?.PropertyItemId,
            ProperyItemTitle = repairdb?.PropertyItem?.Name,
        };
    }

    public Task<bool> DeleteByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<RepairResponseDto?> FindByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<RepairResponseDto>> GetAllRepairAsync()
    {
        List<Repair> repairs = await _repairRepository.GetAsync(1, 20);
        return repairs.Select(item => new RepairResponseDto { 
            OwnerId=item?.PropertyItem?.Owner?.Id, 
            Id=item?.Id, 
            Type = item?.Type,
            OwnerName=item?.PropertyItem?.Owner?.Name,
            ProperyItemId=item?.PropertyItem?.Id,
            ProperyItemTitle = item?.PropertyItem?.Name
        }).ToList();
    }

    public Task<RepairResponseDto?> UpdateRepairAsync(long id, RepairRequestDto repair)
    {
        throw new NotImplementedException();
    }
}
