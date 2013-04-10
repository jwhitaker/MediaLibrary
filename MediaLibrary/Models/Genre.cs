using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheWhitakers.MediaLibrary.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public String Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Movie> Movies { get; set; }
    }
}