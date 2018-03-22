using plan2plan.Domain.Core;
using plan2plan.Infrastructure.Data;
using plan2plan.Infrastructure.Data.Components;
using System;
using plan2plan.Infrastructure.Business.Extentions;
using System.Linq;
using System.Web;

namespace plan2plan.Filter.Auth
{

    public static class UserStorage
    {
        /// <summary>
        /// Указывает на то под каким именнованным cookie лежит id user
        /// </summary>
        public const string aliasUserID = "buddy";
        private const string aliasUser = "bUbby";


        /// <summary>
        /// Хранилище User объекта
        /// </summary>
        private static User user
        {
            get
            {
                if (HttpContext.Current.Session[aliasUser] == null && HttpContext.Current.Session[aliasUser] is User)
                    return HttpContext.Current.Session[aliasUser] as User;
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session[aliasUser] = value;
            }
        }
        /// <summary>
        /// Получаем User
        /// </summary>
        public static User currentUser
        {
            get
            {
                if (user != null)
                    return user;

                var userID = GetCookie(aliasUserID);

                if (userID != null
                    && !string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)
                    && int.TryParse(userID, out int user_id) == true)
                {
                    UserRepository userRepository = new UserRepository(new plat2platContext());
                    return user = userRepository.GetUser(user_id, HttpContext.Current.User.Identity.Name);
                }
                return null;
            }
            set { user = value; }
        }

        /// <summary>
        /// Получаем типы User
        /// </summary>
        public static IQueryable<UserType> UserTypes
        {
            get
            {
                if (HttpContext.Current.Application.Get("UserTypes") == null)
                {
                    UserTypeRepository userTypeRepository = new UserTypeRepository(new plat2platContext());
                    HttpContext.Current.Application.Set("UserTypes", userTypeRepository.GetUserTypes());
                }

                return HttpContext.Current.Application.Get("UserTypes") as IQueryable<UserType>;
            }
        }

        /// <summary>
        /// Сохраняет в Response.Cookies данные 
        /// </summary>
        /// <param name="nameCookie"> Название Cookie</param>
        /// <param name="value">Значение которое нужно сохранить</param>
        /// <param name="dateTime">На сколько нужно сохранить информацию</param>
        internal static void SaveCookieID(string nameCookie, int id, DateTime dateTime)
        {
            var cookie = new HttpCookie(nameCookie, id.EncryptionIDtoString()); 
            cookie.Expires = dateTime;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameCookie"></param>
        /// <returns></returns>
        internal static string GetCookie(string nameCookie)
        {
            var cookie = HttpContext.Current.Request.Cookies[nameCookie];

            return cookie == null ? null : cookie.Value.DecryptionID().ToString();
        }

        /// <summary>
        /// Удаляем Cookie
        /// </summary>
        /// <param name="nameCookie">Название Cookie</param>
        internal static void RemoveCookie(string nameCookie)
        {
            var cookie = HttpContext.Current.Request.Cookies[nameCookie];
            cookie.Expires = DateTime.Now.AddMilliseconds(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}