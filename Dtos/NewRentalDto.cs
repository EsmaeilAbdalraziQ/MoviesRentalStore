using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Dtos
{
    public class NewRentalDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string MovieName { get; set; }
        public List<int> MovieIds { get; set; }

    }
}