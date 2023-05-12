using hospitals.Models;
using hospitals.Data;
using hospitals.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hospitals.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Patient patient)
        {
            _context.Add(patient);
            return Save();
        }

        public bool Delete(Patient patient)
        {
            _context.Remove(patient);
            return Save();
        }

        public async Task<IEnumerable<Patient>> GetAll()
        {
            var paitents = _context.Patient
                .Include(p => p.Room)
                .OrderBy(p => p.PatientId)
                .ToListAsync();
            return await paitents;
        }
        public async Task<IEnumerable<Patient>> GetAllWithAddresses()
        {
            var paitents = _context.Patient
                .Include(p => p.Room)
                .Include(p => p.Address)
                .OrderBy(p => p.PatientId)
                .ToListAsync();
            return await paitents;
        }

        public async Task<IEnumerable<IGrouping<Address, Patient>>> GetAllByAddress()
        {
            var patients = await _context.Patient
                .Include(p => p.Address)
                .OrderBy(p => p.Address.Name)
                .ToListAsync();
            return patients.GroupBy(p => p.Address);
        }

        public async Task<IEnumerable<IGrouping<Doctor, Patient>>> GetAllByDoctor()
        {
            var patients = await _context.Patient
                .Include(p => p.Doctor)
                .OrderBy(p => p.Doctor.Id)
                .ToListAsync();
            return patients.GroupBy(p => p.Doctor);
        }

        public async Task<IEnumerable<IGrouping<Room, Patient>>> GetAllByRoom()
        {
            var patients = await _context.Patient
                .Include(p => p.Room)
                .OrderBy(p => p.Room.RoomName)
                .ToListAsync();
            return patients.GroupBy(p => p.Room);
        }

        public async Task<IEnumerable<Patient>> GetPatientByAge(int minAge, int maxAge)
        {
            
            var patients= _context.Patient
                .Where(d => d.Age >= minAge && d.Age <= maxAge)
                .ToListAsync();

            if(minAge > maxAge)
                patients = _context.Patient
                    .Where(d => d.Age >= minAge)
                    .ToListAsync();

            return await patients;
        }
        public async Task<IEnumerable<Patient>> GetPatientByDate(DateTime entryDate, DateTime exitDate)
        {
            
            var patients= _context.Patient
                .Where(d => d.EntryDate >= entryDate && d.ExitDate<= exitDate)
                .ToListAsync();

            if(entryDate > exitDate)
                patients = _context.Patient
                    .Where(d => d.EntryDate>= entryDate)
                    .ToListAsync();

            return await patients;
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            var patient = _context.Patient
                .Where(p => p.PatientId == id)
                .FirstOrDefaultAsync();
            return await patient;
        }
        public async Task<int> GetMaxAgeAsync()
        {
            var age = _context.Patient.MaxAsync(p => p.Age);
            return await age;
        }
        public async Task<int> GetMinAgeAsync()
        {
            var age = _context.Patient.MinAsync(p => p.Age);
            return await age;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Patient patient)
        {
            _context.Update(patient);
            return Save();
        }
    }
}
