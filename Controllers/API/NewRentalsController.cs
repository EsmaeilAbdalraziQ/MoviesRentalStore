using AutoMapper;
using Cinema.Dtos;
using Cinema.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
namespace Cinema.Controllers.API
{
    public class NewRentalsController : ApiController
    {
        //----Get Our Db Context
        private ApplicationDbContext _context;
        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }
        
        //GetRentals
        public IHttpActionResult GetRentals()
        {

            var rentals = _context.Rentals.Include(I => I.Customer).Include(N => N.Movie)
                .Select(Mapper.Map<Rental, NewRentalDto>);
            return Ok(rentals);
        }
        
        // POST: NewRentals
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            ////Case If No MovieIds
            //if (newRental.MovieIds.Count == 0)
            //    return BadRequest("No Movie Ids Have Been Given.!");
            
            //--Load Customers and Movies Based On Ids that is Dtos Gives Us
            var customer = _context.Customers.Single(s => s.Id == newRental.CustomerId);
            
            ////Case If Customer Id is Invalid => IF SingleOrDefault();
            //if (customer == null)
            //    return BadRequest("Customer Id is NOT Valid.!");
            

            var movies = _context.Movies.Where(w => newRental.MovieIds.Contains(w.Id)).ToList();
            //Select * From Movies
            //Where Id IN (1, 2, 3)

            ////Case On or More of the Movies Ids NoT Loaded
            //if (movies.Count != newRental.MovieIds.Count)
            //    return BadRequest("On or More of the Movies are Invalid.!");
            
            //--Then Foreach Movie we need to create new Rental Object for this Movie and Given Customer 
            foreach (var movie in movies)
            {
                //Case one or Movie is NOT Available
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is NOT Available.!");

                movie.NumberAvailable--;

                var rental = new Rental
                { 
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                //and Added To DB
                _context.Rentals.Add(rental);
            }
            //and SaveChanges();
            _context.SaveChanges();

            return Ok();
            //we Used Ok(); Because we are Returning Multiple Resources(Customer, Movie, Rental)
            //we are Did Not Using Created(); Because we are NOT Creating a Single new Object 
        }




    }
}