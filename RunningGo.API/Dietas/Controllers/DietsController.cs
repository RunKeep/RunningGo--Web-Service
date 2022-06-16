using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Dietas.Domain.Services;
using RunningGo.API.Dietas.Resources;
using RunningGo.API.Shared.Extensions;

namespace RunningGo.API.Dietas.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class DietsController : ControllerBase
{
    private readonly IDietService _dietService;
    private readonly IMapper _mapper;

    public DietsController(IDietService dietService, IMapper mapper)
    {
        _dietService = dietService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<DietResource>> GetAll()
    {
        var categories = await _dietService.List();
        var resources = _mapper.Map<IEnumerable<Diet>, IEnumerable<DietResource>>(categories);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveDietResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var diet = _mapper.Map<SaveDietResource, Diet>(resource);
        var result = await _dietService.Save(diet);
        if (!result.Success)
            return BadRequest(result.Message);
        var dietResource = _mapper.Map<Diet, DietResource>(result.Resource);
        return Created(nameof(Create), dietResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SaveDietResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var diet = _mapper.Map<SaveDietResource, Diet>(resource);
        var result = await _dietService.Update(id, diet);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var dietResource = _mapper.Map<Diet, DietResource>(result.Resource);
        return Ok(dietResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _dietService.Delete(id);
        if (!result.Success)
            return BadRequest(result.Message);

        var dietResource = _mapper.Map<Diet, DietResource>(result.Resource);
        return Ok(dietResource);
    }
}