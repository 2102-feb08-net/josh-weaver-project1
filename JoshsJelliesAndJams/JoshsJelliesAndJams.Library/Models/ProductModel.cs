using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JoshsJelliesAndJams.Library
{
    public class ProductModel
    {
        [Required]
        public int ProductId { get; set; }
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }
        public decimal CostPerItem { get; set; }

        [Required]
        public decimal TotalLine { get; set; }
    }
}
