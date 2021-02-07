using Cinema.Models;
using Cinema.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Cinema.Controllers
{
    public class MoviesController : Controller
    {
        //DbContext To Access The Database
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        //This DbContext is Disposable Object So We Need To Dispose This Object
        //And Override The Dispose() Method of the base Controller Class
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            //var movies = GetMovies();
            if (User.IsInRole("CanManageMovies"))
            {
                return View("Index", _context.Movies.Include(I => I.Genre).ToList());
                //return View("Index");
            }
            else
            {
                return View("ReadOnlyList", _context.Movies.Include(I => I.Genre).ToList());
                //return View("ReadOnlyList");
            }

        }

        [Authorize(Roles = RoleName.CanManageMovies )]
        [HttpGet]
        public ActionResult Create()
        {
            var genres = _context.Genres.ToList();
            //ViewBag.genre = new SelectList(genre, "GenreId", "GenreName");

            var viewModel = new MovieViewModel()
            {
                //Movie = new Movie(),
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(s => s.Id == id);
            
            if (movie == null)
            {
                return HttpNotFound();
            }
            
            var genrc = _context.Genres.ToList();
            //ViewBag.genrc = new SelectList(genrc, "Id", "GenreName", 1);

            var viewModel = new MovieViewModel(movie)//After Adding Constactor
            {
                Genres = _context.Genres.ToList()
                //Movie = movie
            };

            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                //return The Same View("CustomerForm")
                var viewModel = new MovieViewModel(movie)//After Adding Constactor
                {
                    //Movie = movie,
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }

            //----- Other Wise Add Or Update The User
            if (movie.Id == 0)//New Movie
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
                //_context.SaveChanges();
            }
            else//Update Movie
            {
                var movieInDb = _context.Movies.Single(s => s.Id == movie.Id); //Find MovieID To Edit it

                //_context.Entry(movie).State = System.Data.Entity.EntityState.Modified;
                movieInDb.Name = movie.Name;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.MoviePic = movie.MoviePic;
                //movieInDb.DateAdded = movie.DateAdded;

                //_context.SaveChanges();
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index), "Movies");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var movie = _context.Movies.Include(i => i.Genre).SingleOrDefault(s => s.Id == id);
            
            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }


        #region GetMovies() Method
        //private IEnumerable<Movie> GetMovies()
        //{
        //    var movies = new List<Movie>
        //    {
        //        new Movie{ Name = "Movie-1"},
        //        new Movie{ Name = "Movie-2"},
        //        new Movie{ Name = "Movie-2"}
        //    };
        //    return movies;
        //}
        #endregion


        // GET: Movies/Random
        //public ActionResult Random()
        //{
        //    var movie = new Movie() { Name = "Shrek" };
        //    var customers = new List<Customer>
        //    {
        //        new Customer{ Name = "Customer-1"},
        //        new Customer{ Name = "Customer-2"}
        //    };

        //    var viewModel = new RandomMovieViewModels
        //    {
        //        Movie = movie,
        //        Customers = customers
        //    };

        //    return View(viewModel);

        //---Passing Data to Views
        //ViewData["Movie"] = movie; //ViewData["Movie"]
        //ViewBag.Movie = movie; //ViewBag.Movie

        //If you want to pass data to view Just use This approach
        //Just Pass movie object in Instance of ViewResult => Method => View(movie);
        //---- var viewresult = new ViewResult();
        //---- viewresult.ViewData.Model.
        //---- viewresult.ViewBag.
        //return View(movie);
        //}



    }
}