namespace G_ASP_NET_80_EFCore03_04.Models;

public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }

    public List<Enrollment> Enrollments { get; set; } = new();
}
