using System;
using AWSLambdaBCRoomRestfulAPI.CurrentQuarter;
using Newtonsoft.Json;

namespace AWSLambdaBCRoomRestfulAPI
{
    internal class BuilderQuarter
    {
        private const string API = "https://www2.bellevuecollege.edu/data/";
        private const string PARAMETER_CURRENT_QUARTER = "api/v1/quarter/current";

        internal static Quarter Build()
        {
            String parameter = PARAMETER_CURRENT_QUARTER;
            String jsonQuarter = BuilderAPI.GetJsonFromAPI(API, parameter);
            DeserializeQuarter deserializedQuarter = JsonConvert.DeserializeObject<DeserializeQuarter>(jsonQuarter);
            Quarter quarter = deserializedQuarter.quarter;
            return quarter;
        }
    }
}