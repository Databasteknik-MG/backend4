using Domain.Instructors;

namespace Presentation.API.Models.Instructors;

public sealed record CreateInstructorRequest
{
    public required string FirstName { get; init; } = null!;
    public required string LastName { get; init; } = null!;
    public required string Email { get; init; } = null!;
    public string? PhoneNumber { get; init; }
    public required int RoleId { get; init; }
}


public sealed record InstructorResponse
(
    bool Success,
    int StatusCode,
    string Message,
    Instructor Value
);