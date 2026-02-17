using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public sealed class CourseOnlineDbContext(DbContextOptions<CourseOnlineDbContext> options) : DbContext(options)
{
    public DbSet<InstructorEntity> Instructors => Set<InstructorEntity>();
    public DbSet<InstructorRoleEntity> InstructorRoles => Set<InstructorRoleEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseOnlineDbContext).Assembly);

    }
}
