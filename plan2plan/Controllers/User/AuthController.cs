using plan2plan.Domain.Core.ModelView;
using plan2plan.Domain.Interfaces;
using plan2plan.Filter.Auth;
using plan2plan.Infrastructure.Business.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace plan2plan.Controllers.User
{
    public class AuthController : Controller
    {
        private IUserRepository userRepository;
        private IUserSessionRepository userSessionRepository;

        public AuthController(IUserRepository userRepository, IUserSessionRepository userSessionRepository)
        {
            this.userRepository = userRepository;
            this.userSessionRepository = userSessionRepository;
        }
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Авторизация через отдельную страницу
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Enter(UserViewModel user, string ReturnUrl)
        {
            string message = "Логин и пароль обязателен для заполнения";
            if (string.IsNullOrEmpty(user.login) == false
               && string.IsNullOrEmpty(user.pwd) == false)
            {
                if (userRepository.isUserExist(user.login, user.pwd) == true)
                {
                    ViewBag.Authenticate = authenticate(user);

                    return Redirect(ReturnUrl ?? Url.Action("Index", "Home"));
                }
                else
                {
                    message = "Вы ввели неверную пару логин и пароль.";
                }
            }
            ViewBag.ErrorMessage = message;
            return View("Index");
        }

        /// <summary>
        /// Авторизация через диалоговое окно
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogIn([Bind(Include = "login,pwd,remember")]UserViewModel user)
        {
            if (string.IsNullOrEmpty(user.login) == false
                && string.IsNullOrEmpty(user.pwd) == false)
            {
                if (userRepository.isUserExist(user.login, user.pwd) == true)
                {
                    ViewBag.Authenticate = authenticate(user);
                    return PartialView("_LoginPartial");
                }
            }
            return PartialView("_LoginPartial");
        }

        /// <summary>
        /// Авторизация пользователя, и сохранение данных о нем в куки
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private Authenticate authenticate(UserViewModel user)
        {
            int userID = userRepository.GetUserID(user.login, user.pwd);
            //Авторизация пользователя через куки
            FormsAuthentication.SetAuthCookie(user.login, user.remember);
            UserStorage.SaveCookieID(UserStorage.aliasUserID, userID, DateTime.Now.AddMonths(1));

            userSessionRepository.CreateUserSession(new Domain.Core.UserSession
            {
                SessionID = Session.SessionID,
                UserID = userID,
                Start = DateTime.Now,
                IP = Request.UserHostAddress
            });
            userSessionRepository.Save();

            return new Authenticate { IsAuthenticated = true, UserName = user.login };
        }

        [HttpGet]
        public ActionResult LogOut(string ReturnUrl)
        {
            userSessionRepository.Update(Request.UserHostAddress, UserStorage.CurrentUserID);
            userSessionRepository.Save();

            FormsAuthentication.SignOut();
            UserStorage.RemoveCookie(UserStorage.aliasUserID);
            UserStorage.currentUser = null;

            return Redirect(ReturnUrl ?? Url.Action("Index", "Home"));
            // return PartialView("_LoginPartial", new Authenticate { IsAuthenticated = false });
        }
    }

    public class Authenticate
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
    }
}