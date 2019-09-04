using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using AWSLambdaDynamoDB;
using AWSLambdaDynamoDB.DynamoDB.ClassTime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AWSLambdaCheckCurrentQuarter.DynamoDB.TableHandler
{
    public class TableHandlerQuarter : TableHandler<Quarter>
    {
        public TableHandlerQuarter(string tableName)
            : base(tableName, new ItemConverterQuarter())
        {
        }

        public async Task<bool> IsItemInTable(Quarter quarter)
        {
            ScanRequest scanRequest = new ScanRequest()
            {
                TableName = tableName,
                Limit = 5,
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    { ":v_" + QuarterAsString.quarter, new AttributeValue(){S = quarter.quarter} }
                },
                FilterExpression = QuarterAsString.quarter + " = " + ":v_" + QuarterAsString.quarter
            };

            ScanResponse scanResponse = await dynamodbClient.ScanAsync(scanRequest);

            return scanResponse.Count > 0;
        }
    }
}
