using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Thinktecture.IdentityServer.Core.Models;

namespace RestIdentityServer
{
    public class Client
    {
        private static List<Thinktecture.IdentityServer.Core.Models.Client> clientlist = new List<Thinktecture.IdentityServer.Core.Models.Client>();

        static Client()
        {
            Thinktecture.IdentityServer.Core.Models.Client client1 = new Thinktecture.IdentityServer.Core.Models.Client()
            {
                AccessTokenType = Thinktecture.IdentityServer.Core.Models.AccessTokenType.Reference,
                ClientId = "Client1",
                ClientName = "Client1",
                ClientSecrets = new List<Thinktecture.IdentityServer.Core.Models.ClientSecret>(),
                Enabled = true,
                Flow = Thinktecture.IdentityServer.Core.Models.Flows.ClientCredentials,
                ScopeRestrictions = new List<string>() { "restapi" }
            };
            client1.ClientSecrets.Add(new Thinktecture.IdentityServer.Core.Models.ClientSecret("Client1Secret".Sha256()));

            Thinktecture.IdentityServer.Core.Models.Client client2 = new Thinktecture.IdentityServer.Core.Models.Client()
            {
                AccessTokenType = Thinktecture.IdentityServer.Core.Models.AccessTokenType.Reference,
                ClientId = "Client2",
                ClientName = "Client2",
                ClientSecrets = new List<Thinktecture.IdentityServer.Core.Models.ClientSecret>(),
                Enabled = true,
                Flow = Thinktecture.IdentityServer.Core.Models.Flows.ClientCredentials,
                //ScopeRestrictions = new List<string>() { "restapi" }
                ScopeRestrictions = new List<string>() { "unauthorized" }
            };
            client2.ClientSecrets.Add(new Thinktecture.IdentityServer.Core.Models.ClientSecret("Client2Secret".Sha256()));

            Client.clientlist.Add(client1);
            Client.clientlist.Add(client2);
        }

        public static List<Thinktecture.IdentityServer.Core.Models.Client> ClientList
        {
            get
            {
                return Client.clientlist;
            }
        }
    }
}