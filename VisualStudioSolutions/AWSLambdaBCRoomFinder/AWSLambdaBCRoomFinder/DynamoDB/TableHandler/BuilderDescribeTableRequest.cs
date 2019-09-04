using System;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace AWSLambdaDynamoDB
{
    internal class BuilderDescribeTableRequest
    {
        internal static async Task<DescribeTableResponse> Build(string tableName, AmazonDynamoDBClient dynamodbClient)
        {
            DescribeTableRequest describeTableRequest = new DescribeTableRequest()
            {
                TableName = tableName
            };

            return await dynamodbClient.DescribeTableAsync(describeTableRequest);
        }

        internal static async Task<long> BuildCount(string tableName, AmazonDynamoDBClient dynamodbClient)
        {
            DescribeTableResponse describeTableResponse = await Build(tableName, dynamodbClient);
            long count = describeTableResponse.Table.ItemCount;
            return count;
        }

        internal static async Task<bool> BuildMatchingStatus(TableStatus tableStatus, string tableName, AmazonDynamoDBClient dynamodbClient)
        {
            DescribeTableResponse describeTableResponse = await Build(tableName, dynamodbClient);
            bool isMatchingStatus = describeTableResponse.Table.TableStatus == tableStatus;
            return isMatchingStatus;
        }

        internal static async Task BuildWaitTillMatchingStatus(TableStatus tableStatus, string tableName, AmazonDynamoDBClient dynamodbClient)
        {
            bool isStatus = await BuildMatchingStatus(tableStatus, tableName, dynamodbClient);

            while (isStatus == false)
            {
                Thread.Sleep(500);
                isStatus = await BuildMatchingStatus(tableStatus, tableName, dynamodbClient);
            }
        }
    }
}