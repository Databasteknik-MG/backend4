using Domain.Instructors;
using Domain.Instructors.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Instructors;

public sealed class InstructorRoleRepository(CourseOnlineDbContext context) : RepositoryBase<InstructorRole, int, InstructorRoleEntity, CourseOnlineDbContext>(context), IInstructorRoleRepository
{
    public async Task<InstructorRole?> GetByRoleNameAsync(string roleName, CancellationToken ct)
    {
        var entity = await Set
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.RoleName == roleName, ct);

        return entity is null ? null : ToModel(entity);
    }

    protected override InstructorRoleEntity ToEntity(InstructorRole model)
    {
        var entity = new InstructorRoleEntity 
        { 
            Id = model.Id, 
            RoleName = model.RoleName 
        };

        return entity;
    }

    protected override InstructorRole ToModel(InstructorRoleEntity entity)
    {
        var model = new InstructorRole
        (
            entity.Id,
            entity.RoleName
        );

        return model;
    }
}