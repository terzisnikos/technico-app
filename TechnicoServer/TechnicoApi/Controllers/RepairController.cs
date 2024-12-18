using TechnicoDomain.Dtos;
using TechnicoDomain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static TechnicoDomain.Dtos.PropertyItemResponseDto;

namespace TechnicoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RepairController : ControllerBase
{
    private readonly ILogger<RepairController> _logger;
    private readonly IRepairService _service;


    public RepairController(ILogger<RepairController> logger, IRepairService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<RepairResponseDto>>> GetRepairsAsync()
    {
        return await _service.GetAllRepairAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<RepairResponseDto?>> GetRepairsAsync(long id)
    {
        return await _service.FindByIdAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult<RepairResponseDto?>> CreateRepairAsync(RepairRequestDto repair)
    {
        return await _service.CreateRepairAsync(repair);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RepairResponseDto?>> UpdateRepair(long id, RepairRequestDto repair)
    {
        return await _service.UpdateRepairAsync(id, repair);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteRepairAsync(long id)
    {
        return await _service.DeleteByIdAsync(id);
    }

    //Find properties by ownerId
    [HttpGet("owner/{id}")]
    public async Task<ActionResult<IEnumerable<RepairResponseDto>>> GetRepairByOwnerId(long id)
    {
        // get all properties by owner id
        return await _service.GetRepairByOwnerId(id);
    }

}