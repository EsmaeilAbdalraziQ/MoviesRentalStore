using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cinema.Models
{
    public class ApplicationUser : IdentityUser
    {
        //Add New Properties When User Register

        [Required]
        [StringLength(255)]
        public string DrivingLicense { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //  Sql("SET IDENTITY_INSERT MemberShipTypes ON");
        //  Sql("INSERT INTO MemberShipTypes (MemberShipTypeID, SignUpFee, DurationInMonth, DiscountRate) VALUES (4,150,12,20)");
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MemberShipType> MemberShipTypes { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public ApplicationDbContext(): base("DefaultConnection", throwIfV1Schema: false)
        {
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<Cinema.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}