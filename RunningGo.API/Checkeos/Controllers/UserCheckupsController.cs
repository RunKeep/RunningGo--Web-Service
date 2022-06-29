using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Domain.Services;
using RunningGo.API.Checkeos.Resources;

namespace RunningGo.API.Checkeos.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/checkups")]
public class UserCheckupsController: ControllerBase
{
    private readonly ICheckupService _checkupService;
    private readonly IMapper _mapper;
    
    public UserCheckupsController(ICheckupService checkupService, IMapper mapper)
    {
        _checkupService = checkupService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CheckupResource>> GetAllCheckupsByUserId(long userId)
    {
        var checkups = await _checkupService.ListByUserId(userId);
        var resources = _mapper.Map<IEnumerable<Checkup>, IEnumerable<CheckupResource>>(checkups);
        return resources;
    }
}