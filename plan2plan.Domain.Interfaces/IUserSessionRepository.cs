using plan2plan.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Interfaces
{
    public interface IUserSessionRepository
    {
        void CreateUserSession(UserSession userSession);
        int Save();
        void Update(string ip, Guid userID);

        /// <summary>
        /// Возвращает объект UserSession, по sessionID
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="date">С какого преиуда начинать смотреть</param>
        /// <returns></returns>
        UserSession GetUserSessionBySessionID(string sessionID, DateTime date);

    }
}
