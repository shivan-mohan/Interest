using System.Collections.Generic;

namespace RestIdentityServer
{
    public class Scope
    {
        private static List<Thinktecture.IdentityServer.Core.Models.Scope> scopelist = new List<Thinktecture.IdentityServer.Core.Models.Scope>();

        static Scope()
        {
            scopelist.Add(new Thinktecture.IdentityServer.Core.Models.Scope()
            {
                Enabled = true
                     ,
                Name = "restapi"
                     ,
                Type = Thinktecture.IdentityServer.Core.Models.ScopeType.Resource
            });

            scopelist.Add(new Thinktecture.IdentityServer.Core.Models.Scope()
            {
                Enabled = true
                     ,
                Name = "read"
                     ,
                Type = Thinktecture.IdentityServer.Core.Models.ScopeType.Resource
            });

            scopelist.Add(new Thinktecture.IdentityServer.Core.Models.Scope()
            {
                Enabled = true
                     ,
                Name = "unauthorized"
                     ,
                Type = Thinktecture.IdentityServer.Core.Models.ScopeType.Resource
            });
        }

        public static List<Thinktecture.IdentityServer.Core.Models.Scope> ScopeList
        {
            get
            {
                return Scope.scopelist;
            }
        }
    }
}