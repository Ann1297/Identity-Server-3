using IdentityServer.Config;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.MicrosoftAccount;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace IdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/identity", idservApp =>
            {
                var idServerServiceFactory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryUsers(Users.Get())
                    .UseInMemoryScopes(Scopes.Get());

                var options = new IdentityServerOptions
                {
                    Factory = idServerServiceFactory,
                    SiteName = "Identity Server",
                    PublicOrigin = "https://localhost:44302",
                    SigningCertificate = LoadCertificate(),
                    AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions
                    {
                        EnablePostSignOutAutoRedirect = true,
                        IdentityProviders = ConfigureIdentityProviders
                    }
                };

                idservApp.UseIdentityServer(options);
            });
        }

        X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}Certificates\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory),
                "idsrv3test");
        }

        private void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                AuthenticationType = "Google",
                Caption = "Sign-in with Google",
                SignInAsAuthenticationType = signInAsType,

                ClientId = "648354915405-en8r878sg5lnsli4ndu0q2qji7uvri7o.apps.googleusercontent.com",
                ClientSecret = "v3_er_ztnjmG2nArb_tD7rkV"
            });

            app.UseFacebookAuthentication(new FacebookAuthenticationOptions
            {
                AuthenticationType = "Facebook",
                Caption = "Sign-in with Facebook",
                SignInAsAuthenticationType = signInAsType,

                AppId = "259286991250320",
                AppSecret = "b0038a051797e7c6b6e554ef89dbb02c"
            });

            app.UseMicrosoftAccountAuthentication(new MicrosoftAccountAuthenticationOptions
            {
                AuthenticationType = "Microsoft",
                Caption = "Sign-in with Microsoft",
                SignInAsAuthenticationType = signInAsType,

                ClientId = "a9ecde19-532a-4dce-b2f6-bceeee48e13c",
                ClientSecret = "RDe1dP8xcnuTQaX5txCBOiy"
            });

            app.UseTwitterAuthentication(new Microsoft.Owin.Security.Twitter.TwitterAuthenticationOptions
            {
                AuthenticationType = "Twitter",
                Caption = "Sign-in with Twitter",
                SignInAsAuthenticationType = signInAsType,

                ConsumerKey = "bYow8s9w5OYlS3HMOl666AjX4",
                ConsumerSecret = "LdkjRZESAVjc9x2HseB5loM67qCpJCCv9FVhQXvtju78YJ7ZXe"
            });
        
        }
    }
}