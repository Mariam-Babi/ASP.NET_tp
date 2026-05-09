using System.ComponentModel.DataAnnotations;

namespace RestoManager_X.Models.RestosModel
{
    public class Avis
    {
        public int CodeAvis { get; set; }
        public string NomPersonne { get; set; }

        [Range(1, 5, ErrorMessage = "La note doit être comprise entre 1 et 5.")]
        public int Note { get; set; }

        public string? Commentaire { get; set; }
        public int NumResto { get; set; }

        public Restaurant? LeResto { get; set; }
    }
}
