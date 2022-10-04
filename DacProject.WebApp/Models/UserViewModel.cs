using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DacProject.WebApp.Models
{
    public class UserViewModel : UserLoginViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; } = String.Empty;

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; } = String.Empty;

        [Required(AllowEmptyStrings = false)]
        public string Role { get; set; } = String.Empty;
    }
}
