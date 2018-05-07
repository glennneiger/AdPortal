using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace AdPortal.MVC.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Name(this ClaimsPrincipal claims)
        {
            if(!claims.Claims.Any())
            {
                return string.Empty;
            }
            return claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;

        }

        public static string Role(this ClaimsPrincipal claims)
        {
            if(!claims.Claims.Any())
            {
                return string.Empty;
            }
            return claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;

        }
    }
}