using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Domain.Services;
using RunningGo.API.Checkeos.Resources;

namespace RunningGo.API.Checkeos.Controllers;

[ApiController]
[Route("/api/v1/specialists/{specialistId}/checkups")]
public class SpecialistCheckupsController: ControllerBase
{
    private readonly ICheckupService _checkupService;
    private readonly IMapper _mapper;

    public SpecialistCheckupsController(ICheckupService checkupService, IMapper mapper)
    {
        _checkupService = checkupService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<CheckupResource>> GetAllCheckupsBySpecialistId(long specialistId)
    {
        var checkups = await _checkupService.ListBySpecialistId(specialistId);
        var resources = _mapper.Map<IEnumerable<Checkup>, IEnumerable<CheckupResource>>(checkups);
        return resources;
    }
}