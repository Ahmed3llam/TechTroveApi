﻿using TechTrove.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechTrove.Models
{
    public class Wish
    {
        public int Id { get; set; }
        [ForeignKey("user")]
        public string UserID { get; set; }
        [ForeignKey("product")]
        public int ProductID { get; set; }
        public virtual User? User { get; set; }
        public virtual Product? Product { get; set; }
    }
}
