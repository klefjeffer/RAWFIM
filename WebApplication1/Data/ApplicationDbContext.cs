using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RAWFIM.Models;
using System.Reflection.Emit;
using Microsoft.Extensions.Configuration;

namespace RAWFIM.Data
{
    public class ApplicationDbContext : IdentityDbContext<Agent>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base( options ) 
        {

        }


        public DbSet<Agent>? Agents { get; set; }
        public DbSet<Demande>? Demandes { get; set; }
        public DbSet<Marque>? Marques { get; set; }
        public DbSet<Materiel>? Materiels { get; set; }
        public DbSet<Validation_admin>? Validations_admin { get; set; }
        public DbSet<Validation_chef>? Validations_chef { get; set; }
        public DbSet<Type_Machine>? Type_Machines{ get; set; }
        public DbSet<Affectation_Materiel>? Affectation_Materiels { get; set; }
        public DbSet<Transfert_Materiel>? Transfert_Materiels { get; set; }

        public DbSet<Division>? Divisions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Agent>()
                .HasIndex(u => u.Matricule_agent)
                .IsUnique();


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
            
        

        }
    }
}
