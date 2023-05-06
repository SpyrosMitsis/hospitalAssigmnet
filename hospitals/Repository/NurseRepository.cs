using hospitals.Data;
using hospitals.Interfaces;
using hospitals.Models;
using Microsoft.EntityFrameworkCore;

namespace hospitals.Repository
{
    public class NurseRepository : INurseRepository
    {
        private readonly ApplicationDbContext _context;

        public NurseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Nurse nurse)
        {
            _context.Add(nurse);
            return Save();
        }

        public bool Delete(Nurse nurse)
        {
            _context.Remove(nurse);
            return Save();
        }

        public async Task<IEnumerable<Nurse>> GetAll()
        {
            var nurses = _context.Nurse
                .OrderBy(n => n.Id)
                .ToListAsync();

            return await nurses;
        }

        public async Task<Nurse> GetByIdAsync(int id)
        {
            var nurses = _context.Nurse
                .Include(n => n.Rooms)
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync();

            return await nurses;
        }

        public async Task<IEnumerable<Nurse>> GetNurseByAge(int minAge, int maxAge)
        {
            var nurses= _context.Nurse
                .Where(d => d.Age >= minAge && d.Age <= maxAge)
                .ToListAsync();
            if(minAge > maxAge)
                nurses = _context.Nurse
                    .Where(d => d.Age >= minAge)
                    .ToListAsync();

            return await nurses;
        }

        public async Task<IEnumerable<Nurse>> GetNurseBySalary(float minSalary, float maxSalary)
        {
            var nurses = _context.Nurse
                .Where(d => d.Salary >= minSalary && d.Salary <= maxSalary)
                .ToListAsync();

            if(minSalary > maxSalary)
                nurses = _context.Nurse
                    .Where(d => d.Salary >= minSalary)
                    .ToListAsync();

            return await nurses;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Nurse nurse)
        {
            _context.Update(nurse);
            return Save();
        }
    }
}
