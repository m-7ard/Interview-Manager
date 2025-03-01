namespace Domain.Utils;

public interface IResult<T>
{
    public T GetValue();
    public string GetError();
    public bool IsSuccess();
    public bool IsError();
}