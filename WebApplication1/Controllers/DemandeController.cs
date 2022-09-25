using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RAWFIM.Data;
using RAWFIM.Interfaces;
using RAWFIM.Models;
using RAWFIM.ViewModels;
using System.Security.Claims;

namespace RAWFIM.Controllers
{
    public class DemandeController : Controller
    {
        private readonly ApplicationDbContext _context;          
        private readonly IDemandeRepository _demandeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Agent> _userManager;

        public DemandeController( ApplicationDbContext context, IDemandeRepository demandeRepository,IHttpContextAccessor httpContextAccessor,UserManager<Agent> userManager)
        {
            _context = context;
            _demandeRepository = demandeRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager; 


        }
        [Authorize(Roles = "user,validator")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Demandes";
            var usereDemandes = _demandeRepository.GetForUser();
            ViewData["type_machine"] = _context.Type_Machines.ToList();
            ViewData["marque"] = _context.Marques.ToList();
            return View(usereDemandes);
        }

        [Authorize(Roles = "admin")]
        public IActionResult All()
        {
            ViewData["Title"] = "Demandes: Toutes";
            var usereDemandes =  _demandeRepository.GetAllForAdmin();
            var users =  _context.Agents.ToList();
            var userNdemande = new DemandeViewModel()
            {
                ValidationA = usereDemandes,
                Agents = users,
            };
            return View(userNdemande);
        }

        [Authorize(Roles ="validator")]
        public async Task<IActionResult> Agents()
        {
            ViewData["Title"] = "Demandes: Agents";
            var demandesAgents = await _demandeRepository.GetAllByDivision();
            demandesAgents.Agents = await _demandeRepository.GetAgentsFromDivision();
            return View(demandesAgents);
        }

        public IActionResult Erreur()
        {
            return View();
        }

        [Authorize(Roles = "user,validator")]

        public IActionResult Create()
        {
            ViewData["Title"] = "Demandes: Nouveau";
            ViewData["type_machine"] = _context.Type_Machines.ToList();
            ViewData["marque"] = _context.Marques.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Demande demande)
        {
            if (!ModelState.IsValid) return View(demande);
            demande.Date_demande = DateTime.Now;
            var curUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var usr = await _userManager.FindByIdAsync(curUser.ToString());
            var userRole = _context.Divisions.Where(i => i.Id_chef == curUser);
            demande.Id_agent = curUser;
            _demandeRepository.Add(demande);
            if (userRole.Any()==true)
            {
                var validation = new Validation_admin()
                {
                    //id_demande=demande.id_demande,
                    Demande = demande,

                };
                _context.Validations_admin.Add(validation);
                _context.SaveChanges();
            }
            else
            {
                _context.Validations_chef.Add(new Validation_chef()
                {
                    Id_demande = demande.Id_demande,
                    Demande = demande,

                });
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

       

        
    }
}
