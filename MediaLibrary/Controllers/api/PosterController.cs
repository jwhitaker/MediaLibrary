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
using System.Threading.Tasks;
using System.IO;

namespace MediaLibrary.Controllers.api
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(String rootPath) : base(rootPath)
        {
            
        }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            base.GetLocalFileName(headers);

            var i = headers.ContentDisposition.FileName.Split('.').Last();

            var fileName = headers.ContentDisposition.FileName.Replace("\"", "");
            var extension = Path.GetExtension(fileName);

            return Guid.NewGuid().ToString() + extension;
        }
    }

    public class PosterController : ApiController
    {
        private MediaLibraryContext db = new MediaLibraryContext();

        public List<Poster> GetPosters()
        {
            return db.Posters.ToList();
        }

        public Poster GetPoster(int id)
        {
            var poster = db.Posters.Find(id);

            if (poster == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return poster;
        }

        public Task<HttpResponseMessage> PostPoster()
        {
            ThrowIf(HttpStatusCode.UnsupportedMediaType, () => !this.Request.Content.IsMimeMultipartContent("form-data"));

            var rootPath = HttpContext.Current.Server.MapPath("~/Uploads/Posters"); 

            //var rootPath = string.Format("{0}\\{1}", Environment.GetEnvironmentVariable("TEMP"), "medialibrary");

            try
            {
                var dir = Directory.CreateDirectory(rootPath);

                var prd = new CustomMultipartFormDataStreamProvider(dir.FullName);

                return this.Request.Content.ReadAsMultipartAsync<CustomMultipartFormDataStreamProvider>(prd)
                    .ContinueWith(predecessor =>
                    {
                        if (predecessor.IsCompleted)
                        {
                            var provider = predecessor.Result;

                            var posters = provider.FileData
                                .Select(path => new FileInfo(path.LocalFileName))
                                .Where(fi => fi.Exists)
                                .Select(fi => new Poster { FileName = fi.Name, FullPath = fi.FullName, Size = fi.Length });

                            var returnItems = new List<Poster>();

                            foreach (var poster in posters)
                            {
                                db.Posters.Add(poster);
                                returnItems.Add(poster);
                            }

                            db.SaveChanges();
                            
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, returnItems);
                            return response;
                        }
                        else
                        {

                            return Request.CreateResponse(HttpStatusCode.InternalServerError, predecessor.Exception);
                        }
                    });
            }
            catch (Exception e)
            {
                throw new HttpResponseException(this.Request.CreateResponse(HttpStatusCode.InternalServerError, e));
            }
        }

        public HttpResponseMessage DeletePosters()
        {
            var removedItems = new List<Poster>();

            foreach (var i in db.Posters)
            {
                db.Posters.Remove(i);
                File.Delete(i.FullPath);
                removedItems.Add(i);
            }

            db.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, removedItems);
        }

        public HttpResponseMessage DeletePoster(int id)
        {
            Poster poster = db.Posters.Find(id);

            if (poster == null || !File.Exists(poster.FullPath))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            try
            {
                File.Delete(poster.FullPath);
                db.Posters.Remove(poster);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, poster);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private static void ThrowIf(HttpStatusCode code, Func<bool> condition, string reason = null)
        {
            if (condition())
            {
                var message = new HttpResponseMessage(code);
                if (reason != null)
                {
                    message.Content = new StringContent(reason);
                }

                throw new HttpResponseException(message);
            }
        }
    }
}
