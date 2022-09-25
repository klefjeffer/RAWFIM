using Microsoft.AspNetCore.Identity;
using RAWFIM.Models;
using System.Diagnostics;
using System.Net;

namespace RAWFIM.Data



    {
        public class Seed
        {
            public static void SeedData(IApplicationBuilder applicationBuilder)
            {
                using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                    context.Database.EnsureCreated();

                if (!context.Divisions.Any()) 
                {
                    context.Divisions.AddRange(new List<Division>()
                    {
                        new Division()
                        {
                            Nom_division="informatique"
                        },
                       
                        
                    });
                    context.SaveChanges();
                }
                    
                }
            }


        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await roleManager.RoleExistsAsync(UserRoles.Validator))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Validator));

                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                var divInfo = context.Divisions.FirstOrDefault(i => i.Nom_division == "informatique");

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<Agent>>();
                string adminUserEmail = "admin@radees.ma";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new Agent()
                    {
                        UserName = "Administrateur Principal",
                        Email = adminUserEmail,
                        Matricule_agent=18908,
                        EmailConfirmed = true,
                        Id_division=divInfo.Id_division,
                        Nom_agent = "Principal",
                        Prenom_agent = "Administrateur",

                    };
                   
                    await userManager.CreateAsync(newAdminUser, "Radees@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

            }
        }
    }
}