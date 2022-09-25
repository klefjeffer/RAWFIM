using System.ComponentModel.DataAnnotations;

namespace RAWFIM.ViewModels
{
    public class LoginVM
    {
        [Display(Name="Matricule agent")]
        [Required]

        public int Username { get; set; }
        [Required(ErrorMessage = "Champ nécessaire")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
