using hospitals.Interfaces;
using hospitals.Models;
using Microsoft.AspNetCore.Mvc;

namespace hospitals.Controllers
{
    public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeController"/> class.
    /// </summary>
    /// <param name="employeeRepository">The employee repository.</param>
    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    /// <summary>
    /// Displays the list of employees.
    /// </summary>
    /// <returns>The task that represents the asynchronous operation.</returns>
    public async Task<IActionResult> Index()
    {
        var salaryMin = Request.Query["salary_min"];
        var salaryMax = Request.Query["salary_max"];
        var minAge = Request.Query["older_than"];
        var maxAge = Request.Query["younger_than"];

        var employees = await _employeeRepository.GetAll<Employee>();

        ViewBag.MaxAge = await _employeeRepository.MaxAgeAsync<Employee>();
        ViewBag.MinAge = await _employeeRepository.MinAgeAsync<Employee>();
        ViewBag.MaxSalary = await _employeeRepository.MaxSalaryAsync<Employee>();
        ViewBag.MinSalary = await _employeeRepository.MinSalaryAsync<Employee>();
        
        // Filter by Salary
        if (!string.IsNullOrEmpty(salaryMin) && !string.IsNullOrEmpty(salaryMax))
        {
            var minSalary = float.Parse(salaryMin);
            var maxSalary = float.Parse(salaryMax);
            employees = await _employeeRepository.GetEmployeeBySalary<Employee>(minSalary, maxSalary);
        }

        // Filter by age
        if (!string.IsNullOrEmpty(minAge) && !string.IsNullOrEmpty(maxAge))
        {
            var olderThan = int.Parse(minAge);
            var youngerThan = int.Parse(maxAge);
            employees = await _employeeRepository.GetEmployeeByAge<Employee>(youngerThan, olderThan);
        }

        return View(employees);
    }
}

}

