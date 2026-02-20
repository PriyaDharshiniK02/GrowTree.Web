using System.ComponentModel.DataAnnotations;

namespace GrowTree.Web.Models
{
    public class RegisterViewModel
    {
        public string? SponsorReferralCode { get; set; }

        public string? IntroducerCode { get; set; }
        public string? Username { get; set; }
        public string? Country { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Mobile { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        public int TransactionPin { get; set; }
        public int ConfirmTransactionPin { get; set; }
        public string? ReferralCode { get; set; }

        public string UserCode { get; set; }
        //[Required]
        //public bool AgreeTerms { get; set; }

    }
}
