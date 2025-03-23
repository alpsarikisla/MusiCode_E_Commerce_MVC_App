using MusiCodeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Filters;
using System.Web.Mvc;

namespace MusiCodeWebApp.Filters
{
    public class MemberLoginRequiredFilterAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        MusiCodeDBModel db = new MusiCodeDBModel();
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["member"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //Kontrol sonrasındaki yönlendirme gibi davranışları bu alanda belirleyeceğiz
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/Member/Login");
            }
        }
    }
}