using Domain.Instructors;
using Domain.Instructors.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Instructors;

public sealed class InstructorRepository(CourseOnlineDbContext context) : RepositoryBase<Instructor, string, InstructorEntity, CourseOnlineDbContext>(context), IInstructorRepository
{
    public override async Task<IReadOnlyList<Instructor>> GetAllAsync(CancellationToken ct)
    {
        var entities = await Set
            .AsNoTracking()
            .Include(i => i.InstructorRole)
            .ToListAsync(ct);

        return [.. entities.Select(ToModel)];
    }
    
    public override async Task<Instructor?> GetByIdAsync(string id, CancellationToken ct)
    {
        var entity = await Set
            .AsNoTracking()
            .Include(i => i.InstructorRole)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return entity is null ? default : ToModel(entity);
    }

    public async Task<Instructor?> GetByEmailAsync(string email, CancellationToken ct)
    {
        var entity = await Set
            .AsNoTracking()
            .Include(i => i.InstructorRole)
            .FirstOrDefaultAsync(x => x.Email == email, ct);

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
            ModifiedAt = date,
            InstructorRoleId = model.Role.Id
        };

        if (model.PhoneNumber is not null && !string.IsNullOrWhiteSpace(model.PhoneNumber))
            entity.PhoneNumber = model.PhoneNumber;

        return entity;
    }

    protected override Instructor ToModel(InstructorEntity entity)
    {
        if (entity.InstructorRole is null)
            throw new InvalidOperationException("InstructorRole must be included when mapping to domain.");

        var role = new InstructorRole(entity.InstructorRole.Id, entity.InstructorRole.RoleName);

        var model = new Instructor
        (
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.Email,
            entity.PhoneNumber,
            role
        );

        return model;
    }
}
