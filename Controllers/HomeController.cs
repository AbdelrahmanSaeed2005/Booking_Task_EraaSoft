using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task12_EraaSoft.DataAccess;
using Task12_EraaSoft.Models;

namespace Task12_EraaSoft.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BookAnAppointment(int? specializationId, int page = 1)
        {
            var doctor = _context.Doctors.Include(e => e.Specialization).AsQueryable();

            // Specialization filter
            var specializations = _context.Specializations;

            if(specializationId > 0 && specializationId.HasValue)
            {
                doctor = doctor.Where(e => e.SpecializationID == specializationId);
                ViewData["specializationId"] = specializationId;
            }
            ViewData["listOfSpecializations"] = specializations.ToList();

            // Pagination
            var totalNumberInPages = 2.0;
            var totalNumberOfPages = Math.Ceiling(doctor.Count() / totalNumberInPages);

            if (totalNumberOfPages < page)
                return NotFound();

            doctor = doctor.Skip((page - 1) * (int)totalNumberOfPages).Take((int)totalNumberOfPages);
            ViewBag.totalNumberOfPages = totalNumberOfPages;
            ViewBag.currentPage = page;

            return View(doctor.ToList());
        }

        public IActionResult CompleteAppointment(int doctorId)
        {
            var doctor = _context.Doctors.FirstOrDefault(e => e.DoctorId == doctorId);

            if (doctor is not null)
                return View(doctor);
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult CompleteAppointment(Enrollment enrollment)
        {
            // تأكد إن الدكتور موجود
            var doctor = _context.Doctors.FirstOrDefault(e => e.DoctorId == enrollment.DoctorId);
            if (doctor == null)
                return NotFound();

            // تأكد إن الوقت مش محجوز قبل كده
            var existingAppointment = _context.Enrollments
                .FirstOrDefault(e => e.DoctorId == enrollment.DoctorId &&
                                     e.Date == enrollment.Date &&
                                     e.Time == enrollment.Time);

            if (existingAppointment != null)
            {
                // الوقت محجوز، رجع الصفحة مع رسالة
                ViewBag.ErrorMessage = "This time slot is already booked. Please choose another.";
                return View(doctor);
            }

            // الحجز متاح
            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();

            ViewBag.ShowModal = true;
            return View(doctor);
        }


        public IActionResult Appointments()
        {

            var appointments = _context.Enrollments.Include(e => e.Doctor);

            if (appointments is not null)
            {
              return View(appointments.ToList());  
            }
            return NotFound();
        }


    }
}