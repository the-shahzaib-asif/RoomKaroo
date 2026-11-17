using Microsoft.AspNetCore.Mvc;

namespace RoomIQ.Controllers
{
    public class AccountController : Controller
    {
        // 4. login.html -> Identity/Auth Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password, string role)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                Response.Cookies.Append("UserEmail", email);
                Response.Cookies.Append("UserRole", role ?? "Finder");

                if (role == "Lister")
                    return RedirectToAction("ListerDashboard", "Home");

                return RedirectToAction("FinderHome", "Home");
            }

            ViewBag.Error = "Invalid login details.";
            return View();
        }

        // 5. signup.html -> Identity/Auth Signup
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(string fullName, string email, string password, string role)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                Response.Cookies.Append("UserEmail", fullName ?? email);
                Response.Cookies.Append("UserRole", role ?? "Finder");

                if (role == "Lister")
                    return RedirectToAction("ListerDashboard", "Home");

                return RedirectToAction("FinderHome", "Home");
            }

            return View();
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserEmail");
            Response.Cookies.Delete("UserRole");
            return RedirectToAction("Index", "Home");
        }
    }
}