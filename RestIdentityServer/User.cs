using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestIdentityServer
{
    public class User
    {
        private static List<Thinktecture.IdentityServer.Core.Services.InMemory.InMemoryUser> userlist = new List<Thinktecture.IdentityServer.Core.Services.InMemory.InMemoryUser>();

        static User()
        {
            Thinktecture.IdentityServer.Core.Services.InMemory.InMemoryUser user = new Thinktecture.IdentityServer.Core.Services.InMemory.InMemoryUser();
            user.Username = "Dong";
            user.Password = "Dong";
            user.Subject = "dong@dong.com";
            user.Claims = new List<System.Security.Claims.Claim>();
            System.Security.Claims.Claim claim = new System.Security.Claims.Claim(Thinktecture.IdentityServer.Core.Constants.ClaimTypes.Email, "dong@dong.com");
            user.Claims.ToList().Add(claim);
            User.userlist.Add(user);
        }

        public static List<Thinktecture.IdentityServer.Core.Services.InMemory.InMemoryUser> UserList
        {
            get
            {
                return User.userlist;
            }
        }
    }
}