using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RAWFIM.Data;
using RAWFIM.Interfaces;
using RAWFIM.Models;
using RAWFIM.ViewModels;
using System.Data;

namespace RAWFIM.Controllers
{
    [Authorize(Roles = "admin")]
    public class MaterielController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMaterielRepository _materielRepository;

        public MaterielController(ApplicationDbContext context, IMaterielRepository materielRepository)
        {
            _context = context;
            _materielRepository = materielRepository;
        }
        public async Task<IActionResult> Index()
        {
            var materiels = await _materielRepository.GetAll();
            ViewData["type_machine"] = _context.Type_Machines.ToList();
            ViewData["marque"] = _context.Marques.ToList();
            return View(materiels);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Materiel materiel = await _materielRepository.GetByIdAsync(id);
            ViewData["type_machine"] = _context.Type_Machines.ToList();
            ViewData["marque"] = _context.Marques.ToList();
            var affectations= await _materielRepository.GetAffectation_Materiel(id);
            var listAffectation = new Affectations_MaterielViewModel()
            {
                Materiel = materiel,
                Affectations = affectations,

            };  
            return View(listAffectation);
        }

        public IActionResult Create() 
        {
            ViewData["type_machine"] = _context.Type_Machines.ToList();
            ViewData["marque"] = _context.Marques.ToList();
            return View();
        }

        [HttpPost]
        public  IActionResult Create(MaterielVM materielVM) 
        {
            if (!ModelState.IsValid)
            {
                ViewData["type_machine"] = _context.Type_Machines.ToList();
                ViewData["marque"] = _context.Marques.ToList();
                return View(materielVM);

            }
            var materiel = new Materiel()
            {
                Date_fin_garantie = materielVM.Date_fin_garantie,
                Date_reception = materielVM.Date_reception,
                Id_type_machine=materielVM.Id_type_machine,
                Num_marché=materielVM.Num_marché,
                Id_marque=materielVM.Id_marque,
                Description_machine=materielVM.Description_machine,
                Quantite_stock=materielVM.Quantite_stock,   
                Nom_machine=materielVM.Nom_machine,

            };

            if (materielVM.TypeNotIn)
            {
                Type_Machine newType = new()
                {
                    Description_type = materielVM.NewType,
                };
                materiel.Id_type_machine = null;

                materiel.Type_machine = newType;
            }
            if (materielVM.MarqueNotIn)
            {
                Marque newMarque = new()
                {
                    Nom_marque = materielVM.NewMarque,
                };
                materiel.Id_marque = null;

                materiel.Marque=newMarque;
            }
            
            _materielRepository.Add(materiel);
            for(int i = 0; i < materiel.Quantite_stock; i++)
            {
                _context.Affectation_Materiels.Add(new Affectation_Materiel()
                { 
                    Id_machine=materiel.Id_machine,


                });

                
            }
            _context.SaveChanges();
            return  RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id) 
        {
            var materiel = await _materielRepository.GetByIdAsync(id);
            if (materiel == null) return View("Error");
            var materielVM = new EditMaterielViewModel
            {
                Date_fin_garantie = materiel.Date_fin_garantie,
                Date_reception = materiel.Date_reception,
                Id_machine = materiel.Id_machine,
                Id_marque = (int)materiel.Id_marque,
                Id_type_machine = (int)materiel.Id_type_machine,
                Description_machine = materiel.Description_machine,
                Nom_machine = materiel.Nom_machine,
                Num_marché = materiel.Num_marché,
                Quantite_stock = materiel.Quantite_stock


            };

            return View(materielVM);
        }
    }
}
