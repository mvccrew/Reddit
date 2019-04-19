using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reddit.FileFormats
{
    public class Formats
    {
        public static List<string> VideoFormats
        {
            get
            {
                return new List<string> { "mp4", "webm", "mkv", "flv", "vob", "ogv",
                    "ogg", "drc", "gif", "gifv", "mng", "avi", "mov", "qt", "wmv", "yuv", "rm",
                "rmvb","asf","amv","m4p","m4v","mpg","mp2","mpeg","mpe","mpv","mpg","m2v","m4v",
                "svi","3gp","3g2","mxf","roq","nsv","f4v","f4p","f4a","f4b"};

            }
        }
        public static List<string> ImageFormats
        {
            get
            {
                return new List<string> {"tif","tiff","jpeg","jpg","jif","jfif","jp2","jpx","j2k","j2c","fpx","pcd","png" };
            }
        }
    }
}