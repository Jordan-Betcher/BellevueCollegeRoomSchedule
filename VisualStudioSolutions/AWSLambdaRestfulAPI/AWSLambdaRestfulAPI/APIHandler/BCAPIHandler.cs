using AWSLambdaRestfulAPI.CurrentQuarter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AWSLambdaRestfulAPI
{
    public class BCAPIHandler
    {
        private const string API = "https://www2.bellevuecollege.edu/data/";
        private const string PARAMETER_CURRENT_QUARTER = "api/v1/quarter/current";

        public static DeserializeJSONQuarter GetJsonFromAPI()
        {
            String jsonCurrentQuarter = BuilderAPI.GetJsonFromAPI(API, PARAMETER_CURRENT_QUARTER);
            DeserializeJSONQuarter infoCurrentQuarter = JsonConvert.DeserializeObject<DeserializeJSONQuarter>(jsonCurrentQuarter);
            return infoCurrentQuarter;
        }
    }
}
