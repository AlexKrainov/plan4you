using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace plan2plan.Filter.Auth
{
    /// <summary>
    /// Фильтры авторизации срабатывают после фильтров аутентификации и до запуска остальных фильтров
    /// и вызова методов действий. Цель фильтров авторизации - разграничить доступ пользователей, 
    /// чтобы к определенным ресурсам приложения имели доступ только определенные пользователи.
    /// </summary>
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private string[] allowedUsersEmail = new string[] { };
        private string[] allowedRoles = new string[] { };

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!string.IsNullOrEmpty(base.Users))
            {
                allowedUsersEmail = base.Users.Split(new char[] { ',' });
                for (int i = 0; i < allowedUsersEmail.Length; i++)
                    allowedUsersEmail[i] = allowedUsersEmail[i].Trim();
            }

            if (!string.IsNullOrEmpty(base.Roles))
            {
                allowedRoles = base.Roles.Split(new char[] { ',' });
                for (int i = 0; i < allowedRoles.Length; i++)
                    allowedRoles[i] = allowedRoles[i].Trim();
            }

            return UserStorage.Get() != null && User(httpContext) == true && Role(httpContext) == true;
            //return httpContext.Request.IsAuthenticated == true && User(httpContext) == true && Role(httpContext) == true;
        }

        private bool User(HttpContextBase httpContext)
        {
            if (allowedUsersEmail.Length > 0)
            {
                string name = httpContext.User.Identity.Name ?? UserStorage.Get().Email.Mail;
                return allowedUsersEmail.Contains(name);
            }
            return true;
        }

        private bool Role(HttpContextBase httpContext)
        {
            var userProfile = UserStorage.Get();

            if (allowedRoles.Length > 0)
            {
                if (string.IsNullOrEmpty(userProfile.Email.Mail) == true)
                    return false;

                //var userType = UserStorage.UserTypes.FirstOrDefault(x => x.ID == userProfile.UserTypeID).type;

                for (int i = 0; i < allowedRoles.Length; i++)
                {
                    if (userProfile.UserType.type == allowedRoles[i])
                        return true;
                }
                return false;
            }
            return true;
        }
    }
}