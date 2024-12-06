using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AdventureWorksWebApp.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authHttpCookie = HttpContext.Request.Cookies[cookieName];

            if (authHttpCookie != null)
            {
                FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(authHttpCookie.Value);
                string username = authenticationTicket.Name;

                ViewBag.Username = username ?? null;
            }
        }
    }
}