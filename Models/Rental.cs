using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class Rental
    {
        public int Id{ get; set; }

        [Required]
        public Customer Customer { get; set; } // Navigation Property
        
        [Required]
        public Movie Movie { get; set; } // Navigation Property
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }


    }
}