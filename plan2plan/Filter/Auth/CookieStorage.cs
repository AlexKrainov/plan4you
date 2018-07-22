using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace plan2plan.Filter.Auth
{
    public static class CookieStorage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="expiresDay">Сколько хранить дней, по умолчанию 1 день</param>
        public static void Set(string name, string value, int expiresDay = 1)
        {
            DateTime dateTime = DateTime.Now.AddDays(expiresDay);

            if (HttpContext.Current.Response.Cookies.Get(name) == null)
            {
                HttpCookie cookie = new HttpCookie(name, value);
                cookie.Expires = dateTime;
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            else
            {
                HttpCookie cookie = HttpContext.Current.Response.Cookies.Get(name);// ToDo: проверить передается ли куки по ссылке
                cookie.Expires = dateTime;
                cookie.Value = value;
                HttpContext.Current.Response.Cookies.Set(cookie);
            }

        }

        public static string Get(string name)
        {
            HttpCookie cookie = HttpContext.Current.Response.Cookies.Get(name);
            if (cookie != null)
            {
                return cookie.Value;
            }
            return null;
        }

        public static void Delete(string name)
        {
            HttpContext.Current.Response.Cookies.Remove(name);
        }

    }
}