using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DacProject.WebApp.Models
{
    public class UserLoginViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        //[EmailAddress]
        public string Email { get; set; } = null;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is not valid")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be less than 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string Password { get; set; } = null;
    }
}
