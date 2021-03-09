using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JoshsJelliesAndJams.Library
{
    public class OrderModel
    {
        public int OrderNumber { get; set; }
        [Required]
        public List<ProductModel> Product { get; set; }
        public DateTime OrderPlaced { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public int NumberOfProducts { get; set; }
        [Required]
        public int CustomerNumber { get; set; }
        [Required]
        public int StoreID { get; set; }
    }
}
