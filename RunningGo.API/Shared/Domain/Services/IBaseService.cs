namespace RunningGo.API.Shared.Domain.Services;

public interface IBaseService<M, R, I>
{
    Task<IEnumerable<M>> List();
    Task<R> Save(M model);
    Task<R> Update(I id, M model);
    Task<R> Delete(I id);
}