using hospitals.Models;
using hospitals.Data;
using hospitals.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hospitals.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;
    
        /// <summary>
        /// Initializes a new instance of the <see cref="PatientRepository"/> class.
        /// </summary>
        /// <param name="context">The application DbContext.</param>
        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    
        /// <summary>
        /// Adds a patient to the repository.
        /// </summary>
        /// <param name="patient">The patient to add.</param>
        /// <returns>True if the patient was added successfully, false otherwise.</returns>
        public bool Add(Patient patient)
        {
            _context.Add(patient);
            return Save();
        }
    
        /// <summary>
        /// Deletes a patient from the repository.
        /// </summary>
        /// <param name="patient">The patient to delete.</param>
        /// <returns>True if the patient was deleted successfully, false otherwise.</returns>
        public bool Delete(Patient patient)
        {
            _context.Remove(patient);
            return Save();
        }
    
        /// <summary>
        /// Gets all patients from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of patients.</returns>
        public async Task<IEnumerable<Patient>> GetAll()
        {
            var patients = await _context.Patient
                .Include(p => p.Room)
                .OrderBy(p => p.PatientId)
                .ToListAsync();
            return patients;
        }
    
        /// <summary>
        /// Gets all patients with their addresses from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of patients with addresses.</returns>
        public async Task<IEnumerable<Patient>> GetAllWithAddresses()
        {
            var patients = await _context.Patient
                .Include(p => p.Room)
                .Include(p => p.Address)
                .OrderBy(p => p.PatientId)
                .ToListAsync();
            return patients;
        }
    
        /// <summary>
        /// Gets all patients grouped by address from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of patients grouped by address.</returns>
        public async Task<IEnumerable<IGrouping<Address, Patient>>> GetAllByAddress()
        {
            var patients = await _context.Patient
                .Include(p => p.Address)
                .OrderBy(p => p.Address.Name)
                .ToListAsync();
            return patients.GroupBy(p => p.Address);
        }
    
        /// <summary>
        /// Gets all patients grouped by doctor from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of patients grouped by doctor.</returns>
        public async Task<IEnumerable<IGrouping<Doctor, Patient>>> GetAllByDoctor()
        {
            var patients = await _context.Patient
                .Include(p => p.Doctor)
                .OrderBy(p => p.Doctor.Id)
                .ToListAsync();
            return patients.GroupBy(p => p.Doctor);
        }
    
        /// <summary>
        /// Gets all patients grouped by room from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of patients grouped by room.</returns>
        public async Task<IEnumerable<IGrouping<Room, Patient>>> GetAllByRoom()
        {
            var patients = await _context.Patient
                .Include(p => p.Room)
                .OrderBy(p => p.Room.RoomName)
                .ToListAsync();
            return patients.GroupBy(p => p.Room);
        }
    
        /// <summary>
        /// Gets patients within a specified age range from the repository.
        /// </summary>
        /// <param name="minAge">The minimum age of the patients.</param>
        /// <param name="maxAge">The maximum age of the patients.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of patients within the age range.</returns>
        public async Task<IEnumerable<Patient>> GetPatientByAge(int minAge, int maxAge)
        {
            var patients = _context.Patient
                .Where(d => d.Age >= minAge && d.Age <= maxAge)
                .ToListAsync();
    
            if (minAge > maxAge)
                patients = _context.Patient
                    .Where(d => d.Age >= minAge)
                    .ToListAsync();
    
            return await patients;
        }
    
        /// <summary>
        /// Gets patients within a specified date range from the repository.
        /// </summary>
        /// <param name="entryDate">The entry date of the patients.</param>
        /// <param name="exitDate">The exit date of the patients.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of patients within the date range.</returns>
        public async Task<IEnumerable<Patient>> GetPatientByDate(DateTime entryDate, DateTime exitDate)
        {
            var patients = _context.Patient
                .Where(d => d.EntryDate >= entryDate && d.ExitDate <= exitDate)
                .ToListAsync();
    
            if (entryDate > exitDate)
                patients = _context.Patient
                    .Where(d => d.EntryDate >= entryDate)
                    .ToListAsync();
    
            return await patients;
        }
    
        /// <summary>
        /// Gets a patient by their ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the patient to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the patient.</returns>
        public async Task<Patient> GetByIdAsync(int id)
        {
            var patient = _context.Patient
                .Where(p => p.PatientId == id)
                .FirstOrDefaultAsync();
            return await patient;
        }
    
        /// <summary>
        /// Gets the maximum age of patients in the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the maximum age.</returns>
        public async Task<int> GetMaxAgeAsync()
        {
            var age = await _context.Patient.MaxAsync(p => p.Age);
            return age;
        }
    
        /// <summary>
        /// Gets the minimum age of patients in the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the minimum age.</returns>
        public async Task<int> GetMinAgeAsync()
        {
            var age = await _context.Patient.MinAsync(p => p.Age);
            return age;
        }
    
        /// <summary>
        /// Saves changes made to the repository.
        /// </summary>
        /// <returns>True if changes were saved successfully, false otherwise.</returns>
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    
        /// <summary>
        /// Updates a patient in the repository.
        /// </summary>
        /// <param name="patient">The patient to update.</param>
        /// <returns>True if the patient was updated successfully, false otherwise.</returns>
        public bool Update(Patient patient)
        {
            _context.Update(patient);
            return Save();
        }
    }
}
