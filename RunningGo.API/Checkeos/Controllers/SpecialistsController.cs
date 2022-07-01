using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Domain.Services;
using RunningGo.API.Checkeos.Resources;
using RunningGo.API.Shared.Extensions;

namespace RunningGo.API.Checkeos.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class SpecialistsController : ControllerBase
{
    private readonly ISpecialistService _specialistService;
    private readonly IMapper _mapper;

    public SpecialistsController(ISpecialistService specialistService, IMapper mapper)
    {
        _specialistService = specialistService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<SpecialistResource>> GetAll()
    {
        var specialists = await _specialistService.List();
        var resources = _mapper.Map<IEnumerable<Specialist>, IEnumerable<SpecialistResource>>(specialists);
        return resources;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _specialistService.FindById(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var specialistResource = _mapper.Map<Specialist, SpecialistResource>(result.Resource);
        return Ok(specialistResource);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveSpecialistResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var specialist = _mapper.Map<SaveSpecialistResource, Specialist>(resource);
        var result = await _specialistService.Save(specialist);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var specialistResource = _mapper.Map<Specialist, SpecialistResource>(result.Resource);
        return Created(nameof(Create), specialistResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] SaveSpecialistResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var specialist = _mapper.Map<SaveSpecialistResource, Specialist>(resource);
        var result = await _specialistService.Update(id, specialist);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var specialistResource = _mapper.Map<Specialist, SpecialistResource>(result.Resource);
        return Ok(specialistResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _specialistService.Delete(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var specialistResource = _mapper.Map<Specialist, SpecialistResource>(result.Resource);
        return Ok(specialistResource); 
    }
}