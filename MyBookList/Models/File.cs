using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobbyList.Models
{
    public class File
    {
        public int Id { get; set; }

        public FileType FileType { get; set; }

        public byte[] Content { get; set; }

        public byte[] AdditionalData { get; set; }

        public int ImageMimeType { get; set; }
    }
}