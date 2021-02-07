using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        [Required]
        public DateTime DateAdded { get; set; }
        
        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Range(1, 25)]
        [Display(Name = "Number In Stock")]
        public int NumberInStock { get; set; }

        public int NumberAvailable { get; set; } //Available Movies

        public Genre Genre { get; set; } //Navigation Property
        
        [Display(Name ="Genre")]
        [Required]
        public int GenreId { get; set; }
        public string MoviePic { get; set; } //Path to Picture


    }
}