using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models;

public class School
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom est obligatoire.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Les sections sont obligatoires.")]
    public string Sections { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le directeur est obligatoire.")]
    public string Director { get; set; } = string.Empty;

    [Range(0, 5, ErrorMessage = "La note doit être entre 0 et 5.")]
    public double Rating { get; set; }

    [Url(ErrorMessage = "Le site web doit être une URL valide.")]
    public string? WebSite { get; set; }
}
