using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyBookList.FunctionalClasses
{
    public class ImageHandler
    {
        private ApplicationDbContext _context;

        public ImageHandler()
        {
            _context = new ApplicationDbContext();
        }

        public int AddImage(HttpPostedFileBase UploadImage)
        {
            int imageId;

            if (UploadImage != null)
            {
                Models.Image image = new Models.Image();
                
                image.FullImage = ReduceSize(UploadImage.InputStream, 210, 300);
                image.ThumbnailImage = ReduceSize(UploadImage.InputStream, 70, 100);
                image.ImageMimeType = UploadImage.ContentLength;
                
                _context.Images.Add(image);
                _context.SaveChanges();

                imageId = image.Id;
            }
            else
            {
                imageId = GlobalVariables.DefaultImageId;
            }

            return imageId;
        }

        private static byte[] ReduceSize(Stream stream, int maxWidth, int maxHeight)
        {
            System.Drawing.Image source = System.Drawing.Image.FromStream(stream);
            double widthRatio = ((double)maxWidth) / source.Width;
            double heightRatio = ((double)maxHeight) / source.Height;
            double ratio = (widthRatio < heightRatio) ? widthRatio : heightRatio;
            System.Drawing.Image thumbnail = source.GetThumbnailImage((int)(source.Width * ratio), (int)(source.Height * ratio), null, IntPtr.Zero);
            using (var memory = new MemoryStream())
            {
                thumbnail.Save(memory, source.RawFormat);
                return memory.ToArray();
            }
        }
    }
}