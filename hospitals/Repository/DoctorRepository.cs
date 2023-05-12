using hospitals.Models;
using hospitals.Data;
using hospitals.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hospitals.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public async Task<Doctor> GetByIdAsync(int id)
        {
            var doctors = _context.Doctor
                .Include(d => d.Patients)
                    .ThenInclude(p => p.Room)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            return await doctors;
        }

    }
}
