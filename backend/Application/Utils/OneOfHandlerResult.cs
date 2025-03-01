using Application.Errors;
using OneOf;

namespace Application.Utils;

public class OneOfHandlerResult<T>
{
    private readonly OneOf<T, List<ApplicationError>> _result;

    private OneOfHandlerResult(OneOf<T, List<ApplicationError>> result)
    {
        _result = result;
    }

    public T GetValue() => _result.AsT0;
    public List<ApplicationError> GetError() => _result.AsT1;
    public bool IsSuccess() => _result.IsT0;
    public bool IsError() => _result.IsT1;

    public static implicit operator OneOfHandlerResult<T>(T value) => new(OneOf<T, List<ApplicationError>>.FromT0(value));

    public static implicit operator OneOfHandlerResult<T>(List<ApplicationError> errors) => new(OneOf<T, List<ApplicationError>>.FromT1(errors));

}