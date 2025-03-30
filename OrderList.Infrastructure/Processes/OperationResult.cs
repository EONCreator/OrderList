namespace OrderList.Infrastructure.Results;

public class OperationResult
{
    public bool Success { get; }
    public string? Message { get; }

    private OperationResult(bool success, string? message = null)
    {
        Success = success;
        Message = message;
    }

    public static OperationResult Succeeded => new(true);
    public static OperationResult Failure(string message) => new(false, message);
}