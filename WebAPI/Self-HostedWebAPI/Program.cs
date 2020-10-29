using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Self_HostedWebAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x => // x is Topshelf.HostConfigurators.HostConfigurator
            {
                x.Service<WebAPIService>(s =>
                {

                    s.ConstructUsing(name => new WebAPIService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.AddCommandLineDefinition("port", port => WebAPIService.Port = int.Parse(port));
                x.SetServiceName("WebAPIService"); //Sets the name of the service
                x.SetDisplayName("WebAPIService"); //Sets the display name of the service
                x.RunAsLocalSystem(); //Runs the service using the local system account.
                x.StartManually(); //Sets whether the service should start automatically on system start-up
            });
        }
    }
}
