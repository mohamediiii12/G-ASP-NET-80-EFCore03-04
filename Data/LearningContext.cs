
using G_ASP_NET_80_EFCore03_04.Model;
using G_ASP_NET_80_EFCore03_04.Models;
using Microsoft.EntityFrameworkCore;

namespace G_ASP_NET_80_EFCore03_04.Data;

public class LearningContext : DbContext
{
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-V14FACA;Database=OnlineLearningDb;trusted_connection=true;trustservercertificate=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LearningContext).Assembly);

        
    }
}
