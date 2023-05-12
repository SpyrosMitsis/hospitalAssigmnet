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

        public async Task<Nurse> GetByIdAsync(int id)
        {
            var nurses = _context.Nurse
                .Include(n => n.Rooms)
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync();

            return await nurses;
        }

    }
}
