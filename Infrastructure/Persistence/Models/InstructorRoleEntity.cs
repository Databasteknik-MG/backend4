namespace Infrastructure.Persistence.Models;

public sealed class InstructorRoleEntity
{
    public int Id { get; set; }
    public required string RoleName { get; set; }

    public ICollection<InstructorEntity> Instructors { get; set; } = [];
}