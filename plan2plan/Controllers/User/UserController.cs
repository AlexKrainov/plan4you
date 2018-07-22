using plan2plan.Domain.Core;
using plan2plan.Domain.Core.UserProfile;
using plan2plan.Domain.Interfaces;
using plan2plan.Filter.Auth;
using plan2plan.Infrastructure.Business.Email;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace plan2plan.Controllers.User
{
    public class UserController : Controller
    {
        private IUserRepository userRepository;
        private ILetterRepository letterRepository;

        public UserController(IUserRepository userRepository, ILetterRepository letterRepository)
        {
            this.userRepository = userRepository;
            this.letterRepository = letterRepository;
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [MyAuthentication]
        public ActionResult PersonPage()
        {

            return View();
        }
        #region Tab person data
        [MyAuthentication]
        [HttpPost]
        public ActionResult PersonData()
        {
            var user = UserStorage.Get();

            if (user != null)
            {
                var userProfile = new UserProfileSessionData(userRepository.GetUser(user.Email.Mail, user.Password));
                
                return PartialView("_PersonDataPartial", userProfile);
            }
            return RedirectToAction("Index", "Auth");
        }

        [HttpGet]
        public async Task<ActionResult> UpdateName(string name)
        {
            var userProfile = UserStorage.Get();

            if (userProfile != null && string.IsNullOrEmpty(name) == false)
            {
                name = name.Trim();
                if (name.Length < 2 || name.Length > 16)
                {
                    return Json(new { is_ok = false, meta = nameof(name), value = name, message = "Не удалось сохранить новое значение. Количество символов должно быть больше 3 и меньше 16." }, JsonRequestBehavior.AllowGet);
                }

                var userFromRepository = userRepository.GetUserByID(userProfile.ID);
                userFromRepository.Name = name;
                try
                {
                    await userRepository.SaveAsync();
                    UserStorage.Set(new UserProfileSessionData(userFromRepository));
                    return Json(new { is_ok = true, meta = nameof(name), value = name, message = "Ок" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, string.Format("Ошибка при сохранении нового именя для пользователя (ip: {0})", Request.UserHostAddress));
                    return Json(new { is_ok = false, meta = nameof(name), value = name, message = "Не удалось сохранить новое значение. Повторите попытку позже." }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { is_ok = false, meta = nameof(name), value = name, message = "Авторизуйтесь заново.", to_log_in = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> UpdateSurName(string surname)
        {
            var userProfile = UserStorage.Get();

            if (userProfile != null)
            {
                surname = surname.Trim();
                if (string.IsNullOrEmpty(surname) == false)
                {
                    surname = surname.Trim();
                }

                var userFromRepository = userRepository.GetUserByID(userProfile.ID);
                userFromRepository.SurName = surname;
                try
                {
                    await userRepository.SaveAsync();
                    UserStorage.Set(new UserProfileSessionData(userFromRepository));
                    return Json(new { is_ok = true, meta = nameof(surname), value = surname, message = "Ок" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, string.Format("Ошибка при сохранении нового именя для пользователя (ip: {0})", Request.UserHostAddress));
                    return Json(new { is_ok = false, meta = nameof(surname), value = surname, message = "Не удалось сохранить новое значение. Повторите попытку позже." }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { is_ok = false, meta = nameof(surname), value = surname, message = "Авторизуйтесь заново.", to_log_in = true }, JsonRequestBehavior.AllowGet);
        }

        [MyAuthentication]
        [HttpPost]
        public ActionResult UpdateEmail(string mail)
        {


            return View();
        }

        [MyAuthentication]
        public ActionResult ConfirmEmail(string returnUrl)
        {
            var userProfile = UserStorage.Get();
            ViewBag.Email = userProfile.Email.Mail;

            return View();
        }

        [HttpPost]
        [MyAuthentication]
        public async Task<ActionResult> ConfirmEmail_WillSendLetter()
        {
            var userProfile = UserStorage.Get();

            if (userProfile != null)
            {
                try
                {
                    EmailWorker emailWorker = new EmailWorker();
                    BuildingEmailBody body = new BuildingEmailBody();
                    string htmlBody = body.ForConfirmEmail(this.Server.MapPath("~/Common/Email/HtmlBody/ConfirmEmail.html"), "uid=" + userProfile.ID.ToString(), userProfile.Email.Mail);
                    await emailWorker.SendEmailAsync(userProfile.Email.Mail, "Подтверждение почты1", htmlBody);

                    ViewBag.Success = "Письмо отправлено. В течение минуты оно придет к вам на почту.";

                    letterRepository.CreateLetter(new Domain.Core.Letter
                    {
                        Date = DateTime.Now,
                        EmailID = userProfile.EmailID,
                        LetterTypeID = 2 //Confirm email
                    });
                    letterRepository.Save();

                    return PartialView("_ForgotPasswordPartial");
                }
                catch (Exception ex)
                {

                    ViewBag.ErrorMessage = "Ошибка при отправке письма.";
                    Log.Error(ex, "Ошибка при отправке почты.");
                    return PartialView("_ForgotPasswordPartial");
                }
            }

            ViewBag.NotExist = "Авторизуйтесь";
            return PartialView("_ForgotPasswordPartial");
        }

        [HttpPost]
        [MyAuthentication]
        public ActionResult ConfirmEmailSuccess()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConfirmEmailSuccess(string uid)
        {
            Guid id;
            if (string.IsNullOrEmpty(uid) == false && Guid.TryParse(uid, out  id) == true)
            {
                var user = userRepository.GetUserByID(id);

                //Проверяем отправляли письмо о подтверждении почты в течении дня
                Letter letter = letterRepository.GetLetter(user.Email.Mail);

                //Если дата отправки письма больше чем 1 день
                if (letter == null || letter.Date.AddDays(1) <= DateTime.Now)
                {
                    ViewBag.ErrorMessage = "Дата отправки письма больше чем 1 день, подтвердите почту в личных настройках повторно.";
                    ViewBag.isErrorLink = "true";

                    Log.Warning("method=ConfirmEmailSuccess на почту=" + letter.email.Mail + ", не было отправлено пиьсмо о подтверждении.");
                    return View();
                }

                try
                {
                    user.Email.isEmailConfirmed = true;
                    userRepository.Update(user);
                    userRepository.Save();
                    ViewBag.Success = "Почта подтверждена";
                    return View();
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Ошибка при подтверждении почты. Повторите попытку позднее.";
                    Log.Warning("method=ConfirmEmailSuccess данного ID=" + uid + ", нет в нашей базе данных");
                }
            }
            ViewBag.isErrorLink = "true";
            return View();
        }

        #endregion

        [HttpPost]
        public ActionResult MyOrder()
        {
            return PartialView("_MyOrderPartial");
        }

        #region Восстановление пароля

       
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(string mail)
        {
            if (string.IsNullOrEmpty(mail) == false)
            {
                var user = await userRepository.GetUserByEmailAsync(mail);

                if (user != null)
                {
                    try
                    {
                        EmailWorker emailWorker = new EmailWorker();
                        BuildingEmailBody body = new BuildingEmailBody();
                        string htmlBody = body.ForForgotPassword(this.Server.MapPath("~/Common/Email/HtmlBody/ForgotPassword.html"), "uid=" + user.ID.ToString());
                        await emailWorker.SendEmailAsync(user.Email.Mail, "Восстановление пароля", htmlBody);

                        ViewBag.Success = "Письмо отправлено. В течение минуты оно придет к вам на почту.";

                        letterRepository.CreateLetter(new Domain.Core.Letter
                        {
                            Date = DateTime.Now,
                            EmailID = user.EmailID,
                            LetterTypeID = 1 //Password recovery
                        });
                        letterRepository.Save();

                        return PartialView("_ForgotPasswordPartial");
                    }
                    catch (Exception ex)
                    {

                        ViewBag.ErrorMessage = "Ошибка при отправке письма.";
                        Log.Error(ex, "Ошибка при отправке почты.");
                        return PartialView("_ForgotPasswordPartial");
                    }
                }
            }
            ViewBag.NotExist = "Такой почты не существует";
            return PartialView("_ForgotPasswordPartial");
        }

        public ActionResult ResetPassword(string uid)
        {
            if (string.IsNullOrEmpty(uid) == false)
            {
                Guid id;
                if (Guid.TryParse(uid, out id) == true)
                {
                    var user = userRepository.GetUserByID(id);

                    if (user != null)
                    {
                        ViewBag.userID = uid;
                        return View();
                    }
                    else
                    {
                        ViewBag.NotExist = "Неверная ссылка перенаправления. Пожалуйста напишите нам об этом ";
                        Log.Warning("method=ResetPassword данного ID=" + uid + ", нет в нашей базе данных");
                    }
                }
            }
            ViewBag.isErrorLink = "true";
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string password, string uid)
        {
            if (string.IsNullOrEmpty(password) == false
                && string.IsNullOrEmpty(uid) == false)
            {
                Guid id;
                if (Guid.TryParse(uid, out id) == true)
                {
                    var user = userRepository.GetUserByID(id);

                    if (user != null)
                    {
                        if (userRepository.UpdatePassword(user.ID, password) == true)
                        {
                            userRepository.Save();
                            ViewBag.Success = "Пароль успешно изменен.";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    ViewBag.Error = "Не удалось найти данного пользователя.";
                    Log.Warning("Не удалось найти данного пользователя(ID = " + uid + ")");
                    return View();
                }
            }
            Log.Warning("Не удалось найти данного пользователя(ID = " + uid + ")");
            ViewBag.Error = "Не верно введены данные.";
            return View();
        }
        #endregion
    }
}