using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MediaLibrary.Models;

namespace MediaLibrary.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private MediaLibraryContext db = new MediaLibraryContext();

        public dynamic GetMovies(int page = 1, int itemsPerPage = 3)
        {
            var model = new
            {
                Page = page,
                TotalItems = db.Movies.Count(),
                Items = db.Movies.Include(g => g.Genres).Page(page, itemsPerPage)
            };

            return model;
        }
        
        public Movie GetMovie(int id)
        {
            Movie movie = db.Movies.Find(id);

            if (movie == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return movie;
        }

        public HttpResponseMessage PutMovie(int id, Movie movie)
        {
            if (ModelState.IsValid && id == movie.Id)
            {
                var originalMovie = db.Movies.Find(id);

                var originalMovieEntry = db.Entry(originalMovie);
                originalMovieEntry.CurrentValues.SetValues(movie);

                foreach (var g in originalMovie.Genres)
                {
                    if (!movie.Genres.Any(x => x.Id == g.Id))
                    {
                        g.Movies.Remove(originalMovie);
                    }
                }

                foreach (var g in movie.Genres)
                {
                    if (!originalMovie.Genres.Any(x => x.Id == g.Id))
                    {
                        db.Genres.Attach(g);
                        originalMovie.Genres.Add(g);
                    }
                }

                originalMovie.DateModified = DateTime.Now;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                
                return Request.CreateResponse(HttpStatusCode.OK, movie);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage PostMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);

                foreach (var g in movie.Genres)
                {
                    db.Genres.Attach(g);
                }

                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, movie);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = movie.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage DeleteMovie(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Movies.Remove(movie);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, movie);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}