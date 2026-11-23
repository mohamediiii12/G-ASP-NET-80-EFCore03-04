using G_ASP_NET_80_EFCore03_04.Data;
using G_ASP_NET_80_EFCore03_04.Models;
using Microsoft.EntityFrameworkCore;

namespace G_ASP_NET_80_EFCore03_04
{
    internal class Program
    {
        private static void Main()
        {
            // ===================== CREATE =====================
            Section("CREATE");
            int instructorId = AddInstructor();
            int newCourseId = AddCourse(instructorId, categoryId: 3);
            AddCourse(instructorId: 999, categoryId: 3);         
            AddCourse(instructorId, categoryId: 999);             
            List<int> studentIds = AddStudents();

            Enroll(studentIds[0], 2);                              
            Enroll(studentIds[0], 2);                             
            Enroll(studentIds[1], newCourseId);                    
            Enroll(999, 2);                                        
            Enroll(studentIds[1], 999);                           

            // ===================== READ =====================
            Section("READ");
            FindCourse(1, "ASP.NET Core Fundamentals");
            FindCourse(999, "Does Not Exist");
            FilterCourses(30m, 80m);
            SubCategories(1);
            CourseRoster(1);

            // ===================== UPDATE =====================
            Section("UPDATE");
            ChangePrice(1, 54.99m);
            ChangePrice(999, 10m);                               
            PublishCourse(newCourseId);
            Enroll(studentIds[1], newCourseId);                 
            UpdateProgress(studentIds[0], 2, 60);
            UpdateProgress(studentIds[0], 2, 150);                 
            UpdateProgress(studentIds[0], 2, 100);                

            // ===================== DELETE =====================
            Section("DELETE");
            Unenroll(studentIds[0], 2);
            Unenroll(studentIds[0], 2);                         
            DeleteCategoryUnsafe(1);                               
            DeleteCategorySafe(1);                                
            DeleteCategorySafe(4);                                 
            DeleteCourse(1);                                    
        }

        private static void Section(string title)
        {
            Console.WriteLine();
            Console.WriteLine($"=============== {title} ===============");
        }

        // ----------------------------------------------------------------
        // CREATE
        // ----------------------------------------------------------------
        private static int AddInstructor()
        {
            using var context = new LearningContext();

            var instructor = new Instructor
            {
                FullName = "Hana Mostafa",
                Email = "hana.mostafa@learnhub.com",
                Bio = "Backend engineer and instructor.",
                JoinedDate = DateTime.Today
            };

            context.Instructors.Add(instructor);
            context.SaveChanges();

            Console.WriteLine($"Instructor added. Generated Id = {instructor.Id}");
            return instructor.Id;
        }

        private static int AddCourse(int instructorId, int categoryId)
        {
            using var context = new LearningContext();

            if (!context.Instructors.Any(i => i.Id == instructorId))
            {
                Console.WriteLine($"Cannot add course: instructor {instructorId} does not exist.");
                return 0;
            }

            if (!context.Categories.Any(c => c.Id == categoryId))
            {
                Console.WriteLine($"Cannot add course: category {categoryId} does not exist.");
                return 0;
            }

            var course = new Course
            {
                Title = "Building REST APIs with ASP.NET Core",
                Description = "Design and build RESTful APIs.",
                Price = 59.99m,
                Level = CourseLevel.Intermediate,
                IsPublished = false,
                InstructorId = instructorId,
                CategoryId = categoryId
            };

            context.Courses.Add(course);
            context.SaveChanges();

            Console.WriteLine($"Course added. Generated Id = {course.Id}");
            return course.Id;
        }

        private static List<int> AddStudents()
        {
            using var context = new LearningContext();

            var students = new List<Student>
        {
            new() { FullName = "Laila Hassan", Email = "laila.hassan@mail.com", DateOfBirth = new DateTime(2002, 4, 8) },
            new() { FullName = "Karim Adel",   Email = "karim.adel@mail.com",   DateOfBirth = new DateTime(2000, 7, 21) },
            new() { FullName = "Nour Fathy",   Email = "nour.fathy@mail.com",   DateOfBirth = new DateTime(2003, 1, 30) }
        };

            context.Students.AddRange(students); 
            context.SaveChanges();           

            foreach (var s in students)
                Console.WriteLine($"Student added: {s.FullName} (Id = {s.Id})");

            return students.Select(s => s.Id).ToList();
        }

        private static void Enroll(int studentId, int courseId)
        {
            using var context = new LearningContext();

            var student = context.Students.Find(studentId);
            if (student == null)
            {
                Console.WriteLine($"Enrollment refused: student {studentId} does not exist.");
                return;
            }

            var course = context.Courses.Find(courseId);
            if (course == null)
            {
                Console.WriteLine($"Enrollment refused: course {courseId} does not exist.");
                return;
            }

            if (!course.IsPublished)
            {
                Console.WriteLine($"Enrollment refused: course '{course.Title}' is not published.");
                return;
            }

            if (context.Enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId))
            {
                Console.WriteLine($"Enrollment refused: {student.FullName} is already enrolled in '{course.Title}'.");
                return;
            }

            context.Enrollments.Add(new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.Now,
                ProgressPercentage = 0,
                Status = EnrollmentStatus.Active
            });
            context.SaveChanges();

            Console.WriteLine($"{student.FullName} enrolled in '{course.Title}'.");
        }

        // ----------------------------------------------------------------
        // READ
        // ----------------------------------------------------------------
        private static void FindCourse(int id, string title)
        {
            using var context = new LearningContext();

            var byId = context.Courses.Find(id);
            Console.WriteLine(byId == null
                ? $"Find({id}): not found"
                : $"Find({id}): {byId.Title} - {byId.Price:0.00}");

            var byTitle = context.Courses.FirstOrDefault(c => c.Title == title);
            Console.WriteLine(byTitle == null
                ? $"FirstOrDefault(\"{title}\"): not found"
                : $"FirstOrDefault(\"{title}\"): Id = {byTitle.Id}, {byTitle.Level}");
        }

        private static void FilterCourses(decimal min, decimal max)
        {
            using var context = new LearningContext();

            var courses = context.Courses
                .Where(c => c.Price >= min && c.Price <= max)
                .OrderByDescending(c => c.Price)
                .Select(c => new { c.Title, c.Level, c.Price })
                .ToList();

            Console.WriteLine($"Courses priced between {min} and {max} (highest first):");
            if (courses.Count == 0)
            {
                Console.WriteLine("  No courses found.");
                return;
            }

            foreach (var c in courses)
                Console.WriteLine($"  {c.Title} | {c.Level} | {c.Price:0.00}");
        }

        private static void SubCategories(int parentId)
        {
            using var context = new LearningContext();

            var parent = context.Categories.Find(parentId);
            if (parent == null)
            {
                Console.WriteLine($"Category {parentId} not found.");
                return;
            }

            var subs = context.Categories
                .Where(c => c.ParentCategoryId == parentId)
                .ToList();

            Console.WriteLine($"Direct sub-categories of '{parent.Name}':");
            if (subs.Count == 0)
                Console.WriteLine("  (none)");
            foreach (var s in subs)
                Console.WriteLine($"  {s.Id}: {s.Name}");

            var topLevel = context.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToList();

            Console.WriteLine("Top-level categories:");
            foreach (var t in topLevel)
                Console.WriteLine($"  {t.Id}: {t.Name}");
        }

        private static void CourseRoster(int courseId)
        {
            using var context = new LearningContext();

            var course = context.Courses.Find(courseId);
            if (course == null)
            {
                Console.WriteLine($"Course {courseId} not found.");
                return;
            }

           
            var studentIds = context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.StudentId)
                .ToList();

            Console.WriteLine($"'{course.Title}' has {studentIds.Count} enrolled student(s):");

            
            var names = context.Students
                .Where(s => studentIds.Contains(s.Id))
                .Select(s => s.FullName)
                .ToList();

            foreach (var name in names)
                Console.WriteLine($"  {name}");
        }

        // ----------------------------------------------------------------
        // UPDATE
        // ----------------------------------------------------------------
        private static void ChangePrice(int courseId, decimal newPrice)
        {
            using var context = new LearningContext();

            if (newPrice < 0)
            {
                Console.WriteLine("Price cannot be negative.");
                return;
            }

            var course = context.Courses.Find(courseId);
            if (course == null)
            {
                Console.WriteLine($"Cannot change price: course {courseId} not found.");
                return;
            }

            decimal oldPrice = course.Price;
            course.Price = newPrice;
            context.SaveChanges();

            Console.WriteLine($"Price of '{course.Title}' changed from {oldPrice:0.00} to {newPrice:0.00}.");
        }

        private static void UpdateProgress(int studentId, int courseId, int progress)
        {
            using var context = new LearningContext();

            if (progress < 0 || progress > 100)
            {
                Console.WriteLine($"Rejected: progress {progress} must be between 0 and 100.");
                return;
            }

            
            var enrollment = context.Enrollments.Find(studentId, courseId);
            if (enrollment == null)
            {
                Console.WriteLine($"Enrollment (student {studentId}, course {courseId}) not found.");
                return;
            }

            enrollment.ProgressPercentage = progress;
            if (progress == 100)
                enrollment.Status = EnrollmentStatus.Completed;

            context.SaveChanges();

            Console.WriteLine($"Progress updated to {progress}% (status: {enrollment.Status}).");
        }

        private static void PublishCourse(int courseId)
        {
            using var context = new LearningContext();

            var course = context.Courses.Find(courseId);
            if (course == null)
            {
                Console.WriteLine($"Cannot publish: course {courseId} not found.");
                return;
            }

            if (course.IsPublished)
            {
                Console.WriteLine($"Course '{course.Title}' is already published.");
                return;
            }

            course.IsPublished = true;
            course.PublishedDate = DateTime.Now;
            context.SaveChanges();

            Console.WriteLine($"Course '{course.Title}' published on {course.PublishedDate:yyyy-MM-dd}.");
        }

        // ----------------------------------------------------------------
        // DELETE
        // ----------------------------------------------------------------
        private static void Unenroll(int studentId, int courseId)
        {
            using var context = new LearningContext();

            var enrollment = context.Enrollments.Find(studentId, courseId);
            if (enrollment == null)
            {
                Console.WriteLine($"Cannot unenroll: enrollment (student {studentId}, course {courseId}) not found.");
                return;
            }

            context.Enrollments.Remove(enrollment);
            context.SaveChanges();

            Console.WriteLine($"Student {studentId} unenrolled from course {courseId}.");
        }

        
        private static void DeleteCategoryUnsafe(int categoryId)
        {
            using var context = new LearningContext();

            var category = context.Categories.Find(categoryId);
            if (category == null)
            {
                Console.WriteLine($"Category {categoryId} not found.");
                return;
            }

            try
            {
                context.Categories.Remove(category);
                context.SaveChanges();
                Console.WriteLine($"Category '{category.Name}' deleted.");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Delete failed (DbUpdateException): {ex.InnerException?.Message ?? ex.Message}");
            }
        }


        private static void DeleteCategorySafe(int categoryId)
        {
            using var context = new LearningContext();

            var category = context.Categories.Find(categoryId);
            if (category == null)
            {
                Console.WriteLine($"Category {categoryId} not found.");
                return;
            }

            if (context.Categories.Any(c => c.ParentCategoryId == categoryId))
            {
                Console.WriteLine($"Cannot delete '{category.Name}': it still has sub-categories.");
                return;
            }

            if (context.Courses.Any(c => c.CategoryId == categoryId))
            {
                Console.WriteLine($"Cannot delete '{category.Name}': it still has courses.");
                return;
            }

            context.Categories.Remove(category);
            context.SaveChanges();

            Console.WriteLine($"Category '{category.Name}' deleted.");
        }

        private static void DeleteCourse(int courseId)
        {
            using var context = new LearningContext();

            var course = context.Courses.Find(courseId);
            if (course == null)
            {
                Console.WriteLine($"Course {courseId} not found.");
                return;
            }

            int before = context.Enrollments.Count(e => e.CourseId == courseId);
            Console.WriteLine($"Enrollments of '{course.Title}' before delete: {before}");

            context.Courses.Remove(course);
            context.SaveChanges();

            int after = context.Enrollments.Count(e => e.CourseId == courseId);
            Console.WriteLine($"Enrollments of course {courseId} after delete: {after}");

            
        }
    }
}
