using System.Security.Cryptography.X509Certificates;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RestIdentityServer.Start))]

namespace RestIdentityServer
{
    public class Start
    {
        public void Configuration(IAppBuilder userapp)
        {
            userapp.UseIdentityServer(new Thinktecture.IdentityServer.Core.Configuration.IdentityServerOptions()
            {
                Factory = Thinktecture.IdentityServer.Core.Configuration.InMemoryFactory.Create(User.UserList, Client.ClientList, Scope.ScopeList)
            });
        }
    }
}