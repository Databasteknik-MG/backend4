using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class InstructorRoleEntityConfiguration : IEntityTypeConfiguration<InstructorRoleEntity>
{
    public void Configure(EntityTypeBuilder<InstructorRoleEntity> e)
    {
        e.ToTable("InstructorRoles");

        e.HasKey(x => x.Id);

        e.Property(x => x.RoleName)
            .HasMaxLength(50)
            .IsRequired();

        e.HasIndex(x => x.RoleName)
            .IsUnique();
    }
}