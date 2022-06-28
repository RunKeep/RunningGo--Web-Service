using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Domain.Services;
using RunningGo.API.Checkeos.Resources;
using RunningGo.API.Shared.Extensions;

namespace RunningGo.API.Checkeos.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class CheckupsController: ControllerBase
{
    private readonly ICheckupService _checkupService;
    private readonly IMapper _mapper;

    public CheckupsController(ICheckupService checkupService, IMapper mapper)
    {
        _checkupService = checkupService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CheckupResource>> GetAll()
    {
        var checkups = await _checkupService.List();
        var resources = _mapper.Map<IEnumerable<Checkup>, IEnumerable<CheckupResource>>(checkups);
        return resources;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _checkupService.FindById(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var checkupResource = _mapper.Map<Checkup, CheckupResource>(result.Resource);
        return Ok(checkupResource);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveCheckupResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var checkup = _mapper.Map<SaveCheckupResource, Checkup>(resource);
        var result = await _checkupService.Save(checkup);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var checkupResource = _mapper.Map<Checkup, CheckupResource>(result.Resource);
        return Created(nameof(Create), checkupResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SaveCheckupResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var checkup = _mapper.Map<SaveCheckupResource, Checkup>(resource);
        var result = await _checkupService.Update(id, checkup);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var checkupResource = _mapper.Map<Checkup, CheckupResource>(result.Resource);

        return Ok(checkupResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _checkupService.Delete(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        var checkupResource = _mapper.Map<Checkup, CheckupResource>(result.Resource);
        return Ok(checkupResource);
        
    }
}