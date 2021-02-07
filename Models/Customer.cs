using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage= "Please Enter Customer Name.")]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Date Of Birth")]
        [Min18YearsRangeFreeMembership]//Custom Validation Class
        public DateTime? BirthDate { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }

        //Navigation Property to Navigate from On Type To Another => From Customer To MemberShipType
        public MemberShipType MemberShipType { get; set; }

        [Display(Name ="MemberShip Type")]
        public int MemberShipTypeID { get; set; }

    }
}