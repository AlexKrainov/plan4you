﻿using plan2plan.Domain.Core;
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
        void Update(string ip, int userID);

    }
}
