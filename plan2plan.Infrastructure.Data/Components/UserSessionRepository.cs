using plan2plan.Domain.Core;
using plan2plan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Infrastructure.Data.Components
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private plat2platContext context;

        public UserSessionRepository(plat2platContext context)
        {
            this.context = context;
        }
        public void CreateUserSession(UserSession userSession)
        {
            context.UserSessions.Add(userSession);
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public void Update(string sessionID, int userID)
        {
            UserSession userSession = context.UserSessions.FirstOrDefault(x => x.SessionID == sessionID && x.UserID == userID);

            if (userSession != null)
            {
                userSession.Finish = DateTime.Now;
            }
        }
    }
}
