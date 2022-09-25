using System.ComponentModel.DataAnnotations;

namespace RAWFIM.Models
{
    public class Marque
    {
        [Key]
        public int Id_marque { get; set; }
        [Required]
        public string? Nom_marque { get; set; }
    }
}
