using Microsoft.AspNetCore.Mvc;

namespace GrowTree.Web.Models
{
    public class TreeNodeViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public string SponsorName { get; set; }
        public decimal TotalEarnings { get; set; }
        public int Level { get; set; }

        public List<TreeNodeViewModel> Children { get; set; } = new();
    }
}
