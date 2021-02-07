using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class MemberShipType
    {
        [Key]
        public int MemberShipTypeID { get; set; }

        [Required]
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonth { get; set; }
        public byte DiscountRate { get; set; }

        public static readonly int Unknown = 0;
        public static readonly int Free = 0;

    }
}