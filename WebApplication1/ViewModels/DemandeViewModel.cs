using RAWFIM.Models;

namespace RAWFIM.ViewModels
{
    public class DemandeViewModel
    {
        public IEnumerable<Demande>? Demandes { get; set; }
        public IEnumerable<Validation_chef>? ValidationC { get; set; }
        public IEnumerable<Validation_admin>? ValidationA { get; set; }
        public IEnumerable<Agent>? Agents { get; set; }
    }
}
