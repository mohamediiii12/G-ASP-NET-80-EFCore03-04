using G_ASP_NET_80_EFCore03_04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_ASP_NET_80_EFCore03_04.Configurations;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.FullName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(i => i.Email)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(i => i.Bio)
               .IsRequired(false)
               .HasMaxLength(1000);

        builder.Property(i => i.JoinedDate)
               .IsRequired()
               .HasColumnType("date");
    }
}
