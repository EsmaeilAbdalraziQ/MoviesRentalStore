using Cinema.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Customer Name.")]
        [StringLength(255)]
        public string Name { get; set; }

        //[Min18YearsRangeFreeMembership]//Custom Validation Class
        public DateTime? BirthDate { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }
        public int MemberShipTypeID { get; set; }
        public MemberShipTypeDto MemberShipType { get; set; }
    }
}