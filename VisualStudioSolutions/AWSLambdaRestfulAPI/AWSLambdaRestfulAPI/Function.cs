using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using AWSLambdaRestfulAPI.CurrentQuarter;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambdaRestfulAPI
{
    public class Function
    {
        public string FunctionHandler(string input, ILambdaContext context)
        {
            Quarter quarter = GetCurrentQuarter();
            return quarter.quarter;
        }

        public Quarter GetCurrentQuarter()
        {
            DeserializeJSONQuarter deserializeCurrentQuarter = BCAPIHandler.GetJsonFromAPI();
            Quarter quarter = deserializeCurrentQuarter.quarter;
            return quarter;
        }
    }
}
