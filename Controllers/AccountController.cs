
using GrowTree.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace GrowTree.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
      
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ViewBag.Message = "Registration successful (test mode)";
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users
                .FirstOrDefault(u => u.Email == model.Email && u.IsActive);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }

            var hashedPassword = HashPassword(model.Password);

            if (user.PasswordHash != hashedPassword)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }

            // LOGIN SUCCESS (temporary)
            TempData["Success"] = "Login successful";
            return RedirectToAction("Dashboard");
        }
        private string HashPassword(string password)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = sha.ComputeHash(
                    System.Text.Encoding.UTF8.GetBytes(password)
                );
                return Convert.ToBase64String(bytes);
            }
        }


        public IActionResult Dashboard()
        {
            return View();
        }

       
    }
}

      

      
    

