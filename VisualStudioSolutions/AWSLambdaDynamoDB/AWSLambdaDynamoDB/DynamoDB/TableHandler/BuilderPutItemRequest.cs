using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace AWSLambdaDynamoDB
{
    internal class BuilderPutItemRequest
    {
        internal static async Task<PutItemResponse> Build<Item>(Item item, string tableName, ItemConverter<Item> itemConverter, AmazonDynamoDBClient dynamodbClient)
        {
            PutItemRequest putItemRequest = new PutItemRequest()
            {
                TableName = tableName,
                Item = itemConverter.GetItemAsAttributes(item)
            };

            return await dynamodbClient.PutItemAsync(putItemRequest);
        }
    }
}