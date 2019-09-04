using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace AWSLambdaDynamoDB
{
    internal class BuilderCreateTableRequest
    {
        internal static async Task<CreateTableResponse> Build<Item>(string tableName, ItemConverter<Item> itemConverter, AmazonDynamoDBClient dynamodbClient)
        {
            CreateTableRequest createTableRequest = new CreateTableRequest()
            {
                TableName = tableName,
                AttributeDefinitions = itemConverter.GetAttributeDefinitions(),
                KeySchema = itemConverter.GetKeySchema(),
                ProvisionedThroughput = new ProvisionedThroughput()
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                }
            };

            return await dynamodbClient.CreateTableAsync(createTableRequest);
        }
    }
}