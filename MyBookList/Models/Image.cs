using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookList.Models
{
    public class Image
    {
        public int Id { get; set; }

        public byte[] FullImage { get; set; }

        public byte[] ThumbnailImage { get; set; }

        public int ImageMimeType { get; set; }
    }
}