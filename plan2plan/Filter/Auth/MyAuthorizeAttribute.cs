﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace plan2plan.Filter.Auth
{
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        private string[] allowedUsers = new string[] { };
        private string[] allowedRoles = new string[] { };

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!string.IsNullOrEmpty(base.Users))
            {
                allowedUsers = base.Users.Split(new char[] { ',' });

                for (int i = 0; i < allowedUsers.Length; i++)
                    allowedUsers[i] = allowedUsers[i].Trim();
            }

            if (!string.IsNullOrEmpty(base.Roles))
            {
                allowedRoles = base.Roles.Split(new char[] { ',' });
                for (int i = 0; i < allowedRoles.Length; i++)
                    allowedRoles[i] = allowedRoles[i].Trim();
            }

            return httpContext.Request.IsAuthenticated && User(httpContext) && Role(httpContext);
        }

        private bool User(HttpContextBase httpContext)
        {
            if (allowedUsers.Length > 0)
                return allowedUsers.Contains(httpContext.User.Identity.Name);
            return true;
        }

        private bool Role(HttpContextBase httpContext)
        {
            if (allowedRoles.Length > 0)
            {
                if (UserStorage.currentUser == null)
                    return false;
                var userType = UserStorage.UserTypes.FirstOrDefault(x => x.ID == UserStorage.currentUser.UserTypeID).type;

                for (int i = 0; i < allowedRoles.Length; i++)
                {
                    if (userType == allowedRoles[i])
                        return true;
                }
                return false;
            }
            return true;
        }
    }
}