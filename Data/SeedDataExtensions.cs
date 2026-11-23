using G_ASP_NET_80_EFCore03_04.Model;
using G_ASP_NET_80_EFCore03_04.Models;
using Microsoft.EntityFrameworkCore;

namespace G_ASP_NET_80_EFCore03_04.Data;

public static class SeedDataExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Instructor>().HasData(
            new Instructor { Id = 1, FullName = "Sara Ahmed", Email = "sara.ahmed@learnhub.com", Bio = "Senior .NET developer and trainer.", JoinedDate = new DateTime(2024, 1, 15) },
            new Instructor { Id = 2, FullName = "Omar Khaled", Email = "omar.khaled@learnhub.com", Bio = null, JoinedDate = new DateTime(2024, 6, 1) });


        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Programming", Description = "All programming topics", ParentCategoryId = null },
            new Category { Id = 2, Name = "Backend", Description = "Server-side development", ParentCategoryId = 1 },
            new Category { Id = 3, Name = "ASP.NET", Description = null, ParentCategoryId = 2 },
            new Category { Id = 4, Name = "Frontend", Description = "Client-side development", ParentCategoryId = 1 },
            new Category { Id = 5, Name = "Design", Description = "UI and graphic design", ParentCategoryId = null });

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Title = "ASP.NET Core Fundamentals", Description = "Build web apps with ASP.NET Core", Price = 49.99m, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2024, 3, 1), InstructorId = 1, CategoryId = 3 },
            new Course { Id = 2, Title = "Advanced EF Core", Description = "Deep dive into EF Core", Price = 79.99m, Level = CourseLevel.Advanced, IsPublished = true, PublishedDate = new DateTime(2024, 5, 10), InstructorId = 1, CategoryId = 3 },
            new Course { Id = 3, Title = "C# for Beginners", Description = null, Price = 29.99m, Level = CourseLevel.Beginner, IsPublished = true, PublishedDate = new DateTime(2024, 4, 20), InstructorId = 2, CategoryId = 1 },
            new Course { Id = 4, Title = "Intro to UI Design", Description = "Design basics", Price = 39.99m, Level = CourseLevel.Beginner, IsPublished = false, PublishedDate = null, InstructorId = 2, CategoryId = 5 });

        modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, FullName = "Ahmed Ali", Email = "ahmed.ali@mail.com", DateOfBirth = new DateTime(2000, 2, 14) },
            new Student { Id = 2, FullName = "Mona Samir", Email = "mona.samir@mail.com", DateOfBirth = new DateTime(1999, 9, 3) },
            new Student { Id = 3, FullName = "Youssef Nabil", Email = "youssef.nabil@mail.com", DateOfBirth = new DateTime(2001, 11, 25) });

        modelBuilder.Entity<Enrollment>().HasData(
            new Enrollment { StudentId = 1, CourseId = 1, EnrollmentDate = new DateTime(2025, 1, 10), ProgressPercentage = 40, Status = EnrollmentStatus.Active, FinalGrade = null },
            new Enrollment { StudentId = 2, CourseId = 1, EnrollmentDate = new DateTime(2025, 1, 12), ProgressPercentage = 100, Status = EnrollmentStatus.Completed, FinalGrade = 92.5m },
            new Enrollment { StudentId = 1, CourseId = 2, EnrollmentDate = new DateTime(2025, 2, 1), ProgressPercentage = 10, Status = EnrollmentStatus.Active, FinalGrade = null },
            new Enrollment { StudentId = 3, CourseId = 3, EnrollmentDate = new DateTime(2025, 2, 15), ProgressPercentage = 20, Status = EnrollmentStatus.Dropped, FinalGrade = null });
    }
}
