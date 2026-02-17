namespace GrowTree.Web.Models
{
    public class Package
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public decimal Price { get; set; }
        public string PlanCategory { get; set; }
        public string RankName { get; set; }
        public int MaxLevels { get; set; }
    }
}
