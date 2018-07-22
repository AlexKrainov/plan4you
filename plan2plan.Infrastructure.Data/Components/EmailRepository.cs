using plan2plan.Domain.Core;
using plan2plan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace plan2plan.Infrastructure.Data.Components
{
    public class EmailRepository : IEmailRepository
    {
        private plat2platContext context;

        public EmailRepository(plat2platContext context)
        {
            this.context = context;
        }
        public Email GetEmail(string mail)
        {
            return context.Emails.FirstOrDefault(x => x.Mail == mail);
        }

        public Email GetEmailByIP(string ip)
        {
            return context.Emails
                //.Include(x => x.Letters)
                .FirstOrDefault(x => x.IP == ip);
        }

        /// <summary>
        /// Возвращает либо найденный(существующий)  объект почты, либо создает объект почты
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public Email GetOrCreateEmail(string mail, string ip)
        {
            Email email = this.GetEmail(mail);

            if (email != null)
            {
                return email;
            }
            return new Email
            {
                Mail = mail,
                subscribedToNewsletters = true,
                isEmailConfirmed = false,
                IP = ip
            };
        }
    }
}
