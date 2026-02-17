using Microsoft.AspNetCore.Mvc;
using GrowTree.Web.Models;

namespace GrowTree.Web.Controllers
{
    public class DashboardController : Controller
    {

        public IActionResult Index()
        {
            // 🔐 PROTECT DASHBOARD (LOGIN REQUIRED)
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            var model = new DashboardViewModel
            {
                UserName = HttpContext.Session.GetString("UserName") ?? "User",
                Rank = "Not Achieved",
                TotalEarnings = 0,

                WithdrawableWallet = 0,
                BusinessWallet = 0,
                UpgradeWallet = 0,

                DirectIncome = 0,
                TeamIncome = 0,
                BonusIncome = 0,
                PassiveIncome = 0
            };

            return View(model);
        }
    }
}
