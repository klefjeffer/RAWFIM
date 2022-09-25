using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RAWFIM.Data;
using RAWFIM.Models;
using RAWFIM.ViewModels;
using System.Security.Claims;

namespace RAWFIM.Controllers
{
    public class ValidationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ValidationController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> AcceptC(int id)
        //{
        //    EditValidationVM vm = new EditValidationVM();
        //    return await AcceptC(id, vm);
        //}
        public async Task<IActionResult> AcceptC(int id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to validate");
            }
            var demande = await _context.Validations_chef.AsNoTracking().FirstOrDefaultAsync(r => r.Id_demande == id);
            if (demande != null)
            {
               

                var validation = new Validation_chef()
                {
                    Id_demande = id,
                    Date_decision=DateTime.Now,
                    Id_validateur =  _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                    Status_validation=true,
                   
                    

            };
                _context.Update(validation);

                _context.Validations_admin.Add(new Validation_admin()
                {
                    Id_demande=id,
                    
                });
                var saved= _context.SaveChanges();
                if (saved>0)
                {
                    return RedirectToAction("Agents", "Demande");
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RefusC(int id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to validate");
            }
            var demande = await _context.Validations_chef.AsNoTracking().FirstOrDefaultAsync(r => r.Id_demande == id);
            if (demande != null)
            {
                // var curUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var validation = new Validation_chef()
                {
                    Id_demande = id,
                    Date_decision = DateTime.Now,
                    Id_validateur = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                    Status_validation = false,



                };
                _context.Update(validation);

                var saved = _context.SaveChanges();
                if (saved > 0)
                {
                    return RedirectToAction("Agents", "Demande");
                }
            }
            return RedirectToAction("Agents");
        }

      

        public async Task<IActionResult> RefusA(int id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to validate");
            }
            var demande = await _context.Validations_admin.AsNoTracking().FirstOrDefaultAsync(r => r.Id_demande == id);
            if (demande != null)
            {
                // var curUser = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var validation = new Validation_admin()
                {
                    Id_demande = id,
                    Date_decision = DateTime.Now,
                    Id_validateur = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                    Status_validation = false,



                };
                _context.Update(validation);

                var saved = _context.SaveChanges();
                if (saved > 0)
                {
                    return RedirectToAction("All", "Demande");
                }
            }
            return RedirectToAction("All");
        }
    }
}
