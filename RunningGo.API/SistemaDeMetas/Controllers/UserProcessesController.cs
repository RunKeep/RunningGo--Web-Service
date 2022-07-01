using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.SistemaDeMetas.Domain.Models;
using RunningGo.API.SistemaDeMetas.Domain.Services;
using RunningGo.API.SistemaDeMetas.Resources;

namespace RunningGo.API.SistemaDeMetas.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/processes")]
public class UserProcessesController: ControllerBase
{
    private readonly IProcessService _processService;
    private readonly IMapper _mapper;

    public UserProcessesController(IProcessService processService, IMapper mapper)
    {
        _processService = processService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ProcessResource>> GetAllProcessesByUserId(long userId)
    {
        var processes = await _processService.ListByUserId(userId);
        var resources = _mapper.Map<IEnumerable<Process>, IEnumerable<ProcessResource>>(processes);
        return resources;
    }
}