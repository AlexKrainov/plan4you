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
        [HttpPost]
        public ActionResult LogIn(UserViewModel user)
        {
            bool IsAuthenticated = false;
            if (string.IsNullOrEmpty(user.login) == false
                && string.IsNullOrEmpty(user.pwd) == false)
            {
                if (userRepository.isUserExist(user.login, user.pwd) == true)
                {
                    int userID = userRepository.GetUserID(user.login, user.pwd);
                    //Авторизация пользователя через куки
                    FormsAuthentication.SetAuthCookie(user.login, user.remember_me); 
                    UserStorage.SaveCookieID(UserStorage.aliasUserID, userID, DateTime.Now.AddMonths(1));

                    userSessionRepository.CreateUserSession(new Domain.Core.UserSession
                    {
                        SessionID = Session.SessionID,
                        UserID = userID,
                        Start = DateTime.Now
                    });
                    userSessionRepository.Save();

                    IsAuthenticated = true;
                    return PartialView("_LoginPartial", new Authenticate { IsAuthenticated = IsAuthenticated, UserName = user.login });// son(true, JsonRequestBehavior.AllowGet);
                }
            }
            return PartialView("_LoginPartial");
        }

        [HttpGet]
        public ActionResult LogOut(string returnUrl)
        {
            //qgoaat1a5tnvipwlxljambvs
            var z = HttpContext.Session.SessionID;
            userSessionRepository.Update(Session.SessionID, UserStorage.currentUser.ID);
            userSessionRepository.Save();

            FormsAuthentication.SignOut();
            UserStorage.RemoveCookie(UserStorage.aliasUserID);
            UserStorage.currentUser = null;

            return Redirect(returnUrl ?? Url.Action("Index", "Home"));
            // return PartialView("_LoginPartial", new Authenticate { IsAuthenticated = false });
        }
    }

    public class Authenticate
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
    }
}