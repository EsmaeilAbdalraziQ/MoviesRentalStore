using Cinema.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.ViewModels
{
    public class MovieViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }

        //public Movie Movie { get; set; }

        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Range(1, 30)]
        [Display(Name = "Number In Stock")]
        public int? NumberInStock { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public int? GenreId { get; set; }

        public string MoviePic { get; set; } //Path to Picture

        #endregion

        public string Title
        {
            get
            {
                return Id != 0 
                    ? "Edit Movie" 
                    : "New Movie";
            }
        }

        public MovieViewModel()
        {
            //To Make Sure is our Form The Hidden  is Populate
            Id = 0;
        }

        public MovieViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }
    }
}