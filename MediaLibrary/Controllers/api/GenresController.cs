using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TheWhitakers.MediaLibrary.Models;

namespace TheWhitakers.MediaLibrary.Controllers.Api
{
    public class GenresController : ApiController
    {
        private MediaLibraryContext db = new MediaLibraryContext();

        public IEnumerable<Genre> GetGenres()
        {
            return db.Genres.OrderBy(x => x.Name).AsEnumerable();
        }
    }
}