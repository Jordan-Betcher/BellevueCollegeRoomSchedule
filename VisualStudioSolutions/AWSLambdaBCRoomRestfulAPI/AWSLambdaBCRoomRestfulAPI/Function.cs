using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambdaBCRoomRestfulAPI
{
    public class Function
    {
        public string FunctionHandler(string input, ILambdaContext context)
        {
            PrintBCAPI();
            return "";
        }

        private void PrintBCAPI()
        {
            BCAPIHandler.PrintAll();
        }
    }
}
