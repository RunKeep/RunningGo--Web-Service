using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Dietas.Domain.Services;
using RunningGo.API.Dietas.Resources;

namespace RunningGo.API.Dietas.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/diets")]
public class UserDietsController: ControllerBase
{
    private readonly IDietService _dietService;
    private readonly IMapper _mapper;

    public UserDietsController(IDietService dietService, IMapper mapper)
    {
        _dietService = dietService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<DietResource>> GetAllDietsByUserId(long userId)
    {
        var diets = await _dietService.ListByUserId(userId);
        var resources = _mapper.Map<IEnumerable<Diet>, IEnumerable<DietResource>>(diets);
        return resources;
    }
}