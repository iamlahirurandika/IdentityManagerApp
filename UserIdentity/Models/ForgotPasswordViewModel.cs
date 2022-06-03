using System.ComponentModel.DataAnnotations;

namespace UserIdentity.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
