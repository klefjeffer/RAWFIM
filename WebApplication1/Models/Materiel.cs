using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RAWFIM.Models
{
    public class Materiel
    {
        [Key]
        public int Id_machine { get; set; }
       
        public int? Id_type_machine { get; set; }
        [ForeignKey("Id_type_machine")]
        public virtual Type_Machine? Type_machine { get; set; }

        public string? Num_marché { get; set; }
       
        public int? Id_marque { get; set; }
        [ForeignKey("Id_marque")]
        public virtual Marque? Marque { get; set; }
        public DateTime Date_reception { get; set; }
        public DateTime Date_fin_garantie { get; set; }
        public string? Description_machine { get; set; }
        public int Quantite_stock { get; set; }
        public string? Nom_machine { get; set; }
       


    }
}
