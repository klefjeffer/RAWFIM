using RAWFIM.Models;
using RAWFIM.ViewModels;

namespace RAWFIM.Interfaces
{
    public interface IDemandeRepository
    {
        DemandeViewModel GetForUser();
        Task<IEnumerable<Demande>> GetAll();
        Task<DemandeViewModel> GetAllByDivision();
        Task<IEnumerable<Agent>> GetAgentsFromDivision();
        IEnumerable<Validation_admin> GetAllForAdmin();
        bool Add(Demande demande);
        bool Update(Demande demande);
        bool Delete(int id);
        bool Save();
    }
}
