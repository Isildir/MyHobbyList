using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBookList.Models;

namespace MyBookList.Controllers
{
    public class ModalController : Controller
    {
        #region Index view method.    
        #region Get: /Modal/Index method.    
        /// <summary>  
        /// Get: /Modal/Index method.    
        /// </summary>  
        /// <returns>Return index view</returns>  
        public ActionResult Index()
        {
            try
            {
            }
            catch (Exception ex)
            {
                // Info    
                Console.Write(ex);
            }
            // Info.    
            return this.View();
        }
        #endregion
        #region POST: /Modal/Index    
        /// <summary>  
        /// POST: /Modal/Index    
        /// </summary>  
        /// <param name="model">Model parameter</param>  
        /// <returns>Return - Modal content</returns>  
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Book model)
        {
            try
            {
                // Verification    
                if (ModelState.IsValid)
                {
                    // Info.    
                    return this.Json(new { EnableSuccess = true, SuccessTitle = "Success", SuccessMsg = model.Title });
                }
            }
            catch (Exception ex)
            {
                // Info    
                Console.Write(ex);
            }
            // Info    
            return this.Json(new { EnableError = true, ErrorTitle = "Error", ErrorMsg = "Something goes wrong, please try again later" });
        }
        #endregion
        #endregion
    }
}


    /// <summary>  
    /// Modal controller class.    
    /// </summary>  
    