using System.ComponentModel.DataAnnotations;

namespace Hotellerie_X.Models.HotellerieModel
{
    public class Appreciation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de la personne est obligatoire.")]
        [Display(Name = "Nom Personne")]
        public string NomPers { get; set; }

        [Required(ErrorMessage = "Le commentaire est obligatoire.")]
        [DataType(DataType.MultilineText)]
        public string Commentaire { get; set; }

        [Required(ErrorMessage = "La note est obligatoire.")]
        [Range(1, 10, ErrorMessage = "La note doit être comprise entre 1 et 10.")]
        public int Note { get; set; }

        public int HotelId { get; set; }

        public Hotel? Hotel { get; set; }
    }
}
