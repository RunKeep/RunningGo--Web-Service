using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Domain.Services;
using RunningGo.API.Checkeos.Resources;
using RunningGo.API.Shared.Extensions;

namespace RunningGo.API.Checkeos.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ArrangesController: ControllerBase
{
    private readonly IArrangeService _arrangeService;
    private readonly IMapper _mapper;

    public ArrangesController(IArrangeService arrangeService, IMapper mapper)
    {
        _arrangeService = arrangeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ArrangeResource>> GetAll()
    {
        var arranges = await _arrangeService.List();
        var resources = _mapper.Map<IEnumerable<Arrange>, IEnumerable<ArrangeResource>>(arranges);
        return resources;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _arrangeService.FindById(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var arrangeResource = _mapper.Map<Arrange, Arrange>(result.Resource);
        return Ok(arrangeResource);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveArrangeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var arrange = _mapper.Map<SaveArrangeResource, Arrange>(resource);
        var result = await _arrangeService.Save(arrange);
        if (!result.Success)
            return BadRequest(result.Message);

        var arrangeResource = _mapper.Map<Arrange, ArrangeResource>(result.Resource);
        return Created(nameof(Create), arrangeResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SaveArrangeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var arrange = _mapper.Map<SaveArrangeResource, Arrange>(resource);
        var result = await _arrangeService.Update(id, arrange);
        if (!result.Success)
            return BadRequest(result.Message);

        var arrangeResource = _mapper.Map<Arrange, ArrangeResource>(result.Resource);
        return Ok(arrangeResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _arrangeService.Delete(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var arrangeResource = _mapper.Map<Arrange, ArrangeResource>(result.Resource);
        return Ok(arrangeResource);
    }
}