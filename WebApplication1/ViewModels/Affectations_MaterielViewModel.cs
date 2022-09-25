using RAWFIM.Models;

namespace RAWFIM.ViewModels
{
    public class Affectations_MaterielViewModel
    {
        public IEnumerable<Affectation_Materiel> Affectations { get; set; }
        public Materiel Materiel { get; set; }

        public int? Actuel { get; set; }

    }
}
