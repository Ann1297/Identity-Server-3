using Client_ResourceOwner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Client_ResourceOwner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Identity()
        {
            return View(GetClaimsModel());
        }

        private ClaimsModel GetClaimsModel()
        {
            var identity = (ClaimsIdentity)HttpContext.User.Identity;

            return new ClaimsModel
            {
                Header = "Claims for Authorized User",
                Message = "The following claims have been retrieved from the Identity Server",
                Claims = identity.Claims.Select(x => new KeyValuePair<string, string>(x.Type, x.Value)),
                IsAuthenticated = HttpContext.User.Identity.IsAuthenticated,
            };
        }
    }
}