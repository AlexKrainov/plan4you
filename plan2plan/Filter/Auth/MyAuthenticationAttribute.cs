using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace plan2plan.Filter.Auth
{
    /// <summary>
    /// Фильтры аутентификации срабатывают до любого другого фильтра и выполнения метода,
    ///а также тогда, когда метод уже завершил выполнение, но его результат - объект ActionResult - не обработан.
    /// </summary>
    public class MyAuthenticationAttribute : FilterAttribute, IAuthenticationFilter
    {
        //Authentication = login + password (who you are)
        //Authorization = permissions(what you are allowed to do)


        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var user = UserStorage.Get();
            IIdentity ident = filterContext.Principal.Identity;
          
            if (ident.IsAuthenticated == false || user == null)
                filterContext.Result = new HttpUnauthorizedResult(); //доступ к данному ресурсу для пользовател запрещен
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var userSession = UserStorage.Get();
            var user = filterContext.HttpContext.User;
            
            if (user == null || user.Identity.IsAuthenticated == false || userSession == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary{
                    {"controller", "Auth"},
                    {"action", "Index"},
                    {"returnUrl", filterContext.HttpContext.Request.Url }
                });
            }

        }

        
    }
}