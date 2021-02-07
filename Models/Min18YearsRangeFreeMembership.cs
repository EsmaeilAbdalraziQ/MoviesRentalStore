using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class Min18YearsRangeFreeMembership : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //First we need to select Membership Type
            //that is give us access to containing class and because it is Object we Need To Cast it to Customer Class
            var customer = (Customer)validationContext.ObjectInstance;
            //Check Membership Type
            //(customer.MemberShipTypeID == 0 "Unknown") that is Happen when Customer Does not Select any thing from DropDownList 
            //(customer.MemberShipTypeID == 1 "FREE") For MembershipType Id That is we have Defined in database 
            //if (customer.MemberShipTypeID == 0 || customer.MemberShipTypeID == 1)

            if (customer.MemberShipTypeID == MemberShipType.Unknown || 
                customer.MemberShipTypeID == MemberShipType.Free) 
            {
                return ValidationResult.Success;
            }
            if (customer.BirthDate == null)
            {
                return new ValidationResult("BirthDate is Required. ");
            }

            //We Need To Calculate the Customer Age
            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;

            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer Should Be at Least 18 Years Old To Apply Membership !");
        }
    }
}