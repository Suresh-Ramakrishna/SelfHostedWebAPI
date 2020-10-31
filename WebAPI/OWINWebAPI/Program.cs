using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace OWINWebAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x => // x is Topshelf.HostConfigurators.HostConfigurator
            {
                x.Service<OWINService>();
                x.RunAsLocalSystem(); //Runs the service using the local system account.
                x.StartAutomatically(); //Sets whether the service should start automatically on system start-up
            });
        }
    }
    public class OWINService : ServiceControl
    {
        public bool Start(HostControl hostControl)
        {
            string baseAddress = "http://localhost:9000/";
            WebApp.Start<Startup>(baseAddress);
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            return true;
        }
    }
}
