using G_ASP_NET_80_EFCore03_04.Model;

namespace G_ASP_NET_80_EFCore03_04.Models;
public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public CourseLevel Level { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? PublishedDate { get; set; }   

    public int InstructorId { get; set; }
    public Instructor? Instructor { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public List<Enrollment> Enrollments { get; set; } = new();
}
