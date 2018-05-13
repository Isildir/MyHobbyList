using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyHobbyList.Models;

namespace MyHobbyList.Controllers
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
            var image = _context.Files.Single(x => x.Id == id);

            return File(image.Content, image.ImageMimeType.ToString());
        }

        public ActionResult ShowMini(int id)
        {
            var image = _context.Files.Single(x => x.Id == id);

            return File(image.AdditionalData, image.ImageMimeType.ToString());
        }
    }
}