using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MediaLibrary.Models
{
    public class Movie
    {
        public Movie()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        public int Id { get; set; }
        [Required]
        public String Title { get; set; }
        public String YearReleased { get; set; }

        public String Directors { get; set; }
        public String Actors { get; set; }
        public String Plot { get; set; }
        public String Notes { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
    }
}