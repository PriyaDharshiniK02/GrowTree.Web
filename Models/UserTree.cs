using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrowTree.Web.Models
{
    [Table("UserTree")]
    public class UserTree
    {
        [Key]
        [Column("UserTreeId")]   
        public int UserTreeId { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        
        public int? ParentId { get; set; }

        [Column("Position")]
        public string Position { get; set; }

        public bool IsActive { get; set; }

        [Column("Level")]
        public int Level { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }
    }
}
