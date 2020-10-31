using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = "";
            using (var client = HttpClientFactory.Create(new CustomMessageHandler()))
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("http://localhost:1234/api/Products"),
                    Method = HttpMethod.Get,
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"Hello:World")));
                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
                result = response.Content.ReadAsStringAsync().Result;
            }
            using (var client = HttpClientFactory.Create(new CustomMessageHandler()))
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("http://localhost:1234/api/Products"),
                    Method = HttpMethod.Post,
                    Content = new StringContent("Pepsi, Juice\r\n")
                };
                request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("text/csv");
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"Hello:World")));
                var response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();
                result = response.Content.ReadAsStringAsync().Result;
            }
            Console.ReadKey();
        }
    }
}
