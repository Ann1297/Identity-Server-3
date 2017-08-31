using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace IdentityServer.Config
{
    public class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "lilly",
                    Password = "secret",
                    Subject = "	8743b5-2063cd84097-a65d1633-f5c74f5",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Lilly"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Evans"),
                        new Claim(Constants.ClaimTypes.Role, "Operator")
                    }
                },
                new InMemoryUser
                {
                    Username = "James",
                    Password = "secret",
                    Subject = "	453f45-092b8010gy6-ft561946-f5c754g3",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "James"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Potter"),
                        new Claim(Constants.ClaimTypes.Role, "Geek")
                    }
                }
            };
        }
    }
}