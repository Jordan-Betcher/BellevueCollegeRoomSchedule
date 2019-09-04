using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AWSLambdaRestfulAPI
{
    internal class BuilderAPI
    {
        internal static string GetJsonFromAPI(string url, string parameters)
        {
            String content = "";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(parameters).Result;
            if (response.IsSuccessStatusCode)
            {
                content += response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            client.Dispose();

            return content;
        }
    }
}