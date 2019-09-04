using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace AWSLambdaDynamoDB
{
    internal class BuilderDeleteItemRequest
    {
        internal static async Task<DeleteItemResponse> Build<Item>(Item item, string tableName, ItemConverter<Item> itemConverter, AmazonDynamoDBClient dynamodbClient)
        {
            DeleteItemRequest deleteItemRequest = new DeleteItemRequest()
            {
                TableName = tableName,
                Key = itemConverter.GetItemAsAttributes(item)
            };

            return await dynamodbClient.DeleteItemAsync(deleteItemRequest);
        }
    }
}