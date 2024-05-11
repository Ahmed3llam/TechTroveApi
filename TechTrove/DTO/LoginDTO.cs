using System.ComponentModel.DataAnnotations;

namespace TechTrove.DTO
{
    public class LoginDTO
    {
        public string email { get; set; }
        public string password { get; set; }
        public bool rememberMe { get; set; }
    }
}
