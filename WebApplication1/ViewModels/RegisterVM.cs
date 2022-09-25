using System.ComponentModel.DataAnnotations;

namespace RAWFIM.ViewModels
{
    public class RegisterVM
    {
        [Display(Name="Matricule agent")]
        [Required(ErrorMessage="Matricule nécessaire")]
        public int Matricule { get; set; }
        [Display(Name = "Prénom agent")]
        [Required(ErrorMessage = "Prénom nécessaire")]
        public string Prenom { get; set; }
        [Display(Name = "Nom agent")]
        [Required(ErrorMessage = "Nom nécessaire")]
        public string Nom { get; set; }
        [Display(Name = "Division agent")]
        [Required(ErrorMessage = "Division nécessaire")]
        public int Id_division { get; set; }

        public bool Est_chef { get; set; }
        public bool DivIsNew { get; set; }
        public string? NewDivName { get; set; }

    }
}
