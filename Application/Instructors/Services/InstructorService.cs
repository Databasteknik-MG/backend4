using Application.Common.Results;
using Application.Instructors.Contracts;
using Application.Instructors.Inputs;
using Domain.Instructors;
using Domain.Instructors.Repositories;

namespace Application.Instructors.Services;

public sealed class InstructorService(IInstructorRepository instructorRepository, IInstructorRoleService instructorRoleService) : IInstructorService
{
    async Task<Result<Instructor?>> IInstructorService.CreateInstructorAsync(CreateInstructorInput input, CancellationToken ct)
    {
        var instructorRoleResult = await instructorRoleService.GetInstructorRoleByIdAsync(input.RoleId, ct);
        var instructorRole = instructorRoleResult.Value;

        if (instructorRole is null)
            return Result<Instructor?>.BadRequest("instructor role is required");


        var instructor = new Instructor
        (
            Guid.NewGuid().ToString(),
            input.FirstName,
            input.LastName,
            input.Email,
            input.PhoneNumber,
            instructorRole
        );

        var result = await instructorRepository.AddAsync(instructor, ct);
        
        return result is null
            ? Result<Instructor?>.BadRequest("instructor was not created")
            : Result<Instructor?>.Ok(result);

    }

    Task<Result> IInstructorService.DeleteInstructorAsync(string id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    Task<Result<Instructor?>> IInstructorService.GetInstructorByEmailAsync(string email, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    Task<Result<Instructor?>> IInstructorService.GetInstructorByIdAsync(string id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    Task<IReadOnlyList<Instructor>> IInstructorService.GetInstructorsAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    Task<Result<Instructor?>> IInstructorService.UpdateInstructorAsync(UpdateInstructorInput input, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
