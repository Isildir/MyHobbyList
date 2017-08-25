using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace MyBookList.Models
{
    public static class ExtensionUserBooks
    {
        public static string GetUserBooks(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserBooks");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }

    public static class ExtensionUserMovies
    {
        public static string GetUserMovies(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserMovies");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }

    public static class ExtensionUserGames
    {
        public static string GetUserGames(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserGames");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }

    public static class ExtensionUserSeries
    {
        public static string GetUserSeries(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserSeries");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}