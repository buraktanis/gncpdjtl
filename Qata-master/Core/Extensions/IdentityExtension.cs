using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Core.Extensions
{
    public static class IdentityExtension
    {
        public static string Name(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).Claims.Where(c => c.Type == ClaimTypes.Name)
                              .Select(c => c.Value).SingleOrDefault();
            return (claim != null) ? claim : string.Empty;
        }
        public static string Surname(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).Claims.Where(c => c.Type == ClaimTypes.Surname)
                              .Select(c => c.Value).SingleOrDefault();
            return (claim != null) ? claim : string.Empty;
        }

        public static string Token(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Token");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string PhotoUrl(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("PhotoUrl");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string INCHARGE(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("INCHARGE");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string ADDR2(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("ADDR2");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string CITY(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("CITY");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string EMAILADDR(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("EMAILADDR");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string CODE(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("CODE");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string TOWN(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("TOWN");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string DEFINITION_(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("DEFINITION_");
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string NameIdentifier(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                              .Select(c => c.Value).SingleOrDefault();
            return (claim != null) ? claim : string.Empty;
        }


        public static string MobilePhone(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).Claims.Where(c => c.Type == ClaimTypes.MobilePhone)
                              .Select(c => c.Value).SingleOrDefault();
            return (claim != null) ? claim : string.Empty;
        }

        public static List<string> Role(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).Claims.Where(c => c.Type == ClaimTypes.Role)
                              .Select(c => c.Value).ToList();


            return claim;
        }

        public static string Email(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).Claims.Where(c => c.Type == ClaimTypes.Email)
                              .Select(c => c.Value).SingleOrDefault();
            return (claim != null) ? claim : string.Empty;
        }
        public static string Tel(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("tel");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
