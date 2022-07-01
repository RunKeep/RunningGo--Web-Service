using RunningGo.API.Checkeos.Domain.Models;
using RunningGo.API.Checkeos.Domain.Services.Communication;
using RunningGo.API.Shared.Domain.Services;

namespace RunningGo.API.Checkeos.Domain.Services;

public interface IArrangeService: IBaseService<Arrange, ArrangeResponse, int>
{
    Task<ArrangeResponse> FindById(int id);
}