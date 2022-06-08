using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Dietas.Domain.Services;
using RunningGo.API.Dietas.Resources;
using RunningGo.API.Shared.Extensions;

namespace RunningGo.API.Dietas.Controllers;

[Route("/api/v1/[controller]")]
public class FoodsController: ControllerBase
{
    private readonly IFoodService _foodService;
    private readonly IMapper _mapper;

    public FoodsController(IMapper mapper, IFoodService foodService)
    {
        _mapper = mapper;
        _foodService = foodService;
    }

    [HttpGet]
    public async Task<IEnumerable<FoodResource>> GetAll()
    {
        var foods = await _foodService.List();
        var resources = _mapper.Map<IEnumerable<Food>, IEnumerable<FoodResource>>(foods);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SaveFoodResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var food = _mapper.Map<SaveFoodResource, Food>(resource);
        var result = await _foodService.Save(food);
        if (!result.Success)
            return BadRequest(result.Message);
        var foodResource = _mapper.Map<Food, FoodResource>(result.Resource);
        return Created(nameof(Create), foodResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SaveFoodResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var food = _mapper.Map<SaveFoodResource, Food>(resource);
        var result = await _foodService.Update(id, food);
        if (!result.Success)
            return BadRequest(result.Message);

        var foodResource = _mapper.Map<Food, FoodResource>(result.Resource);
        return Ok(foodResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _foodService.Delete(id);
        if (!result.Success)
            return BadRequest(result.Message);
        var foodResource = _mapper.Map<Food, FoodResource>(result.Resource);
        return Ok(foodResource);
    }
}