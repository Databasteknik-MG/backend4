using Application.Common.Results;
using Application.Instructors.Contracts;
using Application.Instructors.Inputs;
using Domain.Instructors;
using Domain.Instructors.Repositories;

namespace Application.Instructors.Services;

public sealed class InstructorRoleService(IInstructorRoleRepository roleRepository) : IInstructorRoleService
{
    public async Task<Result<InstructorRole?>> CreateInstructorRoleAsync(string roleName, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return Result<InstructorRole?>.BadRequest("Role name cannot be empty.");

        var existingRole = await roleRepository.GetByRoleNameAsync(roleName, ct);
        if(existingRole is not null)
            return Result<InstructorRole?>.Conflict($"A role with the name '{roleName}' already exists.");

        var newRole = new InstructorRole
        (
            roleName
        );

        var created = await roleRepository.AddAsync(newRole, ct);
        return created is null
            ? Result<InstructorRole?>.Error("Failed to create the instructor role.")
            : Result<InstructorRole?>.Ok(created);
    }

    public async Task<Result> DeleteInstructorRoleAsync(int id, CancellationToken ct)
    {
        if (id < 1)
            return Result.BadRequest("Invalid role ID.");

        var deleted = await roleRepository.RemoveAsync(id, ct);
        return !deleted
            ? Result.Error("Failed to delete the instructor role.")
            : Result.Ok();
    }

    public async Task<Result<IReadOnlyList<InstructorRole>>> GetInstructorRolesAsync(CancellationToken ct)
    {
        var roles = await roleRepository.GetAllAsync(ct);
        return Result<IReadOnlyList<InstructorRole>>.Ok(roles);
    }
}
