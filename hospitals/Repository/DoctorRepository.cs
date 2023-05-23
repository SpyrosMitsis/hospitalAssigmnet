using hospitals.Models;
using hospitals.Data;
using hospitals.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hospitals.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param
        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }
        /// <summary>
        /// Retrieves a doctor by their ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the doctor to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, containing the retrieved doctor.</returns>
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
