namespace AllSpice.Models;

public class Recipe
{
    public int Id;
    public string Title { get; set; }
    public string Instructions { get; set; }
    public string Img { get; set; }
    public string Category { get; set; }
    public Account Creator { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
