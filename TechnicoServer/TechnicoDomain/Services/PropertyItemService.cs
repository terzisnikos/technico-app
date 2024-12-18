using TechnicoDomain.Dtos;
using TechnicoDomain.Models;
using TechnicoDomain.Repositories;

namespace TechnicoDomain.Services;

public class PropertyItemService : IPropertyItemService
{
    private readonly IRepository<PropertyItem, long> _repairRepository;

    public PropertyItemService(IRepository<PropertyItem, long> repairRepository)
    {
        _repairRepository = repairRepository;
    }

    public async Task<PropertyItemResponseDto?> CreatePropertyItemAsync(PropertyItemRequestDto repair)
    {
        var repairEntity = new PropertyItem
        {
            Name = repair.Name,
        };

        var repairdb = await _repairRepository.CreateAsync(repairEntity);
        if (repairdb == null) return null;

        return new PropertyItemResponseDto
        {
            Id = repairdb.Id,
            Name = repairdb.Name,
        };
    }

    public async Task<bool> DeleteByIdAsync(long id)
    {
        return await _repairRepository.DeleteAsync(id);
    }

    public async Task<PropertyItemResponseDto?> FindByIdAsync(long id)
    {
        var repair = await _repairRepository.GetAsync(id);
        if (repair == null) return null;

        return new PropertyItemResponseDto
        {
            Id = repair.Id,
            Name = repair.Name,
        };
    }

    public async Task<List<PropertyItemResponseDto>> GetAllPropertyItemAsync()
    {
        var repairs = await _repairRepository.GetAsync(1, 20); 
        return repairs.Select(item => new PropertyItemResponseDto
        {
            Id = item.Id,
            Name = item.Name,
        }).ToList();
    }

    public async Task<PropertyItemResponseDto?> UpdatePropertyItemAsync(long id, PropertyItemRequestDto repair)
    {
        var existingPropertyItem = await _repairRepository.GetAsync(id);
        if (existingPropertyItem == null) return null;

        existingPropertyItem.Name = repair.Name;

        var updatedPropertyItem = await _repairRepository.UpdateAsync(existingPropertyItem);
        if (updatedPropertyItem == null) return null;

        return new PropertyItemResponseDto
        {
            Id = updatedPropertyItem.Id,
            Name = updatedPropertyItem.Name,
            OwnerId = updatedPropertyItem.OwnerId,
            OwnerName = updatedPropertyItem.Owner?.Name
        };
    }

}
