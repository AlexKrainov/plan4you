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
    public class UserRepository : IUserRepository
    {
        private plat2platContext context;

        public UserRepository(plat2platContext context)
        {
            this.context = context;
        }

        public User GetUserByID(Guid userID)
        {
            return context.Users
                .Include(x => x.UserType)
                .Include(x => x.Email)
                .FirstOrDefault(x => x.ID == userID);
        }
        public User GetUser(string email, string password)
        {
            return context.Users
               .Include("UserType").Include(x => x.Email)
               .FirstOrDefault(x => x.Email.Mail == email && x.Password == password);
        }

        /// <summary>
        /// Возвращает ID юзера, если его нет возвращает -1
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Guid GetUserID(string email, string password)
        {
            User user = context.Users.Include( x => x.Email).FirstOrDefault(x => x.Email.Mail == email && x.Password == password);

            if (user != null)
            {
                return user.ID;
            }
            return Guid.Empty;
        }
        
        public void Create(User user)
        {
            context.Users.Add(user);
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return context.SaveChangesAsync();
        }   

        public bool isUserExistByEmailAndPassword(string email, string password)
        {
            return context.Users.Include(x => x.Email).Any(x => x.Email.Mail == email && x.Password == password);
        }

        public bool isUserExistByEmail(string email)
        {
            return context.Users.Include(x => x.Email).Any(x => x.Email.Mail == email);
        }

        public User GetUserByEmail(string mail)
        {
            return context.Users.Include(x => x.Email).FirstOrDefault(x => x.Email.Mail == mail);
        }

        public async Task<User> GetUserByEmailAsync(string mail)
        {
            return await context.Users.Include(x => x.Email).FirstOrDefaultAsync(x => x.Email.Mail == mail);
        }

        public bool UpdatePassword(Guid id, string newPassword)
        {
            var user = context.Users.FirstOrDefault(x => x.ID == id);
            if (user != null)
            {
                user.Password = newPassword;
                return true;
            }
            return false;
        }

        public void Update(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

    }
}
