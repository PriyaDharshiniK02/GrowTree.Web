
using GrowTree.Web.Models;
using GrowTree.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;




namespace GrowTree.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly IConfiguration _configuration;
        private readonly SpilloverService _spilloverService;


        
        public AccountController(ApplicationDbContext context, IConfiguration configuration, SpilloverService spilloverService)
        {
            _context = context;
            _configuration = configuration;
            _spilloverService = spilloverService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // ✅ Password validation
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match");
                return View(model);
            }

            // ✅ Transaction pin validation
            if (model.TransactionPin != model.ConfirmTransactionPin)
            {
                ModelState.AddModelError("", "Transaction pin does not match");
                return View(model);
            }

            // ✅ Email check
            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("", "Email already exists");
                return View(model);
            }

            // ✅ UserCode uniqueness check
            if (_context.Users.Any(u => u.UserCode == model.UserCode))
            {
                ModelState.AddModelError("UserCode", "This user code is already taken");
                return View(model);
            }

            // ✅ Referral validation
            if (!string.IsNullOrEmpty(model.ReferralCode))
            {
                bool referralExists = _context.Users
                    .Any(u => u.UserCode == model.ReferralCode);

                if (!referralExists)
                {
                    ModelState.AddModelError("ReferralCode", "Invalid referral code");
                    return View(model);
                }
            }

            string passwordHash = HashPassword(model.Password);
            string transactionPinHash = HashPassword(model.TransactionPin.ToString());

            using (SqlConnection con = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("sp_RegisterUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FullName", model.FullName);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@TransactionPin", transactionPinHash);
                    cmd.Parameters.AddWithValue("@UserCode", model.UserCode);

                    cmd.Parameters.Add("@ReferralCode", SqlDbType.NVarChar, 20).Value =
                        string.IsNullOrEmpty(model.ReferralCode)
                        ? (object)DBNull.Value
                        : model.ReferralCode;

                    con.Open();
                    Console.WriteLine(model.UserCode.Length);
                    Console.WriteLine(passwordHash.Length);
                    Console.WriteLine(transactionPinHash.Length);
                    cmd.ExecuteNonQuery();
                }
            }

            ViewBag.Success = "Registration successful";
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users
                .FirstOrDefault(u => u.Email == model.Email && u.IsActive);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email (or) password");
                return View(model);
            }

            var hashedPassword = HashPassword(model.Password);

            if (user.PasswordHash != hashedPassword)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserName", user.FullName);

            return RedirectToAction("Index", "Dashboard");
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

      

      
    

