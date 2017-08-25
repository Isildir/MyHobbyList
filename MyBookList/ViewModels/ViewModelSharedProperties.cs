using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBookList.ViewModels
{
    public class ViewModelSharedProperties
    {
        public bool CanEdit { get; set; } = false;

        public bool InUse { get; set; } = false;
    }
}