namespace GrowTree.Web.Models
{
    public class SpilloverNodeViewModel
    {
        public int UserId { get; set; }
        public string? Position { get; set; }
        public int? Level { get; set; }

        //public List<SpilloverNodeViewModel> Children { get; set; } = new();
        public SpilloverNodeViewModel? Left { get; set; }
        public SpilloverNodeViewModel? Right { get; set; }
    }
}
