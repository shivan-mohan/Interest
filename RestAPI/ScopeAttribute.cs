namespace RestAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    public class ScopeAttribute : AuthorizeAttribute
    {
        private static string _scopeClaimType = "scope";
        private string[] _scopes;

        public static string ScopeClaimType
        {
            get
            {
                return ScopeAttribute._scopeClaimType;
            }

            set
            {
                ScopeAttribute._scopeClaimType = value;
            }
        }

        public ScopeAttribute(params string[] scopes)
        {
            if (scopes == null)
                throw new ArgumentNullException("scopes");
            this._scopes = scopes;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            List<string> list = Enumerable.ToList<string>(Enumerable.Select<Claim, string>(ClaimsPrincipal.Current.FindAll(ScopeAttribute._scopeClaimType), (Func<Claim, string>)(c => c.Value)));
            foreach (string str in this._scopes)
            {
                if (list.Contains(str))
                    return true;
            }

            return false;
        }
    }
}