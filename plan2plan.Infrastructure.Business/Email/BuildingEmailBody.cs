using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Infrastructure.Business.Email
{
    public class BuildingEmailBody
    {
        const string url = "http://localhost:55253";// "http://wwww.plan2plan.ru";

        /// <summary>
        /// Возвращает тело емейл почты
        /// </summary>
        /// <param name="link">Ссылка на восстановление пароля</param>
        /// <returns></returns>
        public string ForForgotPassword(string path, string link)
        {
            string htmlText = File.ReadAllText(path);

            htmlText = htmlText.Replace("{{ password_reset_link }}", (url + "/User/ResetPassword?" + link));

            return htmlText;
        }

        public string ForConfirmEmail(string path, string link, string email)
        {
            string htmlText = File.ReadAllText(path);

            htmlText = htmlText.Replace("{{ confirm_email_link }}", (url + "/User/ConfirmEmailSuccess?" + link));
            htmlText = htmlText.Replace("{{ email }}", email);

            return htmlText;
        }
    }
}
