using RAWFIM.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RAWFIM.ViewModels
{
    public class MaterielVM
    {
        
        public int Id_type_machine { get; set; }
        [Display(Name = "Marché")]

        [Required(ErrorMessage = "Champ nécessaire")]
        
        public string? Num_marché { get; set; }

        public int Id_marque { get; set; }
        [Required(ErrorMessage = "Champ nécessaire")]
        public string? Description_machine { get; set; }
        [Required(ErrorMessage = "Champ nécessaire")]
        public int Quantite_stock { get; set; }
        [Required(ErrorMessage = "Champ nécessaire")]
        public string? Nom_machine { get; set; }
        public bool TypeNotIn { get; set; }
        public string NewType { get; set; }
        public bool MarqueNotIn { get; set; }
        public string  NewMarque { get; set; }
        [Required(ErrorMessage = "Champ nécessaire")]
        public DateTime Date_reception { get; set; }
        [Required(ErrorMessage = "Champ nécessaire")]
        public DateTime Date_fin_garantie { get; set; }
    }
}
