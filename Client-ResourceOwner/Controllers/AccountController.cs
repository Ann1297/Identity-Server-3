using Client_ResourceOwner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityModel;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using IdentityModel.Client;
using Microsoft.Owin.Security.Jwt;
using System.Security.Cryptography.X509Certificates;

namespace Client_ResourceOwner.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            TokenResponse token = await GetToken(model.Username, model.Password);

            await SignInAsync(token);

            return RedirectToAction("Index", "Home");
        }

        private async Task<TokenResponse> GetToken(string user, string password)
        {
            var client = new TokenClient(
                "https://localhost:44302/identity/connect/token",
                "mvc_resourceowener",
                "supersecret");

            var result = await client.RequestResourceOwnerPasswordAsync(user, password, "openid");
            return result;
        }

        public async Task SignInAsync(TokenResponse token)
        {
            var claims = await ValidateIdentityTokenAsync(token);

            var id = new ClaimsIdentity(claims, "Cookies");
            id.AddClaim(new Claim("access_token", token.AccessToken));
            id.AddClaim(new Claim("expires", DateTime.Now.AddSeconds(token.ExpiresIn).ToLocalTime().ToString()));
            Request.GetOwinContext().Authentication.SignIn(id);
        }

        private async Task<IEnumerable<Claim>> ValidateIdentityTokenAsync(TokenResponse token)
        {
            return await Task.Run<IEnumerable<Claim>>(() =>
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                var cert = new X509Certificate2(
                string.Format(@"{0}Certificates\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory),
                "idsrv3test");

                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidAudience = "https://localhost:44302/identity/resources",               
                    ValidIssuer = "https://localhost:44302/identity",
                    NameClaimType = "name",

                    IssuerSigningTokens = new X509CertificateSecurityTokenProvider(
                             "https://localhost:44302/identity",
                             cert).SecurityTokens
                };

                SecurityToken t;
                ClaimsPrincipal id = tokenHandler.ValidateToken(token.AccessToken, validationParameters, out t);
                var claimList = id.Claims.ToList();
                return claimList.AsEnumerable();
            });
        }
        
        [HttpGet]
        public ActionResult LogOff()
        {
            Request.GetOwinContext()
                .Authentication
                .SignOut("Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}