using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TheWhitakers.MediaLibrary.Extensions
{
    public class MediaLibraryConfiguration : ConfigurationSection
    {
        private static MediaLibraryConfiguration settings = ConfigurationManager.GetSection("MediaLibrary") as MediaLibraryConfiguration;

        public static MediaLibraryConfiguration Settings {
            get {
                return settings;
            }
        }

        [ConfigurationProperty("posterUploadPath", IsRequired=true)]
        public string PosterUploadPath
        {
            get { return (string)this["posterUploadPath"]; }
            set { this["posterUploadPath"] = value; }
        }
    }
}