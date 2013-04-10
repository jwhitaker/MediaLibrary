using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace TheWhitakers.MediaLibrary.Extensions
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(String rootPath)
            : base(rootPath)
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
}