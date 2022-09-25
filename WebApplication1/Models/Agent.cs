using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RAWFIM.Models
{

    public class Agent : IdentityUser
    {
        public int Matricule_agent { get; set; }
        public string? Prenom_agent { get; set; }
        public string? Nom_agent { get; set; }
        
        public int? Id_division { get; set; }

        [ForeignKey("Id_division")]
        public virtual Division? Division { get; set; }
      
    }
}
