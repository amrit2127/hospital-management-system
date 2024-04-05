using HospitalManagementSystem.Data.Repository;
using HospitalManagementSystem.Data.Repository.IRepository;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private Dictionary<string, int> bookingDates = new Dictionary<string, int>();
        private const int maxCapacity = 10;
        private readonly IConfiguration _configuration;
        public BookingController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            Booking bookingForm = new Booking();
            return View(bookingForm);
        }
        [HttpPost]
        public async Task<IActionResult> Index(int itemid, [Bind] Booking bookingForm)
        {
            if (!ModelState.IsValid)
            {
                if (bookingForm == null) return NotFound();
                if (ModelState.IsValid) return View(bookingForm);

                if (bookingForm.Id == 0)
                {
                    bookingForm.DoctorId = itemid;
                    bookingForm.Total = bookingForm.AdultCount * bookingForm.Adult +
                          bookingForm.ChildrenCount * bookingForm.Children;

                    bookingForm.TotalCount = bookingForm.AdultCount + bookingForm.ChildrenCount;

                    TempData["Total"] = bookingForm.Total;
                }
                return RedirectToAction(nameof(BookingConfirmation), new { total = bookingForm.Total });

            }
            else
                return View(bookingForm);
        }
        public IActionResult BookingConfirmation(int id, DateTime orderdate)
        {           
            int total = 0;
            if (TempData["Total"] != null)
            {
                total = (int)TempData["Total"];
            }
            return View(total);
        }
    }
}
