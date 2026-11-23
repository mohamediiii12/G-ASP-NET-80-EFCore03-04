using G_ASP_NET_80_EFCore03_04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_ASP_NET_80_EFCore03_04.Configurations;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
 
        builder.HasKey(e => new { e.StudentId, e.CourseId });

        builder.Property(e => e.EnrollmentDate)
               .IsRequired();

        builder.Property(e => e.ProgressPercentage)
               .IsRequired();

        builder.Property(e => e.Status)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20);

        builder.Property(e => e.FinalGrade)
               .IsRequired(false)
               .HasPrecision(5, 2);


        builder.HasOne(e => e.Student)
               .WithMany(s => s.Enrollments)
               .HasForeignKey(e => e.StudentId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Course)
               .WithMany(c => c.Enrollments)
               .HasForeignKey(e => e.CourseId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
    }
}
