using hospitals.Data;
using hospitals.Interfaces;
using hospitals.Models;
using Microsoft.EntityFrameworkCore;

namespace hospitals.Repository
{
/// <summary>
/// Repository class for managing Employee objects in the database
/// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
    
        /// <summary>
        /// Constructor for the EmployeeRepository class
        /// </summary>
        /// <param name="context">The ApplicationDbContext to use for database operations</param>
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    
        /// <summary>
        /// Adds a new employee object to the database
        /// </summary>
        /// <typeparam name="T">The type of employee object to add (must inherit from Employee)</typeparam>
        /// <param name="employee">The employee object to add to the database</param>
        /// <returns>True if the employee object was successfully added, false otherwise</returns>
        public bool Add<T>(T employee) where T : Employee
        {
            _context.Add(employee);
            return Save();
        }
    
        /// <summary>
        /// Deletes an existing employee object from the database
        /// </summary>
        /// <typeparam name="T">The type of employee object to delete (must inherit from Employee)</typeparam>
        /// <param name="employee">The employee object to delete from the database</param>
        /// <returns>True if the employee object was successfully deleted, false otherwise</returns>
        public bool Delete<T>(T employee) where T : Employee
        {
            _context.Remove(employee);
            return Save();
        }
    
        /// <summary>
        /// Retrieves all employee objects from the database
        /// </summary>
        /// <typeparam name="T">The type of employee object to retrieve (must inherit from Employee)</typeparam>
        /// <returns>A collection of all employee objects of the specified type</returns>
        public async Task<IEnumerable<T>> GetAll<T>() where T : Employee
        {
            var employees = await _context.Set<T>().ToListAsync();
            return employees;
        }
    
        /// <summary>
        /// Retrieves an employee object from the database by its ID
        /// </summary>
        /// <typeparam name="T">The type of employee object to retrieve (must inherit from Employee)</typeparam>
        /// <param name="id">The ID of the employee object to retrieve</param>
        /// <returns>The employee object with the specified ID, or null if no such object exists</returns>
        public async Task<T> GetByIdAsync<T>(int id) where T : Employee
        {
            var employee = _context.Set<T>()
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();
    
            return await employee;
        }
        /// <summary>
        /// Gets all employees from the database within a specified salary range.
        /// </summary>
        /// <typeparam name="T">A class that inherits from the Employee class.</typeparam>
        /// <param name="minSalary">The minimum salary of the employees to be retrieved.</param>
        /// <param name="maxSalary">The maximum salary of the employees to be retrieved.</param>
        /// <returns>An IEnumerable of employees.</returns>
        public async Task<IEnumerable<T>> GetEmployeeBySalary<T>(float minSalary, float maxSalary) where T : Employee
        {

            if(minSalary > maxSalary)
            {
                var employees = _context.Set<T>()
                    .Where(d => d.Salary >= minSalary)
                    .ToListAsync();
                    return await employees;
            }
            else
            {
                var employees = _context.Set<T>()
                    .Where(d => d.Salary >= minSalary && d.Salary <= maxSalary)
                    .ToListAsync();
                    return await employees;
            }

        }
        /// <summary>
        /// Retrieves a collection of employees of type T whose age falls within a given range.
        /// </summary>
        /// <typeparam name="T">The type of employee to retrieve.</typeparam>
        /// <param name="minAge">The minimum age of employees to retrieve.</param>
        /// <param name="maxAge">The maximum age of employees to retrieve.</param>
        /// <returns>An asynchronous operation that returns a collection of employees of type T.</returns>
        public async Task<IEnumerable<T>> GetEmployeeByAge<T>(int minAge, int maxAge) where T : Employee
        {
        
            if(minAge > maxAge)
            {
                var employees = _context.Set<T>()
                    .Where(d => d.Age >= minAge)
                    .ToListAsync();
                    return await employees;
            }
            else
            {
                var employees = _context.Set<T>()
                    .Where(d => d.Age >= minAge && d.Age <= maxAge)
                    .ToListAsync();
        
                return await employees;
            }
        }
        /// <summary>
        /// Saves changes made to the context to the database.
        /// </summary>
        /// <returns>True if changes were saved, false otherwise.</returns>
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        /// <summary>
        /// Updates an employee of type T in the context.
        /// </summary>
        /// <typeparam name="T">The type of employee to update.</typeparam>
        /// <param name="employee">The employee to update.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public bool Update<T>(T employee) where T : Employee
        {
            _context.Update(employee);
            return Save();
        }
        
        /// <summary>
        /// Retrieves the maximum age of employees of type T from the context.
        /// </summary>
        /// <typeparam name="T">The type of employee to retrieve the maximum age for.</typeparam>
        /// <returns>An asynchronous operation that returns the maximum age as an integer.</returns>
        public async Task<int> MaxAgeAsync<T>() where T : Employee
        {
            var age = _context.Set<T>().MaxAsync(e => e.Age);
            return await age;
        }
        
        /// <summary>
        /// Retrieves the minimum age of employees of type T from the context.
        /// </summary>
        /// <typeparam name="T">The type of employee to retrieve the minimum age for.</typeparam>
        /// <returns>An asynchronous operation that returns the minimum age as an integer.</returns>
        public async Task<int> MinAgeAsync<T>() where T : Employee
        {
            var age = _context.Set<T>().MinAsync(e => e.Age);
            return await age;
        }
        
        /// <summary>
        /// Retrieves the maximum salary of employees of type T from the context.
        /// </summary>
        /// <typeparam name="T">The type of employee to retrieve the maximum salary for.</typeparam>
        /// <returns>An asynchronous operation that returns the maximum salary as a double.</returns>
        public async Task<double> MaxSalaryAsync<T>() where T : Employee
        {
            var salary = _context.Set<T>().MaxAsync(e => e.Salary);
            return await salary;
        }
        
        /// <summary>
        /// Retrieves the minimum salary of employees of type T from the context.
        /// </summary>
        /// <typeparam name="T">The type of employee to retrieve the minimum salary for.</typeparam>
        /// <returns>An asynchronous operation that returns the minimum salary as a double.</returns>
        public async Task<double> MinSalaryAsync<T>() where T : Employee
        {
            var salary = _context.Set<T>().MinAsync(e => e.Salary);
            return await salary;
        }
    }
}
