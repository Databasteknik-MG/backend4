using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class InstructorEntityConfiguration : IEntityTypeConfiguration<InstructorEntity>
{
    public void Configure(EntityTypeBuilder<InstructorEntity> e)
    {
        e.ToTable("Instructors", t =>
        {
            t.HasCheckConstraint("CK_Instructors_Email_NotEmpty", "LEN([Email]) > 0");
            t.HasCheckConstraint("CK_Instructors_Phone_NotEmpty", "[PhoneNumber] IS NULL OR LEN([PhoneNumber]) > 0");
        });

        e.HasKey(x => x.Id);

        e.Property(x => x.Id)
            .HasMaxLength(64);

        e.Property(x => x.FirstName)
            .HasMaxLength(20)
            .IsRequired();

        e.Property(x => x.LastName)
            .HasMaxLength(20)
            .IsRequired();

        e.Property(x => x.Email)
            .HasMaxLength(320)
            .IsRequired()
            .IsUnicode(false);

        e.Property(x => x.PhoneNumber)
            .HasMaxLength(16)
            .IsRequired(false);

        e.Property(x => x.InstructorRoleId)
            .IsRequired();

        e.HasOne(x => x.InstructorRole)
            .WithMany(x => x.Instructors)
            .HasForeignKey(x => x.InstructorRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        e.Property(x => x.CreatedAt)
            .HasColumnType("datetime2(0)")
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        e.Property(x => x.ModifiedAt)
            .HasColumnType("datetime2(0)")
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .IsRequired();

        e.Property(x => x.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();

        e.HasIndex(x => x.Email)
            .IsUnique();

        e.HasIndex(x => new { x.LastName, x.FirstName });
    }
}
