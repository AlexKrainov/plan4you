using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Services.Interfaces.Email
{
    public interface IEmailWorker
    {
        /// <summary>
        /// Отправляем одно письмо на онид адрес
        /// </summary>
        /// <param name="email"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        void SendEmail(string email, string title, string message);
        /// <summary>
        /// Отправляем одно письмо на онид адрес асинхронно
        /// </summary>
        /// <param name="email"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendEmailAsync(string email, string title, string message);
    }
}
