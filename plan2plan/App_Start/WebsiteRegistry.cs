using plan2plan.Domain.Core.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace plan2plan.App_Start
{
    ///// <summary>
    ///// http://josephwoodward.co.uk/2014/06/sessions-asp-net-mvc-using-dependency-injection/
    ///// </summary>
    //public class WebsiteRegistry : Registry
    //{
    //    public WebsiteRegistry()
    //    {
    //        this.For<IUserProfileSessionData>().HybridHttpOrThreadLocalScoped().Use(() => GetUserProfileFromSession());
    //    }

    //    public static IUserProfileSessionData GetUserProfileFromSession()
    //    {
    //        var session = HttpContext.Current.Session;
    //        if (session["UserProfile"] != null)
    //        {
    //            return session["UserProfile"] as IUserProfileSessionData;
    //        }

    //        /* Create new empty session object */
    //        session["UserProfile"] = new UserProfileSessionData();

    //        return session["UserProfile"] as IUserProfileSessionData;
    //    }
    //}
}