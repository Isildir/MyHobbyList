using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobbyList.FunctionalClasses
{
    public static class GlobalVariables
    {
        public static int DefaultImageId { get; set; } = 8;
        public static int RecommendCount { get; set; } = 8;
        public static int SimiliarListSize { get; set; } = 6;
        public static string EmptyDescription { get; set; } = "This product has new description yet...";
    }
}