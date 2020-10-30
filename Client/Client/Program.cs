using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("http://localhost:1234/api/Numbers"),
                    Method = HttpMethod.Get,
                };
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"Hello2:World")));
                var responseTask = client.SendAsync(request).ContinueWith(t =>
                {
                    if(t.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var response = t.Result.Content.ReadAsAsync<JObject>();
                        response.Wait();
                        Console.WriteLine(response.Result);
                    }
                    else
                    {
                        Console.WriteLine(t.Result);
                    }
                });
                responseTask.Wait();
            }
            Console.ReadKey();
        }
    }
}
