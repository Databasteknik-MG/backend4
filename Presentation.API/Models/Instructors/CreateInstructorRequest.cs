using Domain.Instructors;

namespace Presentation.API.Models.Instructors;

public sealed record CreateInstructorRequest
(
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber
);

public sealed record InstructorResponse
(
    bool Success,
    int StatusCode,
    string Message,
    Instructor Value
);