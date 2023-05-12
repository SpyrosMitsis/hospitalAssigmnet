using hospitals.Models;

namespace hospitals.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<T>> GetAll<T>() where T : Employee;
        Task<T> GetByIdAsync<T>(int id) where T : Employee;
        Task<IEnumerable<T>> GetEmployeeByAge<T>(int minAge, int maxAge) where T : Employee;
        Task<IEnumerable<T>> GetEmployeeBySalary<T>(float minSalary, float maxSalary) where T : Employee;
        Task<int> MaxAgeAsync<T>() where T : Employee;
        Task<int> MinAgeAsync<T>() where T : Employee;
        Task<double> MaxSalaryAsync<T>() where T : Employee;
        Task<double> MinSalaryAsync<T>() where T : Employee;
        bool Save();
        bool Update<T>(T employee) where T : Employee;
        bool Add<T>(T employee) where T : Employee;
        bool Delete<T>(T employee) where T : Employee;
    }
}
