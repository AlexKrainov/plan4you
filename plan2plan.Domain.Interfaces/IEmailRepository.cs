using plan2plan.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Interfaces
{
    public interface IEmailRepository
    {
        Email GetEmail(string mail);
        Email GetOrCreateEmail(string mail, string ip);
        Email GetEmailByIP(string ip);
    }
}
