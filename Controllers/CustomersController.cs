using Cinema.Models;
using Cinema.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.Extensions.Caching.Memory;
//using System.Runtime.Caching;

namespace Cinema.Controllers
{
    public class CustomersController : Controller
    {
        //DbContext To Access The Database
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //This DbContext is Disposable Object So We Need To Dispose This Object
        //And Override The Dispose() Method of the base Controller Class
        //To close database connections and free up the resources they hold as soon as possible
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            #region Caching

            //How To Caching Data => Imagine If we Want to Caching list of Genres
            //if (MemoryCache.Default["Genres"] == null)
            //{
            //    MemoryCache.Default["Genres"] = _context.Genres.ToList();
            //}

            //var genres = MemoryCache.Default["Genres"] as IEnumerable<Genre>;
            #endregion

            //var customeres = GetCustomers();
            //var customs = _context.Customers.Include(I => I.MemberShipType).ToList();

            //return View(customs);

            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            //Get The DropDown List from MemberShipType from Database
            var memberShip = _context.MemberShipTypes.ToList();

            //We need to path this object to the view but we already have customer object in model
            //so we have to create a viewModel to encapsulate all the data  required by this view
            var viewModel = new CustomerViewModel
            {
                Customer = new Customer(),
                MemberShipTypes = memberShip
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(s => s.Id == id);
            if (customer == null)
            {
                HttpNotFound();
            }

            var viewModel = new CustomerViewModel()
            {
                Customer = customer,
                MemberShipTypes = _context.MemberShipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerViewModel()
                {
                    Customer = customer,
                    MemberShipTypes = _context.MemberShipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0) //New Customer
            {
                _context.Customers.Add(customer);
            }
            else //Update Cutomer
            {
                var customerInDb = _context.Customers.Single(s => s.Id == customer.Id);
                //TryUpdateModel(customerInDb, "", new string[] { "Name", "Email" }); // This Aproach has Many Issue

                //OR We Can Use AutoMapper Library To Make this Updates
                //AutoMapper Library Is A Convinsion Based Mapping Tool
                //Mapper.Map(customer, customerInDb)

                //OR Manually Updating
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate= customer.BirthDate;
                customerInDb.MemberShipTypeID = customer.MemberShipTypeID;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var customer = _context.Customers.Include(i => i.MemberShipType).SingleOrDefault(i => i.Id == id);
            
            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }


        //private IEnumerable<Customer> GetCustomers()
        //{
        //    var customers = new List<Customer>
        //    {
        //        new Customer{ Id=1, Name="Customer-1"},
        //        new Customer{ Id=2, Name="Customer-2"},
        //        new Customer{ Id=3, Name="Customer-3"}
        //    };

        //    return customers;
        //}
    }
}