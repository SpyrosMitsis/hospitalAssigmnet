using hospitals.Models;
using hospitals.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Logging;

namespace hospitals.Controllers
{
    public class DoctorController : Controller
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILogger<DoctorController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DoctorController"/> class.
    /// </summary>
    /// <param name="employeeRepository">The employee repository.</param>
    /// <param name="doctorRepository">The doctor repository.</param>
    /// <param name="logger">The logger.</param>
    public DoctorController(IEmployeeRepository employeeRepository, IDoctorRepository doctorRepository, ILogger<DoctorController> logger)
    {
        _doctorRepository = doctorRepository;
        _employeeRepository = employeeRepository;
        _logger = logger;
    }

    /// <summary>
    /// Displays the list of doctors.
    /// </summary>
    /// <returns>The task that represents the asynchronous operation.</returns>
    public async Task<IActionResult> Index()
    {
        var salaryMin = Request.Query["salary_min"];
        var salaryMax = Request.Query["salary_max"];
        var minAge = Request.Query["older_than"];
        var maxAge = Request.Query["younger_than"];

        var doctors = await _employeeRepository.GetAll<Doctor>();

        ViewBag.MaxAge = await _employeeRepository.MaxAgeAsync<Doctor>();
        ViewBag.MinAge = await _employeeRepository.MinAgeAsync<Doctor>();
        ViewBag.MaxSalary = await _employeeRepository.MaxSalaryAsync<Doctor>();
        ViewBag.MinSalary = await _employeeRepository.MinSalaryAsync<Doctor>();

        // Filter by Salary
        if (!string.IsNullOrEmpty(salaryMin) && !string.IsNullOrEmpty(salaryMax))
        {
            var minSalary = float.Parse(salaryMin);
            var maxSalary = float.Parse(salaryMax);
            doctors = await _employeeRepository.GetEmployeeBySalary<Doctor>(minSalary, maxSalary);
        }

        // Filter by age
        if (!string.IsNullOrEmpty(minAge) && !string.IsNullOrEmpty(maxAge))
        {
            var olderThan = int.Parse(minAge);
            var youngerThan = int.Parse(maxAge);
            doctors = await _employeeRepository.GetEmployeeByAge<Doctor>(olderThan, youngerThan);
        }

        // Log the successful display of doctors
        _logger.LogInformation("{TimeNow} Doctor Shown Correctly", DateTime.Now);

        return View(doctors);
    }

    /// <summary>
    /// Displays the details of a specific doctor.
    /// </summary>
    /// <param name="id">The ID of the doctor.</param>
    /// <returns>The task that represents the asynchronous operation.</returns>
    public async Task<IActionResult> Detail(int id)
    {
        Doctor doctor = await _doctorRepository.GetByIdAsync(id);
        return View(doctor);
    }
}

}
