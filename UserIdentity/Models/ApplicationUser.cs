

using System.ComponentModel.DataAnnotations;

namespace UserIdentity.Models
{
    public class ApplicationUser : IdentityUser 
    {
        [Required]
        public string Name { get; set; }
        

    }
}
