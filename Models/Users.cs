using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace GrowTree.Web.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [MaxLength(15)]
        public string? Mobile { get; set; }

        [Required]
        [MaxLength(20)]
        public string ReferralCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string? TransactionPin { get; set; } = string.Empty;

        public int? SponsorId { get; set; }

        [ForeignKey("SponsorId")]
        public User? Sponsor { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; }

        public int? ParentUserID { get; set; }

        public int? Level { get; set; }

        public string? Position { get; set; }
    }
}
