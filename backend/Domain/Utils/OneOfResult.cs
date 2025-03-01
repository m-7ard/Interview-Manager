using OneOf;

namespace Domain.Utils;

public class OneOfResult<T> : IResult<T>
{
    public OneOfResult(OneOf<T, string> oneOfInstance)
    {
        OneOfInstance = oneOfInstance;
    }

    public OneOf<T, string> OneOfInstance { get; }

    public string GetError()
    {
        return OneOfInstance.AsT1;
    }

    public T GetValue()
    {
        return OneOfInstance.AsT0;
    }

    public bool IsSuccess()
    {
        return OneOfInstance.IsT0;
    }
    
    public bool IsError()
    {
        return OneOfInstance.IsT1;
    }

    public static OneOfResult<T> AsError(string error)
    {
        return new(OneOf<T, string>.FromT1(error));
    }

    public static OneOfResult<T> AsSuccess(T value)
    {
        return new(OneOf<T, string>.FromT0(value));
    }
}