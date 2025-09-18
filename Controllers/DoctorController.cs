
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P1WEBMVC.Data;
using P1WEBMVC.Interfaces;
using P1WEBMVC.Models;
using P1WEBMVC.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace P1WEBMVC.Controllers
{
    public class DoctorController : Controller
    {


        private readonly SqlDbContext sqlDbContext;
        private readonly ITokenService tokenService;


        public DoctorController(ITokenService tokenService, SqlDbContext sqlDbContext)
        {
            this.tokenService = tokenService;
            this.sqlDbContext = sqlDbContext;
        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Profile()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


 [HttpGet]
public async Task<ActionResult> EditProfile(Guid doctorId)
{
    var doctor = await sqlDbContext.Doctors.FindAsync(doctorId);
    if (doctor == null) return NotFound();

    var model = new DoctorEditView
    {
        Name = doctor.Name,
        Username = doctor.Username,
        Email = doctor.Email,
        Specialization = doctor.Specialization,
        Contact = doctor.Contact
    };

    ViewBag.DoctorId = doctor.DoctorId;
    return View(model);
}




        


        [HttpGet]
        public async Task<ActionResult> AllDoctors()
        {
            var doctors = await sqlDbContext.Doctors.ToListAsync();
            return View(doctors);
        }


// public IActionResult AllDoctors()
// {
//     if (!User.Identity.IsAuthenticated || !User.IsInRole("Patient"))
//     {
//         return RedirectToAction("Login", "User"); // Redirect to User Login
//     }

//     // If logged in and is a patient, show the doctors page
//     return View("AllDoctors");
// }


// [HttpGet]
// public async Task<ActionResult> AllDoctors()
// {
//     if (User.Identity == null || !User.Identity.IsAuthenticated || !User.IsInRole("Patient"))
//     {
//         return RedirectToAction("Login", "User"); // Redirect if not logged in as Patient
//     }

//     var doctors = await sqlDbContext.Doctors.ToListAsync();
//     return View(doctors); // Show doctors list if authenticated
// }














        [HttpGet]
        public async Task<ActionResult> DoctorDashboard(Guid doctorId)
        {
            // Fetch patient data based on the provided ID
            var doctor = await sqlDbContext.Doctors.FindAsync(doctorId);

            if (doctor == null)
            {
                return NotFound(); // Optional: handle case when patient doesn't exist
            }

            // Create and populate the ViewModel (DTO)
            var DoctorViewModel = new DoctorView
            {
                 Doctor = doctor
            };

 


            // Return the view with the data
            return View(DoctorViewModel);
        }







        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(DoctorRegisterView user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "All details are required!";
                return View(user);
            }

            try
            {
                var existingUser = await sqlDbContext.Doctors
                    .FirstOrDefaultAsync(u => u.Email == user.Email);

                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "User already exists.";
                    return View(user);
                }

                var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                var newDoctor = new Doctor
                {
                    // From input field: name="Name"
                    Name = user.Name,                  // Set the required Name property
                    Username = user.Username,          // From input field: name="Username"
                    Email = user.Email,                // From input field: name="Email"
                    Password = encryptedPassword,      // Encrypt before assigning
                    Specialization = user.Specialization, // From input field: name="Specialization"
                    Contact = user.Contact             // From input field: name="Contact"
                };


                await sqlDbContext.Doctors.AddAsync(newDoctor);
                await sqlDbContext.SaveChangesAsync();

                return RedirectToAction("DoctorDashboard", "Doctor", new { DoctorId = newDoctor.DoctorId });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View("Error");
            }
        }







         
       

        [HttpPost]
        public async Task<ActionResult> Login(DoctorLoginView user)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    ViewBag.errorMessage = "All credentials Required!";
                    return View();
                }

                var existingUser = await sqlDbContext.Doctors.FirstOrDefaultAsync(u => u.Email == user.Email);


                if (existingUser == null)
                {

                    ViewBag.errorMessage = "User not Found!";
                    return View();

                }

                var checkPass = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);

                if (checkPass)
                {

                    var token = tokenService.CreateToken(existingUser.DoctorId, user.Email, existingUser.Username, 60 * 24);

                    //    Console.WriteLine(token);

                    HttpContext.Response.Cookies.Append("MedicalAppAuthorizationToken", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false,
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTimeOffset.UtcNow.AddHours(24)
                    });


                    var returnUrl = HttpContext.Session.GetString("ReturnUrl");

                    HttpContext.Session.Remove("ReturnUrl");


                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("DoctorDashboard", "Doctor", new { DoctorId = existingUser.DoctorId });
                    }
                    else
                    {
                        // redirect to return Url 
                        return Redirect(returnUrl);
                    }

                }


                else
                {
                    ViewBag.errorMessage = "PassWord incorrect!";
                    return View();
                }




            }
            catch (Exception ex)
            {

                ViewBag.errorMessage = ex.Message;
                return View("Error");
            }




        }






// [HttpPost]
// [ValidateAntiForgeryToken]
// public async Task<ActionResult> EditProfile(DoctorEditView model)
// {
//     if (!ModelState.IsValid)
//     {
//         ViewBag.ErrorMessage = "Please fill out all fields correctly.";
//         return View(model);
//     }

//     var doctor = await sqlDbContext.Doctors.FindAsync(model.DoctorId);

//     if (doctor == null)
//     {
//         return NotFound();
//     }

//     doctor.Name = model.Name;
//     doctor.Username = model.Username;
//     doctor.Email = model.Email;
//     doctor.Specialization = model.Specialization;
//     doctor.Contact = model.Contact;

//     await sqlDbContext.SaveChangesAsync();

//      return RedirectToAction("DoctorDashboard", "Doctor", new { DoctorId = existingUser.DoctorId });

// }



[HttpPost]
[ValidateAntiForgeryToken]
public async Task<ActionResult> EditProfile(DoctorEditView model)
{
    if (!ModelState.IsValid)
    {
        ViewBag.ErrorMessage = "Please fill out all fields correctly.";
        return View(model);
    }

    var doctor = await sqlDbContext.Doctors.FindAsync(model.DoctorId);

    if (doctor == null)
    {
        return NotFound();
    }

    doctor.Name = model.Name;
    doctor.Username = model.Username;
    doctor.Email = model.Email;
    doctor.Specialization = model.Specialization;
    doctor.Contact = model.Contact;

    await sqlDbContext.SaveChangesAsync();

    return RedirectToAction("DoctorDashboard", "Doctor", new { DoctorId = doctor.DoctorId });
}










    }
}


