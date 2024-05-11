using TechTrove.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TechTrove.DTO
{
    public class CategoryDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public int productsCount { get; set; } = 0;
    }
}
