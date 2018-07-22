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

        public UserSession GetUserSessionBySessionID(string sessionID, DateTime date)
        {
            return context.UserSessions.Include(x => x.User)
                 .FirstOrDefault(x => x.SessionID == sessionID && x.Start > date && x.Finish == null);
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public void Update(string ip, Guid userID)
        {
            UserSession userSession = context.UserSessions.OrderByDescending(x => x.ID)
                .FirstOrDefault(x => x.IP == ip && x.UserID == userID);

            if (userSession != null)
            {
                userSession.Finish = DateTime.Now;
            }
        }
    }
}
