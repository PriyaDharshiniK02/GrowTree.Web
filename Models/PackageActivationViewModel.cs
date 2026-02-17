using GrowTree.Web.Models;

namespace GrowTree.Web.ViewModels
{
    public class PackageActivationViewModel
    {
        
        public int ActivateUserId { get; set; }   
        public int SelectedPackageId { get; set; }

        
        public int UpgradeUserId { get; set; }

        
        public List<Package> Packages { get; set; }
    }
}
