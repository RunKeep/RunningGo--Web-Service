﻿using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Dietas.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Services;

namespace RunningGo.API.Dietas.Domain.Services;

public interface IDietService: IBaseService<Diet, DietResponse, int>
{
    public Task<IEnumerable<Diet>> ListByUserId(long userId);
}