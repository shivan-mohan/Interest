using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RestAPI.WebApiApplication))]

namespace RestAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(new Thinktecture.IdentityServer.AccessTokenValidation.IdentityServerBearerTokenAuthenticationOptions()
                {
                    Authority = "https://localhost:44300/"
                    ,
                    RequiredScopes = new[]
                    {
                        "restapi", "read", "unauthorized"
                    }
                });

            app.UseWebApi(WebApiConfig.Register());
        }
    }
}