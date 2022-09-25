using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RAWFIM.Models
{
    public class Division
    {
        [Key]
        public int Id_division { get; set; }
        [Required]
        public string? Nom_division { get; set; }
        public string? Id_chef { get; set; }
        [ForeignKey("Id_chef")]

        public virtual Agent? Chef { get; set; }
    }
}
