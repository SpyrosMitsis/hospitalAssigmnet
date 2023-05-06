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
        public bool Add(Employee employee)
        {
            _context.Add(employee);
            return Save();
        }

        public bool Delete(Employee employee)
        {
            _context.Remove(employee);
            return Save();
        }
        public async Task<IEnumerable<Employee>> GetAll()
        {
            var employee = await _context.Employee.ToListAsync();
            return employee;
        }
        public async Task<Employee> GetByIdAsync(int id)
        {
            var employee = _context.Employee
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            return await employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeBySalary(float minSalary, float maxSalary)
        {
            var employees = _context.Employee
                .Where(d => d.Salary >= minSalary && d.Salary <= maxSalary)
                .ToListAsync();

            if(minSalary > maxSalary)
                employees = _context.Employee
                    .Where(d => d.Salary >= minSalary)
                    .ToListAsync();

            return await employees;
        }
        public async Task<IEnumerable<Employee>> GetEmployeeByAge(int minAge, int maxAge)
        {
            
            var employees = _context.Employee
                .Where(d => d.Age >= minAge && d.Age <= maxAge)
                .ToListAsync();
            if(minAge > maxAge)
                employees = _context.Employee
                    .Where(d => d.Age >= minAge)
                    .ToListAsync();

            return await employees;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Employee employee)
        {
            _context.Update(employee);
            return Save();
        }
    }
}
