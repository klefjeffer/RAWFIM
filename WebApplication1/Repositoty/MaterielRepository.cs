using Microsoft.EntityFrameworkCore;
using RAWFIM.Data;
using RAWFIM.Interfaces;
using RAWFIM.Models;

namespace RAWFIM.Repositoty
{
    public class MaterielRepository : IMaterielRepository


    {
        private readonly ApplicationDbContext _context;

        public MaterielRepository(ApplicationDbContext context)
        {
            _context = context;
        }

         public bool Add(Materiel materiel)
        {
            _context.Add(materiel);
            return Save();
        }

        public bool Delete(int id)
        {
            _context.Remove(id);
            return Save();
        }

        /*public async Task<IEnumerable<Materiel>> Get(int id)
        {
           
        }*/

        public async Task<IEnumerable<Materiel>> GetAll()
        {
            return await _context.Materiels.ToListAsync();
        }

        public async Task<Materiel> GetByIdAsync(int id)
        {
            return await _context.Materiels.Include(i=>i.Type_machine).FirstOrDefaultAsync(i => i.Id_machine == id);
        }

        public async Task<IEnumerable<Affectation_Materiel>> GetAffectation_Materiel(int id)
        {
            return await _context.Affectation_Materiels.Where(c=>c.Id_machine == id).ToListAsync();
        }

       

        public bool Save()
        {
            var saved=_context.SaveChanges();
            return saved>0;
        }

        public bool Update(Materiel materiel)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Materiel>> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
