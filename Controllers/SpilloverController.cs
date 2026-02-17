using GrowTree.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrowTree.Web.Controllers
{
    public class SpilloverController : Controller
    {
        public readonly SpilloverService _spilloverService;

        public SpilloverController(SpilloverService spilloverService)
        {
            _spilloverService = spilloverService;
        }

        public IActionResult SpilloverTree()
        {
            int rootUserId = 1;
            var tree = _spilloverService.GetTree(rootUserId);
            return View(tree);


        }
    }
}
