using HospitalManagementSystem.Data.Repository.IRepository;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace HospitalManagementSystem.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string search, string searchBy)
        {
            //var claimIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //var hospitalList = _unitOfWork.Hospital.GetAll(includeProperties: "HospitalType");
           
            //if (!string.IsNullOrEmpty(searchBy))
            //{
            //    searchBy = searchBy.ToLower();
            //    if (search == "Name")
            //    {
            //        hospitalList = hospitalList.Where(s => s.Name.ToLower().Contains(searchBy));
            //    }
            //    else if (search == "City")
            //    {
            //        hospitalList = hospitalList.Where(s => s.City.ToLower().Contains(searchBy));
            //    }
            //    else if (search == "State")
            //    {
            //        hospitalList = hospitalList.Where(s => s.State.ToLower().Contains(searchBy));
            //    }
            //    else
            //    {
            //        hospitalList = hospitalList.Where(m => m.Name.ToLower().Contains(searchBy) || m.City.ToLower().Contains(searchBy) || m.State.ToLower().Contains(searchBy));
            //    }
            //}

            return View();
        }

        public IActionResult Explore(string search, string searchBy)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var hospitalList = _unitOfWork.Hospital.GetAll(includeProperties: "HospitalType");
            if (!string.IsNullOrEmpty(searchBy))
            {
                searchBy = searchBy.ToLower();
                if (search == "Name")
                {
                    hospitalList = hospitalList.Where(s => s.Name.ToLower().Contains(searchBy));
                }
                else if (search == "City")
                {
                    hospitalList = hospitalList.Where(s => s.City.ToLower().Contains(searchBy));
                }
                else if (search == "State")
                {
                    hospitalList = hospitalList.Where(s => s.State.ToLower().Contains(searchBy));
                }
                else
                {
                    hospitalList = hospitalList.Where(m => m.Name.ToLower().Contains(searchBy) || m.City.ToLower().Contains(searchBy) || m.State.ToLower().Contains(searchBy));
                }
            }
            return View(hospitalList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Details(int id)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
       
            var hospital = _unitOfWork.Hospital.FirstOrDefault(x => x.Id == id,
                includeProperties: "HospitalType");
            if (hospital == null) return NotFound();
            
            return View(hospital);
        }

        public IActionResult Departments(string search)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //  var department = _unitOfWork.Department.FirstOrDefault(x => x.Id == id,includeProperties: "Hospital");
            var department = _unitOfWork.Department.GetAll(includeProperties: "Hospital");

            if (!string.IsNullOrEmpty(search))
            {
                department = department.Where(m => m.Name.Contains(search));
            }

            if (department == null) return NotFound();

            return View(department);
        }

        public IActionResult Doctors(string search)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //  var department = _unitOfWork.Department.FirstOrDefault(x => x.Id == id,includeProperties: "Hospital");
            var doctor = _unitOfWork.Doctor.GetAll(includeProperties: "Department");

            if (!string.IsNullOrEmpty(search))
            {
                doctor = doctor.Where(m => m.Name.Contains(search));
            }

            if (doctor == null) return NotFound();

            return View(doctor);
        }
    }
}
