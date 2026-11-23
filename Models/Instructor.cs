namespace G_ASP_NET_80_EFCore03_04.Models;

public class Instructor
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public DateTime JoinedDate { get; set; }

    public List<Course> Courses { get; set; } = new();
}
