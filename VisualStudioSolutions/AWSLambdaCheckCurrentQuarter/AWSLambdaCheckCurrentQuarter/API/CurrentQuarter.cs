using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;
using Amazon;
using System.Collections.Generic;
using AWSLambdaCheckCurrentQuarter.DynamoDB;

namespace AWSLambdaCheckCurrentQuarter
{
    public class CurrentQuarter
    {
        public const string API = "https://www2.bellevuecollege.edu/data/";
        public const string PARAMETER_CURRENT_QUARTER = "api/v1/quarter/current";

        public static DeserializeJSONQuarter GetCurrentQuarter()
        {
            String jsonCurrentQuarter = GetCurrentQuarterAsJSONString();
            DeserializeJSONQuarter infoCurrentQuarter = JsonConvert.DeserializeObject<DeserializeJSONQuarter>(jsonCurrentQuarter);
            return infoCurrentQuarter;
        }

        public static String GetCurrentQuarterAsJSONString()
        {
            return GetJSONStringOfRestfulAPI(API, PARAMETER_CURRENT_QUARTER);
        }

        public static String GetJSONStringOfRestfulAPI(String url, String parameters)
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

    /*
     * Everything below is named this way for the JSON Deserilaztion of 
     * the json string returned from CurrentQuarterURLs class (above)
     */
    public class DeserializeJSONQuarter
    {
        public Quarter quarter { get; set; }
    }
}