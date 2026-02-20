
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

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match");
                return View(model);
            }

            if (model.TransactionPin != model.ConfirmTransactionPin)
            {
                ModelState.AddModelError("", "Transaction pin does not match");
                return View(model);
            }

            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("", "Email already exists");
                return View(model);
            }

            User? sponsor = null;
            if (!string.IsNullOrEmpty(model.SponsorReferralCode))
            {
                sponsor = _context.Users
                    .FirstOrDefault(u => u.ReferralCode == model.SponsorReferralCode);
            }

            string referralCode = Guid.NewGuid().ToString("N")
                .Substring(0, 8).ToUpper();

             
          string passwordHash = HashPassword(model.Password);
          string transactionPinHash = HashPassword(model.TransactionPin.ToString());

            using (SqlConnection con = new SqlConnection(
              _configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("sp_RegisterUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value = model.FullName;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = model.Email;
                    cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 20).Value = model.Mobile;
                    cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 512).Value = passwordHash;
                    cmd.Parameters.Add("@ReferralCode", SqlDbType.NVarChar, 20).Value = referralCode;

                    cmd.Parameters.Add("@SponsorId", SqlDbType.Int).Value =
                        sponsor != null ? sponsor.UserId : (object)DBNull.Value;

                    cmd.Parameters.Add("@TransactionPin", SqlDbType.NVarChar, 512).Value = transactionPinHash;
                    cmd.Parameters.Add("@UserCode", SqlDbType.NVarChar, 20).Value = model.UserCode;

                    con.Open();
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

      

      
    

