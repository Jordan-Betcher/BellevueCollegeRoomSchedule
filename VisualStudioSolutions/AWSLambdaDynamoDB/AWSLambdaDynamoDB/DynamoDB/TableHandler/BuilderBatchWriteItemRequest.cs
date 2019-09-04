using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace AWSLambdaDynamoDB
{
    internal class BuilderBatchWriteItemRequest
    {
        const int MAX_LENGTH_OF_BATCH = 25;

        internal static async Task<List<BatchWriteItemResponse>> BuildAdd<Item>(List<Item> items, string tableName, ItemConverter<Item> itemConverter, AmazonDynamoDBClient dynamodbClient)
        {
            Item[] items25 = new Item[MAX_LENGTH_OF_BATCH];
            List<BatchWriteItemResponse> responses = new List<BatchWriteItemResponse>();

            for (int index = 0; index < items.Count; index += 25)
            {
                int remaining = items.Count;
                remaining = remaining - index;

                if(items25.Length > remaining)
                {
                    items.CopyTo(index, items25, 0, remaining);
                }
                else
                {
                    items.CopyTo(index, items25, 0, items25.Length);
                }

                BatchWriteItemResponse response = await BuildAdd(items25, tableName, itemConverter, dynamodbClient);
                responses.Add(response);
            }

            return responses;
        }

        private static async Task<BatchWriteItemResponse> BuildAdd<Item>(Item[] items, string tableName, ItemConverter<Item> itemConverter, AmazonDynamoDBClient dynamodbClient)
        {
            List<WriteRequest> writeRequests = new List<WriteRequest>();
            foreach (Item item in items)
            {
                PutRequest putRequest = new PutRequest()
                {
                    Item = itemConverter.GetItemAsAttributes(item)
                };

                WriteRequest writeRequest = new WriteRequest()
                {
                    PutRequest = putRequest
                };

                writeRequests.Add(writeRequest);
            }

            BatchWriteItemRequest batchWriteItemRequest = new BatchWriteItemRequest()
            {
                RequestItems = new Dictionary<string, List<WriteRequest>>()
                {
                    {
                        tableName,
                        writeRequests
                    }
                }
            };

            return await dynamodbClient.BatchWriteItemAsync(batchWriteItemRequest);
        }

        internal static async Task<List<BatchWriteItemResponse>> BuildDelete<Item>(List<Item> items, string tableName, ItemConverter<Item> itemConverter, AmazonDynamoDBClient dynamodbClient)
        {
            Item[] items25 = new Item[MAX_LENGTH_OF_BATCH];
            List<BatchWriteItemResponse> responses = new List<BatchWriteItemResponse>();

            for (int index = 0; index < items.Count; index += 25)
            {
                int remaining = items.Count;
                remaining = remaining - index;

                if (items25.Length > remaining)
                {
                    items.CopyTo(index, items25, 0, remaining);
                }
                else
                {
                    items.CopyTo(index, items25, 0, items25.Length);
                }

                BatchWriteItemResponse response = await BuildDelete(items25, tableName, itemConverter, dynamodbClient);
                responses.Add(response);
            }

            return responses;
        }

        private static async Task<BatchWriteItemResponse> BuildDelete<Item>(Item[] items, string tableName, ItemConverter<Item> itemConverter, AmazonDynamoDBClient dynamodbClient)
        {
            List<WriteRequest> writeRequests = new List<WriteRequest>();
            foreach (Item item in items)
            {
                DeleteRequest deleteRequest = new DeleteRequest()
                {
                    Key = itemConverter.GetItemAsAttributes(item)
                };

                WriteRequest writeRequest = new WriteRequest()
                {
                    DeleteRequest = deleteRequest
                };

                writeRequests.Add(writeRequest);
            }

            BatchWriteItemRequest batchWriteItemRequest = new BatchWriteItemRequest()
            {
                RequestItems = new Dictionary<string, List<WriteRequest>>()
                {
                    {
                        tableName,
                        writeRequests
                    }
                }
            };

            return await dynamodbClient.BatchWriteItemAsync(batchWriteItemRequest);
        }
    }
}