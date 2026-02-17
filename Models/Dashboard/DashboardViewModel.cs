namespace GrowTree.Web.Models
{
    public class DashboardViewModel
    {
        public string UserName { get; set; }
        public string Rank { get; set; }
        public decimal TotalEarnings { get; set; }

        public decimal WithdrawableWallet { get; set; }
        public decimal BusinessWallet { get; set; }
        public decimal UpgradeWallet { get; set; }

        public decimal DirectIncome { get; set; }
        public decimal TeamIncome { get; set; }
        public decimal BonusIncome { get; set; }
        public decimal PassiveIncome { get; set; }
    }
}
