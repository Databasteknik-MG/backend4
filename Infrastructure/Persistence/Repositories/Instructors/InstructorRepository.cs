using Domain.Instructors;
using Domain.Instructors.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Instructors;

public sealed class InstructorRepository(CourseOnlineDbContext context) : RepositoryBase<Instructor, string, InstructorEntity, CourseOnlineDbContext>(context), IInstructorRepository
{
    public async Task<Instructor?> GetByEmailAsync(string email, CancellationToken ct)
    {
        var entity = await Set.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email, ct);
        return entity is null ? default : ToModel(entity);
    }

    protected override InstructorEntity ToEntity(Instructor model)
    {
        var date = DateTime.UtcNow;

        var entity = new InstructorEntity 
        { 
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            CreatedAt = date,
            ModifiedAt = date
        };

        if (model.PhoneNumber is not null && !string.IsNullOrWhiteSpace(model.PhoneNumber))
            entity.PhoneNumber = model.PhoneNumber;

        return entity;
    }

    protected override Instructor ToModel(InstructorEntity entity)
    {
        var model = new Instructor
        (
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Email,
            entity.PhoneNumber
        );

        return model;
    }
}
