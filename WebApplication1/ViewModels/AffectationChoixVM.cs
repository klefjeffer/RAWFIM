using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RAWFIM.ViewModels
{
    public class AffectationChoixVM
    {
        public int Id_demande { get; set; }
        [Required]
        public string Id_affectation { get; set; }
        public string id_admin { get; set; }
        [Required]
        public int Id_machine { get; set; }

    }
}
