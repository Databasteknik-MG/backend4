using Application.Instructors.Inputs;
using Application.Instructors.Outputs;
using Domain.Instructors;

namespace Application.Instructors;

public interface IInstructorService
{
    Task<InstructorResult> CreateAsync(CreateInstructorInput input, CancellationToken ct);
    Task<InstructorResult<Instructor>> GetByIdAsync(string id, CancellationToken ct);
    Task<InstructorResult<Instructor>> GetByEmailAsync(string email, CancellationToken ct);
    Task<InstructorResult<IReadOnlyList<Instructor>>> GetAllAsync(CancellationToken ct);
}

public sealed class InstructorService(IInstructorRepository repo) : IInstructorService
{
    public async Task<InstructorResult> CreateAsync(CreateInstructorInput input, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<InstructorResult<IReadOnlyList<Instructor>>> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<InstructorResult<Instructor>> GetByEmailAsync(string email, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<InstructorResult<Instructor>> GetByIdAsync(string id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
