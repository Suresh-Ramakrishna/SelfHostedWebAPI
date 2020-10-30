using System;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.SelfHost;

namespace SelfHostedWebAPI
{
    class WebAPIService
    {
        public static int Port = 1234;
        public void Start()
        {
            var config = new HttpSelfHostConfiguration("http://localhost:" + Port);
            ConfigureHttpSelfHostConfiguration.Configure(config);
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}", new { id = RouteParameter.Optional });

            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
        }
        public void Stop()
        {
            Console.WriteLine("Stopping WebAPI...");
        }
    }
}