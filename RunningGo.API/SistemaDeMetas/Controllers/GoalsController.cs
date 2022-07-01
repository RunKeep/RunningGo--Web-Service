using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.Shared.Extensions;
using RunningGo.API.SistemaDeMetas.Domain.Models;
using RunningGo.API.SistemaDeMetas.Domain.Services;
using RunningGo.API.SistemaDeMetas.Resources;

namespace RunningGo.API.SistemaDeMetas.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class GoalsController : ControllerBase
{
    private readonly IGoalService _goalService;
    private readonly IMapper _mapper;
    
    public GoalsController(IGoalService goalService, IMapper mapper)
    {
        _goalService = goalService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<GoalResource>> GetAll()
    {
        var goals = await _goalService.List();
        var resources = _mapper.Map<IEnumerable<Goal>, IEnumerable<GoalResource>>(goals);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveGoalResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var goal = _mapper.Map<SaveGoalResource, Goal>(resource);
        var result = await _goalService.Save(goal);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var goalResource = _mapper.Map<Goal, GoalResource>(result.Resource);
        return Created(nameof(Create), goalResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SaveGoalResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var goal = _mapper.Map<SaveGoalResource, Goal>(resource);
        var result = await _goalService.Update(id, goal);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var goalResource = _mapper.Map<Goal, GoalResource>(result.Resource);
        return Ok(goalResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _goalService.Delete(id);
        if (!result.Success)
            return BadRequest(result.Message);
        
        var goalResource = _mapper.Map<Goal, GoalResource>(result.Resource);
        return Ok(goalResource);
    }
}