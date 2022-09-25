using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RAWFIM.Models
{
    public class Demande
    {
        [Key]
        public int Id_demande { get; set; }
        public DateTime Date_demande { get; set; }
       
        public int Id_type_machine { get; set; }
        [ForeignKey("Id_type_machine")]
        public virtual Type_Machine? Type_machine { get; set; }
      
        
        public string? Id_agent { get; set; }
        [ForeignKey("Id_agent")]
        public virtual Agent? Agent { get; set; }

        public string? Desctription_demande { get; set; }
    }
}
