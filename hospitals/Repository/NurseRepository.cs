using hospitals.Data;
using hospitals.Interfaces;
using hospitals.Models;
using Microsoft.EntityFrameworkCore;

namespace hospitals.Repository
{
    public class NurseRepository : INurseRepository
    {
        private readonly ApplicationDbContext _context;
    
        /// <summary>
        /// Initializes a new instance of the <see cref="NurseRepository"/> class.
        /// </summary>
        /// <param name="context">The application DbContext.</param>
        public NurseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    
        /// <summary>
        /// Gets a nurse by their ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the nurse to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the nurse.</returns>
        public async Task<Nurse> GetByIdAsync(int id)
        {
            var nurses = await _context.Nurse
                .Include(n => n.Rooms)
                .Where(n => n.Id == id)
                .FirstOrDefaultAsync();
    
            return nurses;
        }
    }
}
