using plan2plan.Domain.Core.ModelView;
using plan2plan.Domain.Core.UserProfile;
using plan2plan.Domain.Interfaces;
using plan2plan.Filter.Auth;
using plan2plan.Infrastructure.Business.Extentions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private IEmailRepository emailRepository;

        public AuthController(IUserRepository userRepository,
            IUserSessionRepository userSessionRepository,
            IEmailRepository emailRepository)
        {
            this.userRepository = userRepository;
            this.userSessionRepository = userSessionRepository;
            this.emailRepository = emailRepository;
        }
        // GET: Auth
        public ActionResult Index(bool isEnter = true)
        {
            if (isEnter == true)
            {
                ViewBag.ActiveEnter = "show active";
                if (ViewBag.User == null)
                {
                    ViewBag.User = new UserCheckInViewModel();
                }
            }

            return View();
        }

        #region Вход

        /// <summary>
        /// Авторизация через отдельную страницу
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Enter(UserViewModel user, string returnUrl)
        {
            ViewBag.ActiveEnter = "show active";
            string message = "Пароль обязателен для заполнения";
            if (ModelState.IsValid == true)
            {
                if (userRepository.GetUser(user.email /* Почта */, user.pwd) != null)
                {
                    authenticate(user);
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }
                else
                {
                    message = "Вы ввели неверную пару почта и пароль.";
                }
            }

            ViewBag.User = new UserCheckInViewModel(user);
            ViewBag.ErrorMessage = message;

            return View("Index");
        }

        /// <summary>
        /// Авторизация через диалоговое окно
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogIn([Bind(Include = "email,pwd,remember")]UserViewModel user)
        {
            if (ModelState.IsValid == true)
            {
                if (userRepository.GetUser(user.email /* Почта */, user.pwd) != null)
                {
                    authenticate(user);
                    return PartialView("_LoginPartial");
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Json(new
                    {
                        is_ok = true,
                        is_have = false,
                        message = "Вы ввели неверную пару почта и пароль."
                    });
                }
            }
            return Json(new
            {
                is_ok = true,
                is_have = false,
                message = "Не все обязательные поля заполнены."
            });
        }

        #endregion

        #region Регистрация

        /// <summary>
        /// Регистрация через отдельную страницу
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckInPage(UserCheckInViewModel user, string returnUrl)
        {
            ViewBag.ActiveCheckIn = "show active";

            if (ModelState.IsValid == true)
            {
                if (userRepository.GetUser(user.email /* Почта */, user.pwd) != null) //userRepository.isUserExistByLoginAndPassword(user.login, user.pwd) == true)
                {
                    ViewBag.ErrorMessage = "Пользователь с такой почтой и паролем уже существует.";
                }
                else if (userRepository.isUserExistByEmail(user.email) == true)
                {
                    ViewBag.ErrorMessage = "Пользователь с такой почтой уже существует, если это вы ? Воспользуйтесь восстановлением пароля.";
                }
                else if (CreateNewUser(user) == false)
                {
                    ViewBag.ErrorMessage = "Не удалось создать пользователя, попробуйте позже или сообщите нам об ошибке.";
                }
                else
                {
                    authenticate(user);
                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                }

                ViewBag.User = user;
                return View("Index", new { isEnter = false });
            }

            ViewBag.User = user;
            ViewBag.ErrorMessage = "Не все поля формы были заполнены корректно.";

            return View("Index", new { isEnter = false });
        }

        /// <summary>
        /// Регистрация через диалоговое окно
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CheckIn(UserCheckInViewModel user)
        {
            if (ModelState.IsValid == true)
            {
                if (userRepository.GetUser(user.email, user.pwd) != null)
                {
                    return Json(new
                    {
                        is_ok = true,
                        is_have = true,
                        message = "Пользователь с такой почтой и паролем уже существует."
                    });
                }
                else if (userRepository.isUserExistByEmail(user.email) == true)
                {
                    return Json(new
                    {
                        is_ok = true,
                        is_have = true,
                        message = "Пользователь с такой почтой уже существует, если это вы ? Воспользуйтесь восстановлением пароля."
                    });
                }
                else
                {
                    if (CreateNewUser(user) == false)
                    {
                        return Json(new
                        {
                            is_ok = true,
                            is_have = false,
                            message = "Не удалось создать пользователя, попробуйте позже или сообщите нам об ошибке."
                        });
                    }

                    authenticate(user);
                    return PartialView("_LoginPartial");
                    //return RedirectToAction("Index", "Home");
                }
            }
            return Json(new { is_ok = true, is_have = false, message = "Не все обязательные поля заполнены." });
        }

        private bool CreateNewUser(UserCheckInViewModel user)
        {
            plan2plan.Domain.Core.User newUser = new plan2plan.Domain.Core.User
            {
                Name = string.IsNullOrEmpty(user.name) == true ? null : user.name.Trim(),
                Password = user.pwd.Trim(),
                UserTypeID = 2, //Standard
                dateTime = DateTime.Now,
                Email = emailRepository.GetOrCreateEmail(user.email.Trim(), Request.UserHostAddress)
            };

            try
            {
                userRepository.Create(newUser);
                userRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при создании пользователя");
                return false;
            }
            return true;
        }

        #endregion

        [HttpGet]
        public ActionResult LogOut(string ReturnUrl)
        {
            userSessionRepository.Update(Request.UserHostAddress, UserStorage.Get().ID);
            userSessionRepository.Save();

            UserStorage.LogOut();

            return Redirect(ReturnUrl ?? Url.Action("Index", "Home"));
            // return PartialView("_LoginPartial", new Authenticate { IsAuthenticated = false });
        }

        /// <summary>
        /// Авторизация пользователя, и сохранение данных о нем в куки
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private bool authenticate(UserViewModel user)
        {
            var userProfile = new UserProfileSessionData(userRepository.GetUser(user.email, user.pwd));

            UserStorage.Set(userProfile);

            userSessionRepository.CreateUserSession(new Domain.Core.UserSession
            {
                SessionID = Session.SessionID,
                UserID = userProfile.ID,
                Start = DateTime.Now,
                IP = Request.UserHostAddress
            });
            try
            {
                userSessionRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при создании UserSession");
                return false;
            }
            return true;
            // return new Authenticate { IsAuthenticated = true, UserName = userProfile.Name ?? userProfile.Login };
        }
    }

    public class Authenticate
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
    }
}