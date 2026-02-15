namespace Application.Common.Models.Results;

public abstract record ResultBase(
    bool Success,
    int StatusCode,
    string? Message
);

public abstract record ResultBase<T>(
    bool Success,
    int StatusCode,
    string? Message,
    T? Value
) : ResultBase(Success, StatusCode, Message);