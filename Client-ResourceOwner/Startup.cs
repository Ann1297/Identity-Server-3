using Client_ResourceOwner;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(Startup))]
namespace Client_ResourceOwner
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = new PathString("/index"),
                AuthenticationType = "Cookies"
            });
        }
    }
}