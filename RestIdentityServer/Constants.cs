using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestIdentityServer
{
    public class Constants
    {
        public const string IdServerIssuerURI = "https://localhost:44300/ccidentityissuer";
        public const string IdServer = "https://localhost:44300";
        public const string token = IdServer + "/connect/token";
        public const string authorize = IdServer + "/connect/authorize";
    }
}