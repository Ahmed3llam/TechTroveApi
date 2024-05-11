using System.ComponentModel.DataAnnotations;

namespace TechTrove.DTO
{
    public class PasswordDTO
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "password does not match")]
        public string ConfirmPassword { get; set; }

    }
}
