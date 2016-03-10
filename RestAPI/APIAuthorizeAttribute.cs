using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace RestAPI
{
    public sealed class APIAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            ClaimsPrincipal claimtoken = (actionContext.RequestContext.Principal as ClaimsPrincipal);
            Claim client = claimtoken.FindFirst("client_id");
            if (client == null || string.IsNullOrEmpty(client.Value) || !(string.Compare(client.Value, "Client2", StringComparison.OrdinalIgnoreCase) == 0))
            {
                return false;
            }

            return true;
        }
    }
}