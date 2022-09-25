
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RAWFIM.Data;
using RAWFIM.Interfaces;
using RAWFIM.Models;
using RAWFIM.ViewModels;
using System.Security.Claims;

namespace RAWFIM.Controllers
{
    public class AffectationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMaterielRepository _materielRepository;


        public AffectationController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor,IMaterielRepository materielRepository)
        {
            _context=context;
            _httpContextAccessor=httpContextAccessor;
            _materielRepository=materielRepository;
        }
        public IActionResult Index()
        {
            var affectations=_context.Affectation_Materiels.Where(i=>i.Id_validateur!=null).ToList();
            return View(affectations);
        }
        public IActionResult Materiel(int id)
        {
            List<Type_Machine> machines = _context.Type_Machines.ToList();
            List<SelectListItem> types = new();
            machines.ForEach(x => types.Add(new SelectListItem { Text = x.Description_type, Value = x.Id_type_machine.ToString() }));

            List<Marque> marquesL = _context.Marques.ToList();
            List<SelectListItem> marques = new();
            marquesL.ForEach(x => marques.Add(new SelectListItem { Text = x.Nom_marque, Value = x.Id_marque.ToString() }));

            
            ViewData["id_demande"] = id;
            ViewBag.types=types;
            ViewBag.marques = marques;
            return View();
        }

        [HttpPost]
        public JsonResult GetDescriptionMachine(string id_marque, string id_type)
        {

            List<SelectListItem> descriptions = new();
            if (!(string.IsNullOrEmpty(id_type) && string.IsNullOrEmpty(id_marque)))
            {
                int marqueId = Convert.ToInt32(id_marque);
                int typeId = Convert.ToInt32(id_type);

                var materielsDB = _context.Materiels
                    .Where(x => x.Id_marque == marqueId && x.Id_type_machine == typeId).ToList();
                materielsDB.ForEach(x => descriptions.Add(new SelectListItem { Text = x.Description_machine, Value = x.Id_machine.ToString() }));

            }
            return Json(descriptions);
        }

        [HttpPost]
        public JsonResult GetIdMachine(string id_desc)
        {

            List<SelectListItem> machines = new();
            if (!(string.IsNullOrEmpty(id_desc)))
            {
                int id_machine = Convert.ToInt32(id_desc);
               

                var materielsDB = _context.Affectation_Materiels
                    .Where(x => x.Id_machine == id_machine && x.Id_demande==null).ToList();
                materielsDB.ForEach(x => machines.Add(new SelectListItem { Text = x.Id_affectation.ToString(), Value = x.Id_affectation.ToString() }));

            }
            return Json(machines);
        }

        [HttpPost]
        public async Task<IActionResult> Affect(AffectationChoixVM choix)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to validate");

            }
            
            int demande_choix = Convert.ToInt32(choix.Id_demande);
            int affectation_choix=Convert.ToInt32(choix.Id_affectation);
            if (affectation_choix == -1)
            {
                return RedirectToAction("All", "Demande");
            }
            var validation_actuel = await _context.Validations_admin.AsNoTracking().FirstOrDefaultAsync(a=>a.Id_demande==demande_choix);
            if (validation_actuel != null)
            {
                var affectation = await _context.Affectation_Materiels.AsNoTracking().FirstOrDefaultAsync(i => i.Id_affectation == affectation_choix);
               

                var validation_edit = new Validation_admin()
                {
                    Id_demande=demande_choix,
                    Id_validateur = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                    Date_decision = DateTime.Now,
                    Status_validation = true,


                };
               
                _context.Update(validation_edit);
                var demande=await _context.Demandes.AsNoTracking().FirstOrDefaultAsync(a=>a.Id_demande == demande_choix);                
                var affectation_edit = new Affectation_Materiel()
                {
                    Id_affectation=affectation.Id_affectation,
                    Id_demandeur = demande.Id_agent,
                    Id_validateur =validation_edit.Id_validateur,
                    Date_affectation = DateTime.Now,
                    Id_demande = validation_edit.Id_demande,
                    Id_machine=affectation.Id_machine,

                };
                _context.Update(affectation_edit);
                await _context.SaveChangesAsync();
                
            }
            return RedirectToAction("All","Demande");
        }

        public IActionResult Detail(int id)
        {
            
            ViewData["type_machine"] = _context.Type_Machines.ToList();
            ViewData["marque"] = _context.Marques.ToList();
            var affectations =  _context.Affectation_Materiels.Where(i => i.Id_affectation == id).ToList();
            var listAffectation = new Affectations_MaterielViewModel()
            {
               
                Affectations = affectations,
                Actuel = id,
            };
            return View(listAffectation);
        }

        //[HttpPost]
        //public JsonResult GetDescriptionMachine(string id_marque,string id_type)
        //{

        //    List<SelectListItem> descriptions = new List<SelectListItem>();
        //    if (!(string.IsNullOrEmpty(id_marque)))
        //    {
        //        int marqueId = Convert.ToInt32(id_marque);


        //        var materielsDB = _context.Materiels
        //            .Where(x => x.id_marque == marqueId).ToList();
        //        materielsDB.ForEach(x => descriptions.Add(new SelectListItem { Text = x.description_machine, Value = x.id_machine.ToString() }));

        //    }
        //    return Json(descriptions);
        //}
    }
}
