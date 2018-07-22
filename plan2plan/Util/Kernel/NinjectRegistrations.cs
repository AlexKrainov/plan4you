using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using plan2plan.Domain.Interfaces;
using plan2plan.Domain.Core;
using plan2plan.Infrastructure.Data.Components;
using System.Web.Http.Validation;

namespace plan2plan.Util.Kernel
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileRepository>().To<FileRepository>();
            Bind<IStatisticsRepository>().To<StatisticsRepository>();
            Bind<IFeedbackRepository>().To<FeedbackRepository>();
            Bind<IActionRepository>().To<ActionRepository>();
            Bind<IEmailRepository>().To<EmailRepository>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUserSessionRepository>().To<UserSessionRepository>();
            Bind<IUserTypeRepository>().To<UserTypeRepository>();
            Bind<ILetterRepository>().To<LetterRepository>();
            Bind<ILetterTypeRepository>().To<LetterTypeRepository>();
        }
    }
}