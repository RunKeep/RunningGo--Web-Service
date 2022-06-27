using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.Shared.Extensions;
using RunningGo.API.SistemaDeMetas.Domain.Models;
using RunningGo.API.SistemaDeMetas.Domain.Services;
using RunningGo.API.SistemaDeMetas.Resources;

namespace RunningGo.API.SistemaDeMetas.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ProcessesController: ControllerBase
{
    private readonly IProcessService _processService;
    private readonly IMapper _mapper;

    public ProcessesController(IProcessService processService, IMapper mapper)
    {
        _processService = processService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ProcessResource>> GetAll()
    {
        var processes = await _processService.List();
        var resources = _mapper.Map<IEnumerable<Process>, IEnumerable<ProcessResource>>(processes);
        return resources;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var result = await _processService.FindById(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var processResource = _mapper.Map<Process, ProcessResource>(result.Resource);
        return Ok(processResource);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveProcessResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var process = _mapper.Map<SaveProcessResource, Process>(resource);
        var result = await _processService.Save(process);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var processResource = _mapper.Map<Process, ProcessResource>(result.Resource);
        return Created(nameof(Create), processResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SaveProcessResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var process = _mapper.Map<SaveProcessResource, Process>(resource);
        var result = await _processService.Update(id, process);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var processResource = _mapper.Map<Process, ProcessResource>(result.Resource);
        return Ok(processResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _processService.Delete(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var processResource = _mapper.Map<Process, ProcessResource>(result.Resource);
        return Ok(processResource);
    }
}