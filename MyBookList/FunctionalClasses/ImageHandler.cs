using System;
using System.IO;
using System.Web;
using MyHobbyList.Models;

namespace MyHobbyList.FunctionalClasses
{
    public class ImageHandler
    {
        public static int AddImage(HttpPostedFileBase UploadImage, ref ApplicationDbContext _context)
        {
            int imageId;

            if (UploadImage != null)
            {
                Models.File image = new Models.File
                {
                    Content = ReduceSize(UploadImage.InputStream, 210, 300, ref _context),
                    AdditionalData = ReduceSize(UploadImage.InputStream, 70, 100, ref _context),
                    ImageMimeType = UploadImage.ContentLength
                };

                _context.Files.Add(image);
                _context.SaveChanges();

                imageId = image.Id;
            }
            else
            {
                imageId = GlobalVariables.DefaultImageId;
            }

            return imageId;
        }

        private static byte[] ReduceSize(Stream stream, int maxWidth, int maxHeight, ref ApplicationDbContext _context)
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