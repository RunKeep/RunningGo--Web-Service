using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.Rutinas.Domain.Models;
using RunningGo.API.Rutinas.Domain.Services;
using RunningGo.API.Rutinas.Resources;
using RunningGo.API.Shared.Extensions;

namespace RunningGo.API.Rutinas.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class RoutinesController: ControllerBase
{
    private readonly IRoutineService _routineService;
    private readonly IMapper _mapper;

    public RoutinesController(IRoutineService routineService, IMapper mapper)
    {
        _routineService = routineService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<RoutineResource>> GetAll()
    {
        var routines = await _routineService.List();
        var resources = _mapper.Map<IEnumerable<Routine>, IEnumerable<RoutineResource>>(routines);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveRoutineResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var routine = _mapper.Map<SaveRoutineResource, Routine>(resource);
        var result = await _routineService.Save(routine);
        if (!result.Success)
            return BadRequest(result.Message);

        var routineResource = _mapper.Map<Routine, RoutineResource>(result.Resource);
        return Created(nameof(Create), routineResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] SaveRoutineResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var routine = _mapper.Map<SaveRoutineResource, Routine>(resource);
        var result = await _routineService.Update(id, routine);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var routineResource = _mapper.Map<Routine, RoutineResource>(result.Resource);
        return Ok(routineResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _routineService.Delete(id);
        if (!result.Success)
            return BadRequest(result.Message);

        var routineResource = _mapper.Map<Routine, RoutineResource>(result.Resource);
        return Ok(routineResource);
    }
}