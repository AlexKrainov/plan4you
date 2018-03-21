using plan2plan.Domain.Core;
using plan2plan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Infrastructure.Data.Components
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private plat2platContext context;

        public UserTypeRepository(plat2platContext context)
        {
            this.context = context;
        }
        public IEnumerable<UserType> GetUserTypes()
        {
            return context.UserTypes;
        }
    }
}
