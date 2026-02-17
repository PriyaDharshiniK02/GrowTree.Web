using GrowTree.Web.Models;
using GrowTree.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrowTree.Web.Controllers
{
    public class WalletController : Controller
    {

        private readonly ApplicationDbContext _context;

        public WalletController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFund()
        {
            return View();
        }

        public IActionResult FundTransfer()
        {
            return View();
        }

        public IActionResult InterWalletTransfer()
        {
            return View();
        }

        public IActionResult Packages()
        {
            var model = new PackageActivationViewModel
            {
                Packages = _context.Packages.ToList()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Activate(PackageActivationViewModel model)
        {
            var package = _context.Packages
                .FirstOrDefault(p => p.PackageId == model.SelectedPackageId);

            if (package == null)
            {
                ModelState.AddModelError("", "Invalid package");
                return RedirectToAction("Packages");
            }

            var userPackage = new UserPackage
            {
                UserId = model.ActivateUserId,
                PackageId = package.PackageId,
                Status = 1, // Active
                PurchaseDate = DateTime.Now
            };

            _context.UserPackages.Add(userPackage);
            _context.SaveChanges();

            return RedirectToAction("Packages");
        }
        [HttpPost]
        public IActionResult Upgrade(PackageActivationViewModel model)
        {
            var lastPackage = _context.UserPackages
                .Where(x => x.UserId == model.UpgradeUserId)
                .OrderByDescending(x => x.PurchaseDate)
                .FirstOrDefault();

            if (lastPackage == null)
            {
                ModelState.AddModelError("", "User not activated yet");
                return RedirectToAction("Packages");
            }

            var nextPackage = _context.Packages
                .Where(p => p.Price > _context.Packages
                    .Where(pk => pk.PackageId == lastPackage.PackageId)
                    .Select(pk => pk.Price)
                    .First())
                .OrderBy(p => p.Price)
                .FirstOrDefault();

            if (nextPackage == null)
            {
                ModelState.AddModelError("", "No upgrade available");
                return RedirectToAction("Packages");
            }

            var upgrade = new UserPackage
            {
                UserId = model.UpgradeUserId,
                PackageId = nextPackage.PackageId,
                Status = 1,
                PurchaseDate = DateTime.Now
            };

            _context.UserPackages.Add(upgrade);
            _context.SaveChanges();

            return RedirectToAction("Packages");
        }


        public IActionResult ComboPackages()
        {
            return View();
        }

        public IActionResult FamilyWalletTransfer
            ()
        {
            return View();
        }

        public IActionResult DeFiAddFund()

        {
            return View();
        }
        public IActionResult InterCropMiracle()
        {
            return View();
        }

        public IActionResult InterCrop()
        {
            return View();        }
    

     public IActionResult MyPackages()
        {
            return View();
        }
   

     public IActionResult Replant()
      {
    return View();
       }


        public IActionResult InterCropTransfer
            ()
        {
            return View();
        }
    }


}



