using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaLibrary.Models
{
    public class Poster
    {
        public int Id { get; set; }
        public String FileName { get; set; }
        public String FullPath { get; set; }
        public long Size { get; set; }
    }
}