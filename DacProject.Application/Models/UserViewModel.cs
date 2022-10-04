namespace DacProject.Application.Models
{
    public class UserViewModel : UserLoginViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string Role { get; set; } = String.Empty;
    }
}
