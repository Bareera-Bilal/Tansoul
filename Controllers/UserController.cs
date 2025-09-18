//User Controller → Handles user authentication and profile management.
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P1WEBMVC.Data;
using P1WEBMVC.Interfaces;
using P1WEBMVC.Models;
using P1WEBMVC.Models.ViewModels;
using P1WEBMVC.Types;

namespace P1WEBMVC.Controllers
{




    public class UserController : Controller
    {
        private readonly SqlDbContext sqlDbContext;    // encapsulated feilds
        private readonly ITokenService tokenService;
        private readonly IMailService mailService;

        public UserController(SqlDbContext sqlDbContext, ITokenService tokenService, IMailService mailService)
        {
            this.sqlDbContext = sqlDbContext;
            this.tokenService = tokenService;
            this.mailService = mailService;
        }






        [HttpGet]
        public async Task<ActionResult> PatientDashboard(Guid patientId)
        {
            // Fetch patient data based on the provided ID
            var patient = await sqlDbContext.Patients.FindAsync(patientId);

            if (patient == null)
            {
                return NotFound(); // Optional: handle case when patient doesn't exist
            }

            // Create and populate the ViewModel (DTO)
            var patientViewModel = new PatientView
            {
                Patient = patient
            };

            // Return the view with the data
            return View(patientViewModel);
        }




//  [HttpGet]
// public async Task<ActionResult> EditProfile(Guid patientId)
// {
//     var patient = await sqlDbContext.Patients.FindAsync(patientId);
//     if (patient == null) return NotFound();

//     var model = new PatientEditView
//     {
//         PatientId = patient.PatientId,
//         Username = patient.Username,
//         Email = patient.Email,
//         ProfilePicUrl = patient.ProfilePicUrl,
//         PhoneNumber = patient.PhoneNumber
//     };

//     return View(model);
// }








// asdfgh



//         [HttpGet]
// public async Task<ActionResult> EditProfile(Guid patientId)
// {
//     var patient = await sqlDbContext.Patients.FindAsync(patientId);
//     if (patient == null) return NotFound();

//     var model = new PatientEditView
//     {
//         Username = patient.Username,
//         Email = patient.Email,
//        PhoneNumber = patient.PhoneNumber
//     };

//     ViewBag.PatientId = patient.PatientId;
//     return View(model);
// }


















        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterView user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "All details are required!";
                return View(user);
            }

            try
            {
                var existingUser = await sqlDbContext.Patients
                    .FirstOrDefaultAsync(u => u.Email == user.Email);

                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "User already exists.";
                    return View(user);
                }

                var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                var newPatient = new Patient
                {

                    Username = user.Username,
                    Email = user.Email,
                    Password = encryptedPassword,
                    PhoneNumber = user.PhoneNumber // Ensure this is populated in RegisterView
                                                   // Map additional fields if necessary
                };

                await sqlDbContext.Patients.AddAsync(newPatient);
                await sqlDbContext.SaveChangesAsync();

                return RedirectToAction("PatientDashboard", "User", new { patientId = newPatient.PatientId });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View("Error");
            }
        }











        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginView user)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    ViewBag.errorMessage = "All credentials Required!";
                    return View();
                }

                var existingUser = await sqlDbContext.Patients.FirstOrDefaultAsync(u => u.Email == user.Email);


                if (existingUser == null)
                {

                    ViewBag.errorMessage = "User not Found!";
                    return View();

                }

                var checkPass = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);

                if (checkPass)
                {

                    var token = tokenService.CreateToken(existingUser.PatientId, user.Email, existingUser.Username, 60 * 24);

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
                        return RedirectToAction("PatientDashboard", "User", new { patientId = existingUser.PatientId });
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
















        // forgot pass
        public async Task<ActionResult> ForgotPassWord()
        {
            try
            {
                await mailService.SendEmailAsync("bareerabilal03gmail.com", "Forgot Password", "Your otp is 1234", true);
                return RedirectToAction("Login");
            }
            catch (System.Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Error");
            }



        }






        // Logout User
        [HttpGet]
        // public ActionResult Logout()
        // {
        //     try
        //     {
        //         HttpContext.Response.Cookies.Delete("MedicalAppAuthorizationToken");
        //         return RedirectToAction("Login", "User");
        //     }

        //     catch (Exception ex)
        //     {
        //         ViewBag.errorMessage = ex.Message;
        //         return View("Error");
        //     }
        // }


[HttpPost]
public IActionResult Logout()
{
    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return RedirectToAction("Index", "Home");
}



[HttpGet]
        public ActionResult EditProfile()
        {
            return View();
        }






        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<ActionResult> EditProfile(PatientEditView model)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         ViewBag.ErrorMessage = "Please fill out all fields correctly.";
        //         return View(model);
        //     }

        //     var patient = await sqlDbContext.Patients.FindAsync(model.PatientId); // changed Doctors to Patients

        //     if (patient == null)
        //     {
        //         return NotFound();
        //     }

        //     patient.Name = model.Name;
        //     patient.Username = model.Username;
        //     patient.Email = model.Email;
        //     patient.PhoneNumber = model.PhoneNumber; // ensure consistency with model

        //     await sqlDbContext.SaveChangesAsync();

        //     return RedirectToAction("PatientDashboard", "User", new { patientId = model.PatientId });
        // }




    }


}
