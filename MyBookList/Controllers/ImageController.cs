using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookList.Models;

namespace MyBookList.Controllers
{
    [AllowAnonymous]
    public class ImageController : Controller
    {
        private ApplicationDbContext _context;

        public ImageController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult ShowFull(int id)
        {
            var image = _context.Images.Single(x => x.Id == id);

            return File(image.FullImage, image.ImageMimeType.ToString());
        }

        public ActionResult ShowMini(int id)
        {
            var image = _context.Images.Single(x => x.Id == id);

            return File(image.ThumbnailImage, image.ImageMimeType.ToString());
        }
    }
}