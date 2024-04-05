using HospitalManagementSystem.Data.Repository.IRepository;
using HospitalManagementSystem.Models.ViewModels;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using HospitalManagementSystem.Utility;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
        
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DoctorController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var doctorList = _unitOfWork.Doctor.GetAll(includeProperties: "Department");
            return Json(new { data = doctorList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var doctor = _unitOfWork.Doctor.Get(id);
            if (doctor == null)
                return Json(new { success = false, message = "Something went wrong while deleting data!!!" });
            var webRootPath = _webHostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, doctor.ImageUrl.Trim('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _unitOfWork.Doctor.Remove(doctor);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Data deleted successfully!!!" });

        }


        #endregion

        public IActionResult Upsert(int? id)
        {
            DoctorVM doctorVM = new DoctorVM()
            {
                DepartmentList = _unitOfWork.Department.GetAll().Select(cl => new SelectListItem()
                {
                    Text = cl.Name,
                    Value = cl.Id.ToString(),
                }),
                Doctor = new Doctor()
            };
            if (id == null) return View(doctorVM);  //Create
            //Edit
            doctorVM.Doctor = _unitOfWork.Doctor.Get(id.GetValueOrDefault());
            return View(doctorVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(DoctorVM doctorVM)
        {
            if (ModelState.IsValid)
            {
                var webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0)
                {
                    var filename = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);
                    var uploads = Path.Combine(webRootPath, @"images\products");
                    if (doctorVM.Doctor.Id != 0)
                    {
                        var imageExists = _unitOfWork.Hospital.Get(doctorVM.Doctor.Id).ImageUrl;
                        doctorVM.Doctor.ImageUrl = imageExists;
                    }
                    if (doctorVM.Doctor.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, doctorVM.Doctor.ImageUrl.Trim('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    doctorVM.Doctor.ImageUrl = @"\images\products\" + filename + extension;
                }
                else
                {
                    if (doctorVM.Doctor.Id != 0)
                    {
                        var imageExists = _unitOfWork.Hospital.Get(doctorVM.Doctor.Id).ImageUrl;
                        doctorVM.Doctor.ImageUrl = imageExists;
                    }

                }
                if (doctorVM.Doctor.Id == 0)
                {
                    _unitOfWork.Doctor.Add(doctorVM.Doctor);
                }
                else
                {
                    _unitOfWork.Doctor.Update(doctorVM.Doctor);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                doctorVM = new DoctorVM()
                {
                    DepartmentList = _unitOfWork.Department.GetAll().Select(cl => new SelectListItem()
                    {
                        Text = cl.Name,
                        Value = cl.Id.ToString(),
                    }),
                    Doctor = new Doctor()

                };
                if (doctorVM.Doctor.Id != 0)
                {
                    doctorVM.Doctor = _unitOfWork.Doctor.Get(doctorVM.Doctor.Id);
                }
                return View(doctorVM);
            }
        }


    }
}
