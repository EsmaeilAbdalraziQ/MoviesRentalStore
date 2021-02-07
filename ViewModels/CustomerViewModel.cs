using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.ViewModels
{
    public class CustomerViewModel
    {
        public IEnumerable<MemberShipType> MemberShipTypes { get; set; }
        public Customer Customer { get; set; }

    }
}