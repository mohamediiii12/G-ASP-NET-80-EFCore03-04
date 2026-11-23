using G_ASP_NET_80_EFCore03_04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_ASP_NET_80_EFCore03_04.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(c => c.Description)
               .IsRequired(false)
               .HasMaxLength(2000);

        builder.Property(c => c.Price)
               .IsRequired()
               .HasPrecision(18, 2);

        builder.Property(c => c.Level)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(20);

        builder.Property(c => c.IsPublished)
               .IsRequired();

        builder.Property(c => c.PublishedDate)
               .IsRequired(false);

    
        builder.HasOne(c => c.Instructor)
               .WithMany(i => i.Courses)
               .HasForeignKey(c => c.InstructorId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

     
        builder.HasOne(c => c.Category)
               .WithMany(cat => cat.Courses)
               .HasForeignKey(c => c.CategoryId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);
    }
}
