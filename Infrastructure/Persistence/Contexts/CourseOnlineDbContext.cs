using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public sealed class CourseOnlineDbContext(DbContextOptions<CourseOnlineDbContext> options) : DbContext(options)
{
    public DbSet<InstructorEntity> Instructors => Set<InstructorEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<InstructorEntity>(e =>
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

        });
    }
}
