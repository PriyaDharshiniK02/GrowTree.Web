namespace GrowTree.Web.Models
{
    public class MyTeamViewModel
    {
        public Dictionary<int, int> LevelCounts { get; set; }

        public string CurrentUserName { get; set; }
        public string CurrentUserCode { get; set; }
    }
}