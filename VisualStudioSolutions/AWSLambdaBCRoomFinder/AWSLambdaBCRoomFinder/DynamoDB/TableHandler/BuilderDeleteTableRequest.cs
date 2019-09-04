using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace AWSLambdaDynamoDB
{
    internal class BuilderDeleteTableRequest
    {
        internal static async Task<DeleteTableResponse> Build(string tableName, AmazonDynamoDBClient dynamodbClient)
        {
            DeleteTableRequest deleteTableRequest = new DeleteTableRequest()
            {
                TableName = tableName
            };

            return await dynamodbClient.DeleteTableAsync(deleteTableRequest);
        }
    }
}