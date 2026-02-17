using Application.Common.Results;
using Application.Instructors.Inputs;
using Domain.Instructors;
using Domain.Instructors.Repositories;

namespace Application.Instructors;

public sealed class InstructorService(IInstructorRepository repo) : IInstructorService
{
    Task<Result<Instructor?>> IInstructorService.CreateInstructorAsync(CreateInstructorInput input, CancellationToken ct)
    {
        throw new NotImplementedException();
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
