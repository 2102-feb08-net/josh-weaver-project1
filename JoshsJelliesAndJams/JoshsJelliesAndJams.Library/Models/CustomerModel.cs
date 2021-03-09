using System;
using System.ComponentModel.DataAnnotations;
using JoshsJelliesAndJams.Library.svc;

namespace JoshsJelliesAndJams.Library
{
    public class CustomerModel
    {

        public int CustomerID { get; set; }
        
        [Required]
        public string FirstName { get; set; } 
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public string StreetAddress1 { get; set; }

        public string StreetAddress2 { get; set; }
        
        [Required]
        public string City { get; set; }
        
        [Required]
        [MaxLength(2)]
        [MinLength(2)]
        public string State{ get; set; }
        
        [Required]
        [MaxLength(5)]
        [MinLength(5)]
        public string Zipcode { get; set; }
        
        [Required]
        public int DefaultStore { get; set; }

    }
}
