using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Teqways_Com_CarRentManagementSystem.Filters
{
   
   public class IsAdminLoggedIn : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie _LoginCookie = HttpContext.Current.Request.Cookies["RememberMeUserInfo"];

            if (_LoginCookie == null)
            {
                filterContext.Result = new RedirectResult("/Login/Login");
            }
            
        }
    }
}