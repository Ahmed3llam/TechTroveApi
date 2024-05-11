using System.ComponentModel.DataAnnotations;

namespace TechTrove.DTO
{
    public class UserDataDTO
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastname { get; set; }
        public string profileImage { get; set; }
        public string phoneNumber { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string Address { get; set; }
    }
}
