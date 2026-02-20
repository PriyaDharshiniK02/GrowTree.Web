namespace GrowTree.Web.Models
{

    public class MyDirectViewModel
    {
        public int UserId { get; set; }
        public string MemberName { get; set; }
        public string Mobile { get; set; }
        public DateTime JoinDate { get; set; }
        public string Rank { get; set; }
        public decimal TotalEarnings { get; set; }
        public int TotalCount { get; set; }
        public int RefCount { get; set; }
    }
}