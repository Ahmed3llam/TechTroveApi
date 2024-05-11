using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TechTrove.Models;

namespace TechTrove.DTO
{
    public class ProductDataDTO
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "First Name Should be more than 3 letters")]
        public string Title { get; set; }
        [MinLength(20, ErrorMessage = "First Name Should be more than 10 letters")]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
    }
}
