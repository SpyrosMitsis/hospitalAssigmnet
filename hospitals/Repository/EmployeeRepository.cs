using hospitals.Data;
using hospitals.Interfaces;
using hospitals.Models;
using Microsoft.EntityFrameworkCore;

namespace hospitals.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add<T>(T employee) where T : Employee
        {
            _context.Add(employee);
            return Save();
        }

        public bool Delete<T>(T employee) where T : Employee
        {
            _context.Remove(employee);
            return Save();
        }
        public async Task<IEnumerable<T>> GetAll<T>() where T : Employee
        {
            var employees = await _context.Set<T>().ToListAsync();
            return employees;
        }
        public async Task<T> GetByIdAsync<T>(int id) where T : Employee
        {
            var employee = _context.Set<T>()
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            return await employee;
        }

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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update<T>(T employee) where T : Employee
        {
            _context.Update(employee);
            return Save();
        }

        public async Task<int> MaxAgeAsync<T>() where T : Employee
        {
            var age = _context.Set<T>().MaxAsync(e => e.Age);
            return await age;
        }
        public async Task<int> MinAgeAsync<T>() where T : Employee
        {
            var age = _context.Set<T>().MinAsync(e => e.Age);
            return await age;
        }
        public async Task<double> MaxSalaryAsync<T>() where T : Employee
        {
            var salary = _context.Set<T>().MaxAsync(e => e.Salary);
            return await salary;
        }
        public async Task<double> MinSalaryAsync<T>() where T : Employee
        {
            var salary = _context.Set<T>().MinAsync(e => e.Salary);
            return await salary;
        }
    }
}
