using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HTTPS
{
    class Program
    {
        static void Main(string[] args)
        {

            HttpClient httpClient = new HttpClient();

            // Specify to use TLS 1.2 as default connection
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            httpClient.BaseAddress = new Uri("https://foobar.com/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            
            httpClient.PostAsXmlAsync("api/SaveData", "request");

        }
    }
}
