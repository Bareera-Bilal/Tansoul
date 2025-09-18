// using System;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using P1WEBMVC.Models;

// namespace P1WEBMVC.Controllers;


// public class AppointmentController : Controller
// {
//    public IActionResult BookAppointment()
// {
//     return View(); // Will try to return "BookAppointment.cshtml"
// }



// [HttpPost]
// public IActionResult ConfirmAppointment(Appointment appointment)
// {
//     // Save appointment to database
//     sqlDbContext.Appointments.Add(appointment);
//     sqlDbContext.SaveChanges();

//     // Redirect to dashboard or return a success response
//     return Json(new { success = true });
// }




// }



using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P1WEBMVC.Data;
using P1WEBMVC.Models;

namespace P1WEBMVC.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly SqlDbContext sqlDbContext;

        public AppointmentController(SqlDbContext sqlDbContext)
        {
           this.sqlDbContext = sqlDbContext;
        }

        public IActionResult BookAppointment()
        {
            return View(); // Returns "BookAppointment.cshtml"
        }

//         [HttpPost]
// public async Task<IActionResult> Book(Appointment appointment)
// {
//     if (ModelState.IsValid)
//     {
//         appointment.AppointmentDate = DateTime.UtcNow; // or your logic
//         sqlDbContext.Appointments.Add(appointment);
//         await sqlDbContext.SaveChangesAsync();

//         // Redirect to dashboard with appointment ID (optional)
//         return RedirectToAction("PatientDashboard", "User", new { patientId = appointment.PatientId });
//     }

//     return View(appointment);
// }


    }
}
