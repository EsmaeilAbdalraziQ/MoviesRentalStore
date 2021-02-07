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
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //-- {Index} =>  To Get List of Movies

        //public IHttpActionResult GetMovies(string query = null)
        public IHttpActionResult GetMovies(string query = null)
        {
            var movieQuery = _context.Movies
                .Include(i => i.Genre);
                //.Where(w => w.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                movieQuery = movieQuery.Where(m => m.Name.Contains(query));

            //return movieQuery
            //    .ToList()
            //    .Select(Mapper.Map<Movie, MovieDto>);

            var movieDtos = movieQuery
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDtos);
        }

        //-- {Details-Edit} => To Get One Movie

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(s => s.Id == id);

            if (movie is null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //-- ---------- {Create} Action To Post Movie ---------------------------------------
        [Authorize(Roles =RoleName.CanManageMovies)]
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);

            _context.Movies.Add(movie);
            _context.SaveChanges();


            //We Want To Add CustomerDto Id and Return it to the Client
            movieDto.Id = movie.Id;

            //We Need To Return Uri Of the Newly Created Resource To The Client => 
            //--------------------((api / movies / Id),              ObjectCreated
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        //-------------- {Update} Action TO Edit Movie ------------------------------------
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPut] // PutMovie
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _context.Movies.SingleOrDefault(s => s.Id == id);

            if (movieInDb is null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);

            _context.SaveChanges();
            
        }

        //--{Delete} //Action To Delete Movie
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(s => s.Id == id);

            if (movieInDb is null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            //else
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }














    }
}
