using hospitals.Interfaces;
using hospitals.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace hospitals.Controllers
{
    public class NurseController : Controller
    {
        private readonly INurseRepository _nurseRepository;

        public NurseController(INurseRepository nurseRepository)
        {
            _nurseRepository = nurseRepository;

            
        }
        public async Task<IActionResult> Index()
        {

            var salaryMin = Request.Query["salary_min"];
            var salaryMax = Request.Query["salary_max"];
            var minAge = Request.Query["older_than"];
            var maxAge = Request.Query["younger_than"];

            var nurses = await _nurseRepository.GetAll();

            if (!string.IsNullOrEmpty(salaryMin) && !string.IsNullOrEmpty(salaryMax))
            {
                var minSalary = float.Parse(salaryMin);
                var maxSalary = float.Parse(salaryMax);
                nurses = await _nurseRepository.GetNurseBySalary(minSalary, maxSalary);
            }
            if (!string.IsNullOrEmpty(minAge) && !string.IsNullOrEmpty(maxAge))
            {
                var olderThan = int.Parse(minAge);
                var youngerThan = int.Parse(maxAge);
                nurses = await _nurseRepository.GetNurseByAge(youngerThan, olderThan);
            }
            return View(nurses);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Nurse nurse = await _nurseRepository.GetByIdAsync(id);
            return View(nurse);

        }
    }
}
