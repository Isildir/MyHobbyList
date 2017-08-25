using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBookList.Models;

namespace MyBookList.ViewModels
{
    public class SeriesFormViewModel :  ViewModelSharedProperties
    {
        public Series Series { get; set; }
    }
}