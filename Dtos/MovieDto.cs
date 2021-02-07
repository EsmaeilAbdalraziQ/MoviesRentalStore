using Cinema.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public GenreDto Genre { get; set; } //Navigation Property

        [Required]
        public int GenreId { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Range(1, 25)]
        [Display(Name = "Number In Stock")]
        public int NumberInStock { get; set; }

        public string MoviePic { get; set; } //Path to Picture
    }
}