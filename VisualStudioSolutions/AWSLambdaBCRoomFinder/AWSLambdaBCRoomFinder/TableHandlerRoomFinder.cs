using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using AWSLambdaDynamoDB;
using AWSLambdaDynamoDB.DynamoDB;
using AWSLambdaDynamoDB.DynamoDB.ClassTime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWSLambdaBCRoomFinder
{
    public class TableHandlerRoomFinder : TableHandler<Classtime>
    {
        public TableHandlerRoomFinder() : base("DynamoDBClasstime", new ItemConverterClasstime()) { }

        public async Task<List<Classtime>> ScanForRooms(BCSearchParams bcSearchParams)
        {
            String variableBuildingLetter = ":v_" + ClasstimeAsString.building;
            String variableRoomNumber = ":v_" + ClasstimeAsString.roomNumber;
            String variableDay = ":v_" + ClasstimeAsString.day;

            ScanRequest scanRequest = new ScanRequest()
            {
                TableName = base.tableName,
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    { variableBuildingLetter, new AttributeValue(){S = bcSearchParams.buildingLetter} },
                    { variableRoomNumber, new AttributeValue(){S = bcSearchParams.roomNumber} },
                    { variableDay, new AttributeValue(){S = bcSearchParams.day} }
                    
                },
                //ExperssionAttributeNames allows me to swap using "#d" instead of "day", 
                //since "day" is a key word for the request
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    { "#d", ClasstimeAsString.day }
                },
                FilterExpression = ClasstimeAsString.building + " = " + variableBuildingLetter
                                 + " and " + ClasstimeAsString.roomNumber + " = " + variableRoomNumber
                                 + " and " + "#d" + " = " + variableDay
            };

            ScanResponse scanResponse = await dynamodbClient.ScanAsync(scanRequest);

            Console.WriteLine("Number of Classtimes Found For classroom {0}: {1}", 
                bcSearchParams.buildingLetter + bcSearchParams.roomNumber, 
                scanResponse.Count);

            List<Classtime> classtimes = new List<Classtime>();

            foreach(Dictionary<string, AttributeValue> values in scanResponse.Items)
            {
                Classtime classtime = new Classtime()
                {
                    id = values[ClasstimeAsString.id].S,
                    building = values[ClasstimeAsString.building].S,
                    roomNumber = values[ClasstimeAsString.roomNumber].S,
                    day = values[ClasstimeAsString.day].S,
                    startTime = Int32.Parse(values[ClasstimeAsString.startTime].N),
                    endTime = Int32.Parse(values[ClasstimeAsString.endTime].N),
                    classCode = values[ClasstimeAsString.classCode].S
                };

                classtimes.Add(classtime);
            }

            return classtimes;
        }
    }
}
