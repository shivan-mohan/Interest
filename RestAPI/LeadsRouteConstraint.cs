using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;

namespace RestAPI
{
    public class LeadsRouteConstraint : IHttpRouteConstraint
    {
        private int defaultrouteversion;
        private int routeversion;

        public LeadsRouteConstraint(int version)
        {
            this.defaultrouteversion = 1; //constant
            this.routeversion = version;
        }

        #region " IHttpRouteConstraint Members "

        public bool Match(System.Net.Http.HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            int? apiversion = this.GetAPIContentVersion(request);

            if (!apiversion.HasValue) { apiversion = this.GetAPIHeaderVersion(request); }

            if (!apiversion.HasValue) { apiversion = this.defaultrouteversion; }
            return this.routeversion == apiversion.Value;
        }

        #endregion " IHttpRouteConstraint Members "

        private int? GetAPIContentVersion(System.Net.Http.HttpRequestMessage request)
        {
            List<string> mediatypelist = request.Headers.Accept.Select(header => header.MediaType).ToList();
            if (!(mediatypelist == null || mediatypelist.Count == 0))
            {
                if (mediatypelist.Any(media => media.Contains("vnd.leadsapi")))
                {
                    string contenttype = mediatypelist.Where(media => media.Contains("vnd.leadsapi")).First();
                    string[] contenttypesplit = contenttype.Split(new[] { "." }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (!(contenttypesplit == null || contenttypesplit.Count() == 0) && contenttypesplit.Count() > 2)
                    {
                        int apirouteversion;
                        string strapirouteversion = contenttypesplit[2];
                        if(!string.IsNullOrEmpty(strapirouteversion) && strapirouteversion.Count() > 1)
                        {
                            int.TryParse(strapirouteversion[1].ToString(), out apirouteversion);
                            return apirouteversion;
                        }
                    }
                }
            }

            return null;
        }

        private int? GetAPIHeaderVersion(System.Net.Http.HttpRequestMessage request)
        {
            IEnumerable<string> headervaluelist;
            request.Headers.TryGetValues("api-version", out headervaluelist);

            if (!(headervaluelist == null || headervaluelist.Count() == 0))
            {
                int apirouteversion;
                string strapirouteversion = headervaluelist.First();
                int.TryParse(strapirouteversion, out apirouteversion);
                return apirouteversion;
            }

            return null;
        }
    }
}