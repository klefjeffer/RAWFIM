using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RAWFIM.Data;
using RAWFIM.Interfaces;
using RAWFIM.Models;
using RAWFIM.ViewModels;
using System.Security.Claims;

namespace RAWFIM.Repositoty
{
    public class DemandeRepository : IDemandeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Agent> _userManager;
        public DemandeRepository(UserManager<Agent> userManager, ApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context; 
            _httpContextAccessor = httpContextAccessor; 
            _userManager = userManager;
        }
        public DemandeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Demande demand)
        {
            _context.Add(demand);
            return Save();
        }
        public bool Delete(int id)
        {
            _context.Remove(id);
            return Save();
        }

        public  DemandeViewModel GetForUser()
        {
            var curUser =  _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userDemandes =  _context.Demandes.Where(r => r.Id_agent == curUser.ToString());
            var userValidationA =  _context.Validations_admin.Where(r => r.Demande.Id_agent == curUser);
            var userValidationC =  _context.Validations_chef.Where(r => r.Demande.Id_agent == curUser);

            return new DemandeViewModel()
            {
                Demandes = userDemandes,
                ValidationA = userValidationA,
                ValidationC = userValidationC,
            };
        }

        public bool Save()
        {
            var saved =  _context.SaveChanges();
            return saved>0;
        }

        public bool Update(Demande demande)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Demande>> GetAll()
        {
            return await _context.Demandes.ToListAsync();
        }

        public IEnumerable<Validation_admin> GetAllForAdmin()
        {
            var admin_demande = _context.Validations_admin.Where(r=>true).ToList();
   
            return admin_demande;
        }

        public async Task<DemandeViewModel> GetAllByDivision()
        {
            

            var curUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var usr = await _userManager.FindByIdAsync(curUser.ToString());
            var userDivision = usr.Id_division;
            var divisionChefT = await _context.Divisions.FirstOrDefaultAsync(id => id.Id_division == userDivision);
            var divisionChef = divisionChefT.Id_chef;
            //var userDivision = await _context.Agents.FirstOrDefaultAsync(a => a.Id == curUser.ToString()).division;
            var validations = _context.Validations_chef.Where(r =>r.Demande.Agent.Id_division==userDivision);


            var userDemandes = _context.Demandes
                .Where(r => r.Agent.Id_division == userDivision && r.Agent.Id!=divisionChef )
                ;
            DemandeViewModel vm = new()
            {
                   Demandes=userDemandes.ToList(),
                   ValidationC=validations.ToList(),
            };
            return vm;
        }
        public async Task<IEnumerable<Agent>> GetAgentsFromDivision()
        {
            var curUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var usr = await _userManager.FindByIdAsync(curUser.ToString());
            var userDivision = usr.Id_division;
            var agents = _context.Agents.Where(r => r.Id_division == userDivision);

            return agents.ToList();
        }

        
    }
}
