using Application.Common.Results;
using Application.Instructors.Inputs;
using Domain.Instructors;

namespace Application.Instructors;

public interface IInstructorService
{
    Task<Result<Instructor?>> CreateInstructorAsync(CreateInstructorInput input, CancellationToken ct);
    Task<Result<Instructor?>> GetInstructorByIdAsync(string id, CancellationToken ct);
    Task<Result<Instructor?>> GetInstructorByEmailAsync(string email, CancellationToken ct);
    Task<IReadOnlyList<Instructor>> GetInstructorsAsync(CancellationToken ct);
    Task<Result<Instructor?>> UpdateInstructorAsync(UpdateInstructorInput input, CancellationToken ct);
    Task<Result> DeleteInstructorAsync(string id, CancellationToken ct);
}
