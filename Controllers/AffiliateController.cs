using GrowTree.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrowTree.Web.Controllers
{
    public class AffiliateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AffiliateController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MyDirect(int page = 1)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            int pageSize = 10;

            var query = _context.MyDirect
                .OrderByDescending(x => x.JoinDate);   // Latest first

            int totalRecords = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var data = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalRecords = totalRecords;

            return View(data);
        }
        public IActionResult MyTeam()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            // ✅ Get current user details
            var user = _context.Users
                .Where(u => u.UserId == userId)
                .Select(u => new
                {
                    u.FullName,
                    u.UserCode   // Tamilmani007
                })
                .FirstOrDefault();

            Dictionary<int, int> levelCounts = new();

            // ✅ Level 1 count from VIEW
            int level1Count = _context.MyDirect.Count();
            levelCounts[1] = level1Count;

            // Levels 2–12 = 0
            for (int i = 2; i <= 12; i++)
                levelCounts[i] = 0;

            var model = new MyTeamViewModel
            {
                LevelCounts = levelCounts,
                CurrentUserName = user?.FullName,
                CurrentUserCode = user?.UserCode
            };

            return View(model);
        }
        private List<int> GetLevelUsers(List<int> parentIds)
        {
            return _context.Users
                .Where(u => parentIds.Contains(u.SponsorId ?? 0))
                .Select(u => u.UserId)
                .ToList();
        }

        public IActionResult SpilloverTree()
        {
            return View();
        }

        public IActionResult AutoFillTree()
        {
            return View();
        }
        public IActionResult MiracleTree()
        {
            return View();
        }
        public IActionResult PassiveTree()
        {
            return View();
        }
        public IActionResult StarPassiveTree()
        {
            return View();
        }
        public IActionResult SuperPassiveTree()
        {
            return View();
        }
        public IActionResult FieldOfficers()
        {
            return View();
        }
        public IActionResult SeniorFieldOfficers()
        {
            return View();
        }

    }








}
