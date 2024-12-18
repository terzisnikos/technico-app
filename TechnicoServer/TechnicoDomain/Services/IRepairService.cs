using TechnicoDomain.Dtos;
using TechnicoDomain.Models;

namespace TechnicoDomain.Services;

public interface IRepairService
{
    Task<List<RepairResponseDto>> GetAllRepairAsync();
    Task<RepairResponseDto?> FindByIdAsync(long id);

    Task<RepairResponseDto?> CreateRepairAsync(RepairRequestDto repair);
    Task<RepairResponseDto?> UpdateRepairAsync(long id, RepairRequestDto repair);
    Task<bool> DeleteByIdAsync(long id);
    Task<List<RepairResponseDto>> GetRepairByOwnerId(long id);
}