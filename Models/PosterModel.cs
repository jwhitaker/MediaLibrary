using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaLibrary.Models
{
    public class PosterModel
    {
        public int Id { get; set; }
        public String MediaType { get; set; }
        public String MediaItemId { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}