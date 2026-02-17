namespace GrowTree.Web.Models
{
    public class UserPackage
    {
        public int UserPackageId { get; set; }   // PK

        public int UserId { get; set; }

        public int PackageId { get; set; }

        public int Status { get; set; }          // 1 = Active

        public DateTime PurchaseDate { get; set; }
    }
}
