using System.ComponentModel.DataAnnotations.Schema;

namespace TechTrove.DTO
{
    public class OrderDTO
    {
        public int id { set; get; }
        public DateTime date { set; get; }
        public decimal price { set; get; }
        public string name { set; get; }
        public string email { set; get; }
        public string address { set; get; }
        public int itemsCount { set; get; }=0;
    }
}
