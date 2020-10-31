using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Self_HostedWebAPI
{
    class CustomMessageHandlers : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
        {
            Console.WriteLine("Starting CustomMessageHandler");
            var response = await base.SendAsync(request, ct);
            Console.WriteLine("Finished CustomMessageHandler");
            return response;
        }
    }
}
