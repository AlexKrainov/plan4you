using plan2plan.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByID(Guid userID);
        User GetUserByEmail(string mail);
        Task<User> GetUserByEmailAsync(string mail);
        /// <summary>
        /// Ищет пользователя по ключу: почта-пароль
        /// </summary>
        /// <param name="loginOrEmail"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User GetUser(string email, string password);
        Guid GetUserID(string email, string password);
        bool isUserExistByEmailAndPassword(string email, string password);
        bool isUserExistByEmail(string email);
        void Create(User user);
        bool UpdatePassword(Guid id, string newPassword);
        void Update(User user);
        int Save();
        Task<int> SaveAsync(); 
    }
}
