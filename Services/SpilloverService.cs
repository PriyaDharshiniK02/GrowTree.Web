using GrowTree.Web.Models;

namespace GrowTree.Web.Services
{
    public class SpilloverService
    {
        private readonly ApplicationDbContext _context;

        public SpilloverService(ApplicationDbContext context)
        {
            _context = context;
        }


        // 🌳 BUILD TREE FOR UI
        public SpilloverNodeViewModel GetTree(int rootUserId)
        {
            var allNodes = _context.Users
                .Where(x => x.IsActive)
                .ToList();

            return BuildNode(rootUserId, allNodes);
        }

        private SpilloverNodeViewModel BuildNode(
            int userId,
            List<User> allNodes)
        {
            var node = allNodes.First(x => x.UserId == userId);

            var viewModel = new SpilloverNodeViewModel
            {
                UserId = node.UserId,
                Level = node.Level
            };

            var children = allNodes
                .Where(x => x.ParentUserID == userId)
                .ToList();

            var left = children.FirstOrDefault(x => x.Position == "L");
            var right = children.FirstOrDefault(x => x.Position == "R");

            if (left != null)
                viewModel.Left = BuildNode(left.UserId, allNodes);

            if (right != null)
                viewModel.Right = BuildNode(right.UserId, allNodes);

            return viewModel;
        }

       
        // 🔁 BFS SPILLOVER SEARCH
        private int FindSpilloverParent(int sponsorUserId)
        {
            var queue = new Queue<int>();
            queue.Enqueue(sponsorUserId);

            while (queue.Any())
            {
                var current = queue.Dequeue();

                var children = _context.Users
                    .Where(x => x.ParentUserID == current)
                    .ToList();

                if (children.Count < 2)
                    return current;

                foreach (var child in children)
                    queue.Enqueue(child.UserId);
            }

            throw new Exception("No available spillover position");
        }
    }

}
