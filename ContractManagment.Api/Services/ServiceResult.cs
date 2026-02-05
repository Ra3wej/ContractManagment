namespace ContractManagment.Api.Services;

public class ServiceResult<T>
{
    public bool IsSuccess { get; }
    public string? Message { get; }
    public T? Data { get; }

    private ServiceResult(bool isSuccess, T? data, string message)
    {
        IsSuccess = isSuccess;
        Data = data;
        Message = message;
    }

    public static ServiceResult<T> Success(T data, string message = "")
        => new(true, data, message);

    public static ServiceResult<T> Failure(string message)
        => new(false, default, message);
}
