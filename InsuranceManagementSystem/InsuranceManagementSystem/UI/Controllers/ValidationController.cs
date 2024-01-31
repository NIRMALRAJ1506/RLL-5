using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DAL;
using DAL.Repository;
using DAL.Service;
using UI.Models;

namespace UILayer.Controllers
{
    public class ValidationController : Controller
    {
        private readonly IAdminRepository adminRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly InsuranceDbContext context;

        public ValidationController(IAdminRepository adminRepository, ICustomerRepository customerRepository)
        {
            this.adminRepository = adminRepository;
            this.customerRepository = customerRepository;
            this.context = new InsuranceDbContext();
        }

        // GET: Validation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CustomerReg()
        {
            // Generate and store captcha value in session
            var captchaValue = GenerateAlphanumericCaptcha();
            Session["Captcha"] = captchaValue;

            // Pass captcha value to the view
            var user = new UserView();
            user.CaptchaValue = captchaValue;
            return View(user);
        }
        public ActionResult AdminReg()
        {
            // Generate and store captcha value in session
            var captchaValue = GenerateAlphanumericCaptcha();
            Session["Captcha"] = captchaValue;

            // Pass captcha value to the view
            var user = new UserView();
            user.CaptchaValue = captchaValue;
            return View(user);
        }
        [HttpPost]
        public ActionResult AdminReg(UserView user, string captchaInput)
        {
            // Validate captcha
            if (!ValidateCaptcha(captchaInput))
            {
                ModelState.AddModelError("Captcha", "Captcha verification failed.");
                return View("Registration", user);
            }

            // Check if email or username already registered
            if (adminRepository.AdminExistsEmail(user.Email))
            {
                ModelState.AddModelError("Email", "Email already registered with us.");
                return View("Registration", user);
            }
            else if (adminRepository.AdminExists(user.UserName))
            {
                ModelState.AddModelError("UserName", "Username already registered with us.");
                return View("Registration", user);
            }
            Admin newadmin = new Admin
            {
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                RoleId = 1,
                Password = user.Password,
            };

            adminRepository.CreateAdmin(newadmin);

            return RedirectToAction("Dashboard", "Admin");
        }
        [HttpPost]
        public ActionResult CustomerReg(UserView user, string captchaInput)
        {
            // Validate captcha
            if (!ValidateCaptcha(captchaInput))
            {
                ModelState.AddModelError("Captcha", "Captcha verification failed.");
                return View("Registration", user);
            }

            // Check if email or username already registered
            if (customerRepository.CustomerExistsEmail(user.Email))
            {
                ModelState.AddModelError("Email", "Email already registered with us.");
                return View("Registration", user);
            }
            else if (customerRepository.CustomerExists(user.UserName))
            {
                ModelState.AddModelError("UserName", "Username already registered with us.");
                return View("Registration", user);
            }

            Customer customer = new Customer
            {
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                RoleId = 2,
                Password = user.Password,
            };

            customerRepository.CreateCustomer(customer);

            return RedirectToAction("CustomerLogin", "Validation");
        }

        public ActionResult GenerateCaptchaImage()
        {
            var captchaValue = GenerateAlphanumericCaptcha();

            // Store captcha value in session
            Session["Captcha"] = captchaValue;

            // Create an image of the captcha
            var captchaImage = CaptchaHelper.GenerateCaptchaImage(captchaValue);

            // Convert the image to a byte array and return it as an image response
            var imageBytes = CaptchaHelper.ImageToByteArray(captchaImage);

            return File(imageBytes, "image/png");
        }

        
        public ActionResult CustomerLogin()
        {
            return View();
        }
       
        public ActionResult AdminLogin()
        {


            return View();
        }




        [HttpPost]
        public ActionResult AdminLogin(LoginView loginView)
        {
            var isAdmin = Authentication.VerifyAdminCredentials(loginView.UserName, loginView.Password);

            if (isAdmin)
            {
                Session["AdminUserId"] = loginView.UserName; // Store user identifier in session
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(loginView);
            }
        }


        [HttpPost]
        public ActionResult CustomerLogin(LoginView loginView)
        {
            var isCustomer = Authentication.VerifyCustomerCredentials(loginView.UserName, loginView.Password);

            if (isCustomer)
            {
                var user = customerRepository.GetCustomerByUserName(loginView.UserName);
                Session["CustomerUserId"] = user.Id;
                Session["CustomerUserName"] = user.UserName;
                FormsAuthentication.SetAuthCookie(loginView.UserName, false);
                return RedirectToAction("Dashboard", "Customer");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(loginView);
            }
        }

        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = customerRepository.GetCustomerByUserName(model.UserName);

                if (user == null)
                {
                    ModelState.AddModelError(nameof(model.UserName), "Invalid username. Please enter a valid username.");
                    return View(model);
                }
                else
                {
                    // Assuming that model.Password is already in plain text
                    user.Password = model.Password;
                    customerRepository.customerSAveChanges();
                }

                TempData["SuccessMessage"] = "Password reset successfully. Please log in with your new password.";
                return RedirectToAction("CustomerLogin", "Validation");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = adminRepository.GetAdminByUserName(model.UserName);

                if (user == null)
                {
                    ModelState.AddModelError(nameof(model.UserName), "Invalid username. Please enter a valid username.");
                    return View(model);
                }
                else
                {
                    // Assuming that model.Password is already in plain text
                    user.Password = model.Password;
                    adminRepository.SaveAdminchages();
                }

                TempData["SuccessMessage"] = "Password reset successfully. Please log in with your new password.";
                return RedirectToAction("AdminLogin", "Validation");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            // Add any logout logic here, such as clearing session or authentication data
            if (Session["AdminUserId"] != null)
            {
                Session.Remove("AdminUserId"); // Clear admin session variable
            }
            else if (Session["CustomerUserId"] != null)
            {
                Session.Remove("CustomerUserId"); // Clear customer session variable
                Session.Remove("CustomerUserName"); // Clear other customer session variables if needed
            }

            Session.Abandon(); // Abandon the session

            // Set cache control headers to prevent caching
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetExpires(System.DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();

            // Redirect to the home page
            return RedirectToAction("Index", "Home"); // Adjust "Index" and "Home" based on your actual home page route
        }


        private string GenerateAlphanumericCaptcha()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private bool ValidateCaptcha(string userInput)
        {
            var captchaInSession = Session["Captcha"] as string;
            return !string.IsNullOrEmpty(captchaInSession) && userInput.Trim().Equals(captchaInSession, StringComparison.OrdinalIgnoreCase);
        }
    }
}
