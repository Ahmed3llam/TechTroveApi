using TechTrove.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TechTrove.DTO
{
    public class ProductDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string img { get; set; }
        [Column(TypeName = "money")]
        public decimal price { get; set; }
        public int stock { get; set; }
        public string category { get; set; }
        public int reviewCount { get; set; } = 0;
        public int orderCount { get; set; } = 0;

    }
}
