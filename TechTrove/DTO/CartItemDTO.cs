using System.ComponentModel.DataAnnotations.Schema;

namespace TechTrove.DTO
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Quantity { get; set; }
        public decimal TPrice { get; set; }

    }
}
