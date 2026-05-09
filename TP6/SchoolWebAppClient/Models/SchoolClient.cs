namespace SchoolWebAppClient.Models;

public class SchoolClient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sections { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public double Rating { get; set; }
    public string? WebSite { get; set; }
}
