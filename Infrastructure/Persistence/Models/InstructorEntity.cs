using System.Security;

namespace Infrastructure.Persistence.Entities;

public sealed class InstructorEntity
{
    public string Id { get; set; } = null!;
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime ModifiedAt { get; set; }
    public byte[] RowVersion { get; set; } = null!;

    public int InstructorRoleId { get; set; }
    public InstructorRoleEntity InstructorRole { get; set; } = null!;
}

public sealed class InstructorRoleEntity
{
    public int Id { get; set; }
    public required string RoleName { get; set; }

    public ICollection<InstructorEntity> Instructors { get; set; } = [];
}