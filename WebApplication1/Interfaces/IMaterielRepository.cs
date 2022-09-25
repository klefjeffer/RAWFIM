using RAWFIM.Models;

namespace RAWFIM.Interfaces
{
    public interface IMaterielRepository
    {
        Task<IEnumerable<Materiel>> GetAll();
        Task<IEnumerable<Materiel>> Get(int id);
        Task<Materiel> GetByIdAsync(int id);
        Task<IEnumerable<Affectation_Materiel>> GetAffectation_Materiel(int id);

        bool Add(Materiel materiel);
        bool Update(Materiel materiel);
        bool Delete(int id);
        bool Save();
    }
}
