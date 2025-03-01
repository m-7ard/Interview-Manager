using Application.Errors;

namespace Application.Utils;

public interface IApplicationResult<T>
{
    public T GetValue();
    public ApplicationError GetError();
    public bool IsSuccess();
    public bool IsError();
}