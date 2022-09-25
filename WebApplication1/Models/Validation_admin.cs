using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RAWFIM.Models
{
    public class Validation_admin
    { 
        [Key]
        public int Id_demande { get; set; }
        [ForeignKey("Id_demande")]
        public virtual Demande Demande { get; set; }
        
        public string? Id_validateur { get; set; }
        [ForeignKey("Id_validateur")]
        public virtual Agent? Agent_validateur { get; set; }
        public bool? Status_validation { get; set; }
        public DateTime? Date_decision{ get; set; }
        public string? Motif_refus { get; set; }

    }
}
