using System.ComponentModel.DataAnnotations;

namespace DacProject.Domain.Entities
{
    public class UserLogin
    {
        [Required]
        public string Email { get; set; } = String.Empty;

        [StringLength(20)]
        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
