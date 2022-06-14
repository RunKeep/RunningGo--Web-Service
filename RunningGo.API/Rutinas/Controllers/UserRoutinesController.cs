using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.Rutinas.Domain.Models;
using RunningGo.API.Rutinas.Domain.Services;
using RunningGo.API.Rutinas.Resources;

namespace RunningGo.API.Rutinas.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/routines")]
public class UserRoutinesController: ControllerBase
{
    private readonly IRoutineService _routineService;
    private readonly IMapper _mapper;
    
    public UserRoutinesController(IRoutineService routineService, IMapper mapper)
    {
        _routineService = routineService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<RoutineResource>> GetAllRoutinesByUserId(long userId)
    {
        var routines = await _routineService.ListByUserId(userId);
        var resources = _mapper.Map<IEnumerable<Routine>, IEnumerable<RoutineResource>>(routines);
        return resources;
    }
}