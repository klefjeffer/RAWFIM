using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RAWFIM.Models
{

    
    public class Transfert_Materiel
    {
        [Key]
        public int Id_emprunt { get; set; }

        public int Id_affectation { get; set; }
        [ForeignKey("Id_affectation")]

        public virtual Affectation_Materiel? Affectation { get; set; }
      
        public string? Matricule_agent_emprunteur { get; set; }
        [ForeignKey("Matricule_agent_emprunteur")]
        public virtual Agent? Agent_emprunteur { get; set; }

        public DateTime Date_emprunt { get; set; }
        public string? Matricule_agent_empruntant { get; set; }
        [ForeignKey("Matricule_agent_empruntant")]
        public virtual Agent? Agent_empruntant { get; set; }
       
    }
}
