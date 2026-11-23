using G_ASP_NET_80_EFCore03_04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_ASP_NET_80_EFCore03_04.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.FullName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(s => s.Email)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(s => s.DateOfBirth)
               .IsRequired()
               .HasColumnType("date");
    }
}
