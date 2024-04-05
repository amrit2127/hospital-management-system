using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Repository.IRepository;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HospitalTypesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HospitalTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        #region APIs
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var hospitalType=_unitOfWork.HospitalType.GetAll();
            return Json(new { data = hospitalType });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var type = _unitOfWork.HospitalType.Get(id);
            if (type == null)
                return Json(new { success = false, message = "Something Went Wrong while Delete Data!!!" });
            _unitOfWork.HospitalType.Remove(type);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Data Deleted Successfully!!!" });
        }

        #endregion

        public IActionResult Upsert(int? id)
        {
            HospitalType hospitalType = new HospitalType();
            if (id == null) return View(hospitalType);
            hospitalType = _unitOfWork.HospitalType.Get(id.GetValueOrDefault());
            if (hospitalType == null) return NotFound();
            return View(hospitalType);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(HospitalType hospitalType)
        {
            if (hospitalType == null) return NotFound();
            if (!ModelState.IsValid) return View(hospitalType);
            if (hospitalType.Id == 0)
                _unitOfWork.HospitalType.Add(hospitalType);
            else
                _unitOfWork.HospitalType.Update(hospitalType);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }



    }
}
