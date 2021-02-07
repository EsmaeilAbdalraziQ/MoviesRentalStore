using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.ViewModels
{
    public class RandomMovieViewModels
    {
        public Movie Movie{ get; set; }
        public List<Customer> Customers{ get; set; }
    }
}