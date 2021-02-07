using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cinema.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        [StringLength(255)]
        public string GenreName { get; set; }

        //public ICollection<Movie> Movies { get; set; }//Navigation Property

    }
}