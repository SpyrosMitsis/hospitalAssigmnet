using hospitals.Models;

namespace hospitals.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<IEnumerable<Employee>> GetEmployeeByAge(int youngerThan, int olderThan);
        Task<IEnumerable<Employee>> GetEmployeeBySalary(float minSalary, float maxSalary);
    }
}
