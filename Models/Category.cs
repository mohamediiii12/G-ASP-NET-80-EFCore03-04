using G_ASP_NET_80_EFCore03_04.Models;

namespace G_ASP_NET_80_EFCore03_04.Model;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public List<Category> SubCategories { get; set; } = new();

    public List<Course> Courses { get; set; } = new();
}
