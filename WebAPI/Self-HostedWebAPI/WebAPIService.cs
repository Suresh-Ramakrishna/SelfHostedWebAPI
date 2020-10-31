using Self_HostedWebAPI;
using Self_HostedWebAPI.Filters;
using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;
using System.Web.Http.SelfHost;
using System.Xml.Serialization;

namespace SelfHostedWebAPI
{
    class WebAPIService
    {
        public static int Port = 1234;
        public void Start()
        {
            var config = new HttpSelfHostConfiguration("http://localhost:" + Port);
            config.Initializer = (HttpConfiguration) => { Console.WriteLine("In final stage of initialization"); };
            config.Formatters.Add(new CSVMediaTypeFormatter());
            ConfigureHttpSelfHostConfiguration.Configure(config);
            config.EnsureInitialized();
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}", new { id = RouteParameter.Optional }, constraints: new { id = @"\d+" });
            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            
        }
        public void Stop()
        {
            Console.WriteLine("Stopping WebAPI...");
        }
    }
}