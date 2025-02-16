using System.ComponentModel.DataAnnotations;

namespace Gestion_Bunny.Modeles
{
    public class LoginModel
    {
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }
        
        [Required]
        public string? Password { get; set; }
    }
}
