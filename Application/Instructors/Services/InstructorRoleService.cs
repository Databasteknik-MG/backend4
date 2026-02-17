using Application.Common.Results;
using Application.Instructors.Contracts;
using Application.Instructors.Inputs;
using Domain.Instructors;
using Domain.Instructors.Repositories;

namespace Application.Instructors.Services;

public sealed class InstructorRoleService(IInstructorRoleRepository repo) : IInstructorRoleService
{
    public Task<Result<InstructorRole>> CreateInstructorRoleAsync(string roleName, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteInstructorRoleAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<InstructorRole?>> GetInstructorRoleByIdAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IReadOnlyList<InstructorRole>>> GetInstructorRolesAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Result<InstructorRole?>> UpdateInstructorRoleByIdAsync(UpdateInstructorRoleInput input, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
