using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.Rutinas.Domain.Models;
using RunningGo.API.Rutinas.Domain.Services;
using RunningGo.API.Rutinas.Resources;
using RunningGo.API.Shared.Extensions;

namespace RunningGo.API.Rutinas.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class HabitsController: ControllerBase
{
    private readonly IHabitService _habitService;
    private readonly IMapper _mapper;


    public HabitsController(IHabitService habitService, IMapper mapper)
    {
        _habitService = habitService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<HabitResource>> GetAll()
    {
        var habits = await _habitService.List();
        var resources = _mapper.Map<IEnumerable<Habit>, IEnumerable<HabitResource>>(habits);
        return resources;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var result = await _habitService.FindById(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var habitResource = _mapper.Map<Habit, HabitResource>(result.Resource);
        return Ok(habitResource);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveHabitResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var habit = _mapper.Map<SaveHabitResource, Habit>(resource);
        var result = await _habitService.Save(habit);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var habitResource = _mapper.Map<Habit, HabitResource>(result.Resource);
        return Created(nameof(Create), habitResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SaveHabitResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var habit = _mapper.Map<SaveHabitResource, Habit>(resource);
        var result = await _habitService.Update(id, habit);
        if (!result.Success)
            return BadRequest(result.Message);

        var habitResource = _mapper.Map<Habit, HabitResource>(result.Resource);
        return Ok(habitResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _habitService.Delete(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var habitResource = _mapper.Map<Habit, HabitResource>(result.Resource);
        return Ok(habitResource);
    }
}