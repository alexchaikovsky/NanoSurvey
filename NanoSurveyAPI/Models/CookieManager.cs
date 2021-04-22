using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NanoSurveyAPI.Models
{
    public static class CookieManager
    {
        public const string cookieKey = "sessionId";
        public static string GetCookie(HttpContext ctx)
        {
            if (ctx.Request.Cookies.TryGetValue(cookieKey, out string cookieValue))
            {
                return cookieValue;
            }
            return null;
        }
        public static void AddNewCookie(HttpContext ctx, string cookieValue)
        {
            if (cookieValue != GetCookie(ctx))
            {
                ctx.Response.Cookies.Append(cookieKey, cookieValue);
            }
        }
        public static void ExpireCookie(HttpContext ctx)
        {
            ctx.Response.Cookies.Delete(cookieKey);
        }
    }
}
