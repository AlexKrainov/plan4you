using plan2plan.Domain.Core;
using plan2plan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Infrastructure.Data.Components
{
    public class UserRepository : IUserRepository
    {
        private plat2platContext context;

        public UserRepository(plat2platContext context)
        {
            this.context = context;
        }

        public User GetUser(int userID, string login)
        {
            return context.Users.FirstOrDefault(x => x.ID == userID && x.Login == login);
        }
    }
}
