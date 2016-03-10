using System.Web.Http.Routing;
using System.Web.Routing;

namespace RestAPI
{
    public class LeadsRouteAttribute : RouteFactoryAttribute
    {
        private int routeversion;

        public LeadsRouteAttribute(string template, int version)
            : base(template)
        {
            this.routeversion = version;
        }

        public override System.Collections.Generic.IDictionary<string, object> Constraints
        {
            get
            {
                var constraints = new RouteValueDictionary();
                constraints.Add("LeadsRouteConstraint", new LeadsRouteConstraint(this.routeversion));
                return constraints;
            }
        }
    }
}