namespace Presentation.API.Models.Instructors;

public class CreateInstructorRoleRequest
{
    public required string RoleName { get; init; } = null!;
}
