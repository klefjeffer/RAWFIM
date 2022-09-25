using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RAWFIM.Models
{
    public class Affectation_Materiel
    {
        [Key]
        public int Id_affectation { get; set; }
        
        public int Id_machine { get; set; }
        [ForeignKey("Id_machine")]
        public virtual Materiel? Materiel { get; set; }
        
        public string? Id_demandeur { get; set; }
        [ForeignKey("Id_demandeur")]
        public virtual Agent? Agent_demandeur { get; set; }
        public DateTime Date_affectation { get; set; }
       
        public string? Id_validateur { get; set; }
        [ForeignKey("Id_validateur")]
        public virtual Agent? Agent_validateur { get; set; }
        
        public int? Id_demande { get; set; }
        [ForeignKey("Id_demande")]
        public virtual Demande? Demande { get; set; }
    }
}
