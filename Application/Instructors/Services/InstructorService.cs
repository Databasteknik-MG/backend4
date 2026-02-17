using Application.Common.Results;
using Application.Instructors.Contracts;
using Application.Instructors.Inputs;
using Domain.Instructors;
using Domain.Instructors.Repositories;

namespace Application.Instructors.Services;

public sealed class InstructorService(IInstructorRepository instructorRepository, IInstructorRoleRepository roleRepository) : IInstructorService
{
    public async Task<Result<Instructor?>> CreateInstructorAsync(CreateInstructorInput input, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(input.Email))
            return Result<Instructor?>.BadRequest("Email is required.");

        var existing = await instructorRepository.GetByEmailAsync(input.Email.Trim(), ct);
        if (existing is not null)
            return Result<Instructor?>.Conflict("An instructor with that email already exists.");

        var role = await roleRepository.GetByIdAsync(input.RoleId, ct);
        if (role is null)
            return Result<Instructor?>.BadRequest("Instructor role is required.");

        var instructor = new Instructor(
            Guid.NewGuid().ToString(),
            input.FirstName,
            input.LastName,
            input.Email,
            input.PhoneNumber,
            role
        );

        var created = await instructorRepository.AddAsync(instructor, ct);

        return created is null
            ? Result<Instructor?>.BadRequest("Instructor was not created.")
            : Result<Instructor?>.Ok(created);
    }

    public Task<Result> DeleteInstructorAsync(string id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Instructor?>> GetInstructorByEmailAsync(string email, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Instructor?>> GetInstructorByIdAsync(string id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Instructor>> GetInstructorsAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Instructor?>> UpdateInstructorAsync(UpdateInstructorInput input, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
