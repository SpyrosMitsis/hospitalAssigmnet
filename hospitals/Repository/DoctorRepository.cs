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
        public bool Add(Doctor doctor)
        {
            _context.Add(doctor);
            return Save();
        }

        public bool Delete(Doctor doctor)
        {
            _context.Remove(doctor);
            return Save();
        }

        public async Task<IEnumerable<Doctor>> GetAll()
        {
            var doctors = _context.Doctor.OrderBy(d => d.Id).ToListAsync();
            return await doctors;
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Doctor.Include(d => d.Patients).Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorBySalary(float minSalary, float maxSalary)
        {
            var doctors = _context.Doctor.Where(d => d.Salary >= minSalary && d.Salary <= maxSalary).ToListAsync();
            if(minSalary > maxSalary)
                doctors = _context.Doctor.Where(d => d.Salary >= minSalary).ToListAsync();

            return await doctors;
        }
        public async Task<IEnumerable<Doctor>> GetDoctorByAge(int minAge, int maxAge)
        {
            
            var doctors = _context.Doctor.Where(d => d.Age >= minAge && d.Age <= maxAge).ToListAsync();
            if(minAge > maxAge)
                doctors = _context.Doctor.Where(d => d.Age >= minAge).ToListAsync();

            return await doctors;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Doctor doctor)
        {
            _context.Update(doctor);
            return Save();
        }
    }
}
