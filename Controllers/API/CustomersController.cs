using Cinema.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Cinema.Dtos;

namespace Cinema.Controllers.API
{
    public class CustomersController : ApiController
    {
        //We are going to use our context to get Customers From Database
        private ApplicationDbContext _context;
        public CustomersController()
        {
            //Initialize It in Constractor
            _context = new ApplicationDbContext();
        }
        //----------------------------------------------------------------------------------------

        //{Index} => Return a List of Customers 
        //Will Response To Request like This --- Get => /api/customers 
        public IHttpActionResult GetCustomers(string query = null)
        {
            //return _context.Customers.ToList();
            var customersQuery = _context.Customers
                .Include(c => c.MemberShipType);

            //Filtering
            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(w => w.Name.Contains(query));
            
            //After Filtering
            var customerDtos = customersQuery
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }
        //----------------------------------------------------------------------------------------

        //{Details} Another Action To return a Single Customer
        //Will Response To Request like This --- Get => /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            //Get The Customer From DB
            var customer = _context.Customers.SingleOrDefault(s => s.Id == id);

            if (customer is null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound); Before IHttpActionResult
                return NotFound();
            }

            //return customer
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }
        //----------------------------------------------------------------------------------------

        //{Create} Action To Create a Customer
        //Will Response To Request like This --- Post => /api/customers -- Post a Customer To Customers Collection

        [HttpPost] //PostCustomer
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            //If ModelState Not Valid
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest); Before IHttpActionResult
                return BadRequest();
            }

            //Otherwise We Add THis Object to our Context

            //We Need To Map This Deatil --- Back to our domain Object
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            //We Want To Add CustomerDto Id and Return it to the Client
            customerDto.Id = customer.Id;

            //return customerDto; Before IHttpActionResult

            //We Need To Return Uri Of the Newly Created Resource To The Client => 
            //--------------------((api / customers / Id),              ObjectCreated
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }
        //----------------------------------------------------------------------------------------

        //{Edit} Action To Update a Customer
        //Will Response To Request like This --- Put => /api/customers/1

        [HttpPut] // PutCustomer
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();
            }

            var customerInDb = _context.Customers.SingleOrDefault(s => s.Id == id);

            if (customerInDb is null)
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            //Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
            Mapper.Map(customerDto, customerInDb);

            //customerInDb.Name = customerDto.Name;
            //customerInDb.BirthDate = customerDto.BirthDate;
            //customerInDb.IsSubscribedToNewsLetter = customerDto.IsSubscribedToNewsLetter;
            //customerInDb.MemberShipTypeID = customerDto.MemberShipTypeID;

            _context.SaveChanges();

            return Ok();
        }
        //----------------------------------------------------------------------------------------

        //{Delete} Action To Remove a Customer
        //Will Response To Request like This --- Delete => /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(s => s.Id == id);
            if (customerInDb is null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            //Otherwise Delete Customer
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }

    }
}
