using Domain.Instructors;
using Domain.Instructors.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;

namespace Infrastructure.Persistence.Repositories.Instructors;

public sealed class InstructorRoleRepository(CourseOnlineDbContext context) : RepositoryBase<InstructorRole, int, InstructorRoleEntity, CourseOnlineDbContext>(context), IInstructorRoleRepository
{
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