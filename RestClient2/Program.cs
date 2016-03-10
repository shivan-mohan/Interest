using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Marvin.JsonPatch;
using Microsoft.Owin.Builder;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using Newtonsoft.Json;
using Owin;
using Thinktecture.IdentityModel.Client;

namespace RestClient2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                entrypoint();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        async private static void entrypoint()
        {
            TokenResponse tokenresponse = await authenticate();
            await asyncpost(tokenresponse);
            ////await asyncput();
            //await asyncpatch();
        }

        async private static Task<TokenResponse> authenticate()
        {
            OAuth2Client client = null;
            await Task.Run(() => client = new OAuth2Client(new Uri("https://localhost:44300/connect/token"), "Client2", "Client2Secret"));
            return new TaskFactory<TokenResponse>().StartNew(() => client.RequestClientCredentialsAsync("unauthorized").Result).Result;
        }

        async private static Task<bool> asyncpost(TokenResponse tokenresponse)
        {
            HttpClient httpclient = new HttpClient(new HttpClientHandler());
            httpclient.SetBearerToken(tokenresponse.AccessToken);
            HttpContent content = null;
            HttpResponseMessage response = null;
            Dictionary<string, object> post_values = new Dictionary<string, object>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            post_values.Add("Id", 0);
            post_values.Add("Name", "Shivan");
            post_values.Add("Address", "1 Street");
            content = new StringContent(serializer.Serialize(post_values));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await httpclient.PostAsync("http://localhost:59125/api/Leads", content, new CancellationToken(false)).ContinueWith<HttpResponseMessage>(s => { response = s.Result; return s.Result; });
            string postresponsestring = response.Content.ReadAsStringAsync().Result;

            HttpRequestMessage request = new HttpRequestMessage();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.leadsapi.v2+json"));
            //request.Headers.Add("api-version", "2");
            request.Method = HttpMethod.Get;
            request.RequestUri = new Uri("http://localhost:59125/api/Leads/1");
            await httpclient.SendAsync(request, new CancellationToken(false)).ContinueWith<HttpResponseMessage>(s => { response = s.Result; return s.Result; });
            string getresponsestring = response.Content.ReadAsStringAsync().Result;

            return new TaskFactory<bool>().StartNew(() => true).Result;
        }

        async private static Task<bool> asyncput()
        {
            HttpClient httpclient = new HttpClient(new HttpClientHandler());
            HttpContent content = null;
            HttpResponseMessage response = null;
            Dictionary<string, object> post_values = new Dictionary<string, object>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            post_values.Add("Id", 1);
            post_values.Add("Name", "General Dong");
            post_values.Add("Address", "Dongrila");
            content = new StringContent(serializer.Serialize(post_values));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            await httpclient.PutAsync("http://shivan-restapi.culturalcare.com/api/Leads", content, new CancellationToken(false)).ContinueWith<HttpResponseMessage>(s => { response = s.Result; return s.Result; });
            string postresponsestring = response.Content.ReadAsStringAsync().Result;

            await httpclient.GetAsync("http://shivan-restapi.culturalcare.com/api/Leads/1", new CancellationToken(false)).ContinueWith<HttpResponseMessage>(s => { response = s.Result; return s.Result; });
            string getresponsestring = response.Content.ReadAsStringAsync().Result;

            return new TaskFactory<bool>().StartNew(() => true).Result;
        }

        async private static Task<bool> asyncpatch()
        {
            HttpClient httpclient = new HttpClient(new HttpClientHandler());
            HttpContent content = null;
            HttpResponseMessage response = null;
            Dictionary<string, object> post_op1 = new Dictionary<string, object>();
            Dictionary<string, object> post_op2 = new Dictionary<string, object>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            JsonPatchDocument<Lead> patchdoc = new JsonPatchDocument<Lead>();
            patchdoc.Replace(l => l.Name, "General Dong");
            patchdoc.Replace(l => l.Address, "Dongrila");

            //post_op1.Add("op", "replace");
            //post_op1.Add("path", "Name");
            //post_op1.Add("value", "General Dong");

            //post_op2.Add("op", "replace");
            //post_op2.Add("path", "Address");
            //post_op2.Add("value", "Dongrila");
            //content = new StringContent("[" + serializer.Serialize(post_op1) + "," + serializer.Serialize(post_op2) + "]");
            content = new StringContent(JsonConvert.SerializeObject(patchdoc));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpRequestMessage request = new HttpRequestMessage();
            request.Content = content;
            request.Method = new HttpMethod("PATCH");
            request.RequestUri = new Uri("http://shivan-restapi.culturalcare.com/api/Leads/1");
            await httpclient.SendAsync(request, new CancellationToken(false)).ContinueWith<HttpResponseMessage>(s => { response = s.Result; return s.Result; });
            string postresponsestring = response.Content.ReadAsStringAsync().Result;

            await httpclient.GetAsync("http://shivan-restapi.culturalcare.com/api/Leads/1", new CancellationToken(false)).ContinueWith<HttpResponseMessage>(s => { response = s.Result; return s.Result; });
            string getresponsestring = response.Content.ReadAsStringAsync().Result;

            return new TaskFactory<bool>().StartNew(() => true).Result;
        }
    }
}