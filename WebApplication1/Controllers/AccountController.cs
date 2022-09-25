using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RAWFIM.Data;
using RAWFIM.Models;
using RAWFIM.ViewModels;

namespace RAWFIM.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Agent> _userManager;
        private readonly SignInManager<Agent> _signInManager;
        private readonly ApplicationDbContext _context;
   

        public AccountController(UserManager<Agent> userManager, SignInManager<Agent> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
           
        }

       

        public IActionResult Login()
        {
            var response = new LoginVM();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {

            if (!ModelState.IsValid) return View(loginVM);
            var userDB = await _context.Agents.FirstOrDefaultAsync(i => i.Matricule_agent == loginVM.Username);
            if (userDB == null)
            {
                TempData["Error"] = "matricule ou mot de passe incorrect";

                return View(loginVM);
            }
           
            var user = await _userManager.FindByEmailAsync(userDB.Email);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, true, false);
                    if (result.Succeeded)
                    {
                        HttpContext.Session.SetString("Name", (user.Nom_agent + " " + user.Prenom_agent));
                        HttpContext.Items["Name"] = user.Nom_agent + " " + user.Prenom_agent;
                       



                        return RedirectToAction("Index", "Home");
                    }
                }   
                TempData["Error"] = "matricule ou mot de passe incorrect";
                return View(loginVM);
            }
            TempData["Error"] = "matricule ou mot de passe incorrect";
            return View(loginVM);

        }

        public IActionResult Register()
        {
            var response = new RegisterVM();
            TempData["division"] = _context.Divisions.ToList();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["division"] = _context.Divisions.ToList();
                TempData["Error"] = "Erreur de création de compte";
                return View(registerVM);


            }
            var userDB = await _context.Agents.FirstOrDefaultAsync(i => i.Matricule_agent == registerVM.Matricule);
            if (userDB != null)
            {
                TempData["Error"] = "utilisateur existant";
                
                TempData["division"] = _context.Divisions.ToList();

                return View(registerVM);
            }
            var division = new Division();
            var id_division = registerVM.Id_division;
            if (registerVM.DivIsNew)
            {
                division = new Division()
                {
                    Nom_division=registerVM.NewDivName,
                };
                _context.Add(division);
                await _context.SaveChangesAsync();
                id_division = division.Id_division;
            }
           
            division = await _context.Divisions.FirstOrDefaultAsync(i => i.Id_division == id_division);
            
           
            var newUser = new Agent()
            {
                Matricule_agent = registerVM.Matricule,
                Email = "" + registerVM.Matricule + "@radees.ma",
                UserName = "" + registerVM.Prenom+" "+registerVM.Nom,
                Division=division,
                Nom_agent = registerVM.Nom, 
                Prenom_agent = registerVM.Prenom,

            };
            if (registerVM.Est_chef)
            {
                if (division.Id_chef != null)
                {
                    TempData["Error"] = "Un autre utilisateur est chef de division";
                    TempData["division"] = _context.Divisions.ToList();

                    return View(registerVM);
                }
                else
                {
                    division.Chef = newUser;
                }
            }
            string password = registerVM.Matricule + "@Radees.ma";
            var newUserResponse = await _userManager.CreateAsync(newUser, password);
            if (newUserResponse.Succeeded)
            {
                var role = UserRoles.User;
                if (registerVM.Est_chef) role = UserRoles.Validator;
                await _userManager.AddToRoleAsync(newUser, role);
                return RedirectToAction("Index", "Home");
            }
            TempData["Error"] = " Erreur lors de la création du compte";
            return View(registerVM);

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
