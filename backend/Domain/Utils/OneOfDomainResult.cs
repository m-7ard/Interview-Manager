using OneOf;

namespace Domain.Utils;

public class OneOfDomainResult<T>
{
    private readonly OneOf<T, string> _result;

    private OneOfDomainResult(OneOf<T, string> result)
    {
        _result = result;
    }

    public T GetValue() => _result.AsT0;
    public string GetError() => _result.AsT1;
    public bool IsSuccess() => _result.IsT0;
    public bool IsError() => _result.IsT1;

    public static implicit operator OneOfDomainResult<T>(T value) => new(OneOf<T, string>.FromT0(value));

    public static implicit operator OneOfDomainResult<T>(string error) => new(OneOf<T, string>.FromT1(error));

}