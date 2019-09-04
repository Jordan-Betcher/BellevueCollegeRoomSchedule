using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambdaAPILambdaExample
{
    public class Function
    {
        public APIGatewayProxyResponse FunctionHandler(JObject request, ILambdaContext context)
        {
            String buildingLetter = (String)request["BuildingLetter"];
            BCSearchParams bcSearchParams = new BCSearchParams(buildingLetter);
            bcSearchParams.SetRoomNumber(request["RoomNumber"]);
            bcSearchParams.SetDay(request["Day"]);
            bcSearchParams.SetAfterBefore(request["AfterBefore"]);
            bcSearchParams.SetTime(request["Time"]);

            return CreateResponse(bcSearchParams);
        }

        APIGatewayProxyResponse CreateResponse(object result)
        {
            int statusCode = (result != null) ?
                (int)HttpStatusCode.OK :
                (int)HttpStatusCode.InternalServerError;

            string body = (result != null) ?
                JsonConvert.SerializeObject(result) : string.Empty;

            var response = new APIGatewayProxyResponse
            {
                StatusCode = statusCode,
                Body = body,
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" },
                    { "Access-Control-Allow-Origin", "*" }
                }
            };

            return response;
        }
    }
}
