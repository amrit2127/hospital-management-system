using HospitalManagementSystem.Data.Repository.IRepository;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HospitalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HospitalController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
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
            var hospitalList = _unitOfWork.Hospital.GetAll(includeProperties: "HospitalType");
            return Json(new { data = hospitalList });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var hospital = _unitOfWork.Hospital.Get(id);
            if (hospital == null)
                return Json(new { success = false, message = "Something went wrong while deleting data!!!" });
            var webRootPath = _webHostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, hospital.ImageUrl.Trim('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _unitOfWork.Hospital.Remove(hospital);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Data deleted successfully!!!" });

        }
        #endregion

        public IActionResult Upsert(int? id)
        {
            HospitalVM hospitalVM = new HospitalVM()
            {
                HospitalTypeList = _unitOfWork.HospitalType.GetAll().Select(cl => new SelectListItem()
                {
                    Text = cl.Type,
                    Value = cl.Id.ToString(),
                }),
                Hospital = new Hospital()
            };
            if (id == null) return View(hospitalVM);  //Create
            //Edit
            hospitalVM.Hospital = _unitOfWork.Hospital.Get(id.GetValueOrDefault());
            return View(hospitalVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(HospitalVM hospitalVM)
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
                    if (hospitalVM.Hospital.Id != 0)
                    {
                        var imageExists = _unitOfWork.Hospital.Get(hospitalVM.Hospital.Id).ImageUrl;
                        hospitalVM.Hospital.ImageUrl = imageExists;
                    }
                    if (hospitalVM.Hospital.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, hospitalVM.Hospital.ImageUrl.Trim('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    hospitalVM.Hospital.ImageUrl = @"\images\products\" + filename + extension;
                }
                else
                {
                    if (hospitalVM.Hospital.Id != 0)
                    {
                        var imageExists = _unitOfWork.Hospital.Get(hospitalVM.Hospital.Id).ImageUrl;
                        hospitalVM.Hospital.ImageUrl = imageExists;
                    }

                }
                if (hospitalVM.Hospital.Id == 0)
                {
                    _unitOfWork.Hospital.Add(hospitalVM.Hospital);
                }
                else
                {
                    _unitOfWork.Hospital.Update(hospitalVM.Hospital);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                hospitalVM = new HospitalVM()
                {
                    HospitalTypeList = _unitOfWork.HospitalType.GetAll().Select(cl => new SelectListItem()
                    {
                        Text = cl.Type,
                        Value = cl.Id.ToString(),
                    }),
                    Hospital = new Hospital()

                };
                if (hospitalVM.Hospital.Id != 0)
                {
                    hospitalVM.Hospital = _unitOfWork.Hospital.Get(hospitalVM.Hospital.Id);
                }
                return View(hospitalVM);
            }
        }
    }
}
