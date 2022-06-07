using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.Shared.Domain.Models;
using RunningGo.API.Shared.Domain.Services;
using RunningGo.API.Shared.Extensions;
using RunningGo.API.Shared.Resources;

namespace RunningGo.API.Shared.Controllers;

[Route("/api/v1/[controller]")]
public class UsersController: ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IEnumerable<UserResource>> GetAll()
    {
        var users = await _userService.List();
        var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
        return resources;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var result = await _userService.FindById(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var userResource = _mapper.Map<User, UserResource>(result.Resource);
        return Ok(userResource);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveUserResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var user = _mapper.Map<SaveUserResource, User>(resource);
        var result = await _userService.Save(user);
        if (!result.Success)
            return BadRequest(result.Message);
        var userResource = _mapper.Map<User, UserResource>(result.Resource);
        return Ok(userResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, [FromBody] SaveUserResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var user = _mapper.Map<SaveUserResource, User>(resource);
        var result = await _userService.Update(id, user);
        if (!result.Success)
            return BadRequest(result.Message);

        var userResource = _mapper.Map<User, UserResource>(result.Resource);
        return Ok(userResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _userService.Delete(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var userResource = _mapper.Map<User, UserResource>(result.Resource);
        return Ok(userResource);
    }
}