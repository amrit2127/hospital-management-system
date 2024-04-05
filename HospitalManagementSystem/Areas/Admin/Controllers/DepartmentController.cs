using HospitalManagementSystem.Data.Repository.IRepository;
using HospitalManagementSystem.Models.ViewModels;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DepartmentController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
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
            var departmentList = _unitOfWork.Department.GetAll(includeProperties: "Hospital");
            return Json(new { data = departmentList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var department = _unitOfWork.Department.Get(id);
            if (department == null)
                return Json(new { success = false, message = "Something went wrong while deleting data!!!" });
            var webRootPath = _webHostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, department.ImageUrl.Trim('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _unitOfWork.Department.Remove(department);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Data deleted successfully!!!" });

        }
        #endregion

        public IActionResult Upsert(int? id)
        {
            DepartmentVM departmentVM = new DepartmentVM()
            {
                HospitalList = _unitOfWork.Hospital.GetAll().Select(cl => new SelectListItem()
                {
                    Text = cl.Name,
                    Value = cl.Id.ToString(),
                }),
                Department = new Department()
            };
            if (id == null) return View(departmentVM);  //Create
            //Edit
            departmentVM.Department = _unitOfWork.Department.Get(id.GetValueOrDefault());
            return View(departmentVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(DepartmentVM departmentVM)
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
                    if (departmentVM.Department.Id != 0)
                    {
                        var imageExists = _unitOfWork.Hospital.Get(departmentVM.Department.Id).ImageUrl;
                        departmentVM.Department.ImageUrl = imageExists;
                    }
                    if (departmentVM.Department.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, departmentVM.Department.ImageUrl.Trim('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    departmentVM.Department.ImageUrl = @"\images\products\" + filename + extension;
                }
                else
                {
                    if (departmentVM.Department.Id != 0)
                    {
                        var imageExists = _unitOfWork.Hospital.Get(departmentVM.Department.Id).ImageUrl;
                        departmentVM.Department.ImageUrl = imageExists;
                    }

                }
                if (departmentVM.Department.Id == 0)
                {
                    _unitOfWork.Department.Add(departmentVM.Department);
                }
                else
                {
                    _unitOfWork.Department.Update(departmentVM.Department);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                departmentVM = new DepartmentVM()
                {
                    HospitalList = _unitOfWork.Hospital.GetAll().Select(cl => new SelectListItem()
                    {
                        Text = cl.Name,
                        Value = cl.Id.ToString(),
                    }),
                    Department = new Department()

                };
                if (departmentVM.Department.Id != 0)
                {
                    departmentVM.Department = _unitOfWork.Department.Get(departmentVM.Department.Id);
                }
                return View(departmentVM);
            }
        }
    }
}
