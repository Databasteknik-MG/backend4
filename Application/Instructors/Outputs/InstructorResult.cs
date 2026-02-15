using Application.Common.Models.Results;

namespace Application.Instructors.Outputs;

public sealed record InstructorResult(bool Success, int StatusCode, string? Message) : ResultBase(Success, StatusCode, Message)
{
    public static InstructorResult Ok(string? message = null) => new(true, 200, message);
    public static InstructorResult Created(string? message = null) => new(true, 201, message);
    public static InstructorResult NotFound(string? message = null) => new(false, 404, message);
    public static InstructorResult Conflict(string? message = null) => new(false, 409, message);
    public static InstructorResult BadRequest(string? message = null) => new(false, 400, message);
    public static InstructorResult Error(string? message = null) => new(false, 500, message);
}

public sealed record InstructorResult<T>(bool Success, int StatusCode, string? Message, T? Value) : ResultBase<T>(Success, StatusCode, Message, Value)
{
    public static InstructorResult<T> Ok(T value, string? message = null) => new(true, 200, message, value);
    public static InstructorResult<T> Created(T value, string? message = null) => new(true, 201, message, value);
    public static InstructorResult<T> NotFound(string? message = null) => new(false, 404, message, default);
    public static InstructorResult<T> Conflict(string? message = null) => new(false, 409, message, default);
    public static InstructorResult<T> BadRequest(string? message = null) => new(false, 400, message, default);
    public static InstructorResult<T> Error(string? message = null) => new(false, 500, message, default);
}