using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AWSLambdaDynamoDB;
using AWSLambdaDynamoDB.DynamoDB.ClassTime;
using AWSLambdaDynamoDB.DynamoDB;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambdaBCRoomFinder
{
    public class Function
    {
        //public async Task<APIGatewayProxyResponse> FunctionHandler(JObject request, ILambdaContext context)
        public async Task<List<Classtime>> FunctionHandler(JObject request, ILambdaContext context)
        {
            String buildingLetter = (String)request["BuildingLetter"];
            String roomNumber = (String)request["RoomNumber"];
            String day = (String)request["Day"];
            BCSearchParams bcSearchParams = new BCSearchParams(buildingLetter, roomNumber, day);

            TableHandlerRoomFinder tableHandler = new TableHandlerRoomFinder();
            List<Classtime> classtimes = await tableHandler.ScanForRooms(bcSearchParams);

            //Dictionary<String, object> headers = new Dictionary<string, object>();
            //headers.Add("Access-Control-Allow-Origin", "*");

            //Dictionary<String, object> response = new Dictionary<string, object>();
            //response.Add("statusCode", 200);
            //response.Add("headers", headers);
            //response.Add("body", classtimes);

            return classtimes;
            //return CreateResponse(classtimes);
        }

        APIGatewayProxyResponse CreateResponse(object result)
        {
            string body;
            int statusCode;

            if(result is string)
            {
                statusCode = (int)HttpStatusCode.OK;
                body = (string)result;
            }
            if (result != null)
            {
                statusCode = (int)HttpStatusCode.OK;
                body = JsonConvert.SerializeObject(result);
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                body = "";
            }

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
