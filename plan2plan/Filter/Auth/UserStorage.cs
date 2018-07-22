using plan2plan.Domain.Core;
using plan2plan.Infrastructure.Data;
using plan2plan.Infrastructure.Data.Components;
using System;
using plan2plan.Infrastructure.Business.Extentions;
using System.Linq;
using System.Web;
using plan2plan.Domain.Core.UserProfile;
using System.Web.Security;
using Serilog;

namespace plan2plan.Filter.Auth
{

    public static class UserStorage
    {
        private const string userProfile = "UserProfile";
        private const string userCookieName = "_utess";
        /// <summary>
        /// Сохраняет объект пользователся в сессию 
        /// </summary>
        /// <param name="userProfileSessionData">Модель plan2plan.Domain.Core.UserProfile.UserProfileSessionData</param>
        public static void Set(UserProfileSessionData userProfileSessionData)
        {
            HttpContext.Current.Session.Add(userProfile, userProfileSessionData);

            CookieStorage.Set(userCookieName, userProfileSessionData.ID.ToString(), 365);

            FormsAuthentication.SetAuthCookie(userProfileSessionData.Email.Mail, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user">Модель plan2plan.Domain.Core.User </param>
        public static UserProfileSessionData Set(User user)
        {
            var userProfile = new UserProfileSessionData(user);

            UserStorage.Set(userProfile);
            return userProfile;
        }

        public static UserProfileSessionData Get()
        {
            //From Session
            if (HttpContext.Current.Session[userProfile] != null)
            {
                return HttpContext.Current.Session[userProfile] as UserProfileSessionData;
            }

            var userID = CookieStorage.Get(userCookieName);
            Guid id;
            //From Cookies
            if (string.IsNullOrEmpty(userID) == false
                && Guid.TryParse(userID, out id) == true)
            {
                UserRepository userRepository = new UserRepository(new plat2platContext());
                var user = userRepository.GetUserByID(id);

                if (user != null)
                {
                    UserProfileSessionData userProfile = new UserProfileSessionData(user);
                    UserStorage.Set(userProfile);
                    Log.Information("Get user from cookie");
                    return userProfile;
                }
            }


            //From DB
            UserSessionRepository us = new UserSessionRepository(new plat2platContext());
            UserSession userSession = us.GetUserSessionBySessionID(HttpContext.Current.Session.SessionID, DateTime.Now.AddDays(-5));

            if (userSession != null)
            {
                Log.Information("Get user from DB");
                return UserStorage.Set(userSession.User);
            }
            //if (HttpContext.Current.User.Identity != null 
            //    && string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name) != null)
            //{

            //}
            //if (FormsAuthentication.GetAuthCookie()
            //{

            //}

            return null;
        }

        public static void LogOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Remove(userProfile);
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

#if false


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

        public static int CurrentUserID
        {
            get
            {
                if (user != null)
                    return -1;

                var userID = GetCookie(aliasUserID);

                return int.TryParse(userID, out int id) == true ? id : -1;
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
#endif
    }
}