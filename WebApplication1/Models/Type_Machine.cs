using System.ComponentModel.DataAnnotations;

namespace RAWFIM.Models
   
{
    public class Type_Machine
    {
        [Key]
        public int Id_type_machine { get; set; }
        [Required]
        public string? Description_type { get; set; }


    }
}
