using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using AWSLambdaDynamoDB.DynamoDB;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AWSLambdaDynamoDB
{
    public class TableHandler<Item>
    {
        protected RegionEndpoint REGION_ENDPOINT = RegionEndpoint.USWest2;
        protected ItemConverter<Item> itemConverter;
        protected String tableName;
        protected AmazonDynamoDBClient dynamodbClient;

        public TableHandler(String tableName, ItemConverter<Item> itemConverter, AmazonDynamoDBClient dynamodbClient)
        {
            this.tableName = tableName;
            this.itemConverter = itemConverter;
            this.dynamodbClient = dynamodbClient;
        }

        public TableHandler(String tableName, ItemConverter<Item> itemConverter)
        {
            this.tableName = tableName;
            this.itemConverter = itemConverter;
            this.dynamodbClient = new AmazonDynamoDBClient(REGION_ENDPOINT);
        }

        public async Task WaitTillTableIsStatus(TableStatus tableStatus)
        {
            await BuilderDescribeTableRequest.BuildWaitTillMatchingStatus(tableStatus, tableName, dynamodbClient);
        }

        public async Task<bool> ContainsItemMatchingRoom(Classtime classtime)
        {
            ScanRequest scanRequest = new ScanRequest()
            {
                TableName = tableName,
                Limit = 5,
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    { ":v_" + ClasstimeAsString.building, new AttributeValue(){S = classtime.building} },
                    { ":v_" + ClasstimeAsString.roomNumber, new AttributeValue(){S = classtime.roomNumber} }
                },
                FilterExpression = ClasstimeAsString.building + " = " + ":v_" + ClasstimeAsString.building
                          + " and " + ClasstimeAsString.roomNumber + " = " + ":v_" + ClasstimeAsString.roomNumber
            };

            ScanResponse scanResponse = await dynamodbClient.ScanAsync(scanRequest);

            return scanResponse.Count > 0;
        }

        public async Task<bool> TableIsStatus(TableStatus tableStatus)
        {
            return await BuilderDescribeTableRequest.BuildMatchingStatus(tableStatus, tableName, dynamodbClient);
        }

        internal async Task WaitTillTableExists(bool targetExistance)
        {
            await BuilderListTablesRequest.BuildWaitForTargetExistance(targetExistance, tableName, dynamodbClient);
        }

        public async Task<bool> DoesTableExist()
        {
            return await BuilderListTablesRequest.BuildExists(tableName, dynamodbClient);
        }

        public async Task<long> GetCountAsync()
        {
            return await BuilderDescribeTableRequest.BuildCount(tableName, dynamodbClient);
        }

        public async Task<List<BatchWriteItemResponse>> AddItemsInBatchAsync(List<Item> items)
        {
            return await BuilderBatchWriteItemRequest.BuildAdd(items, tableName, itemConverter, dynamodbClient);
        }

        public async Task<List<BatchWriteItemResponse>> DeleteItemsInBatchAsync(List<Item> items)
        {
            return await BuilderBatchWriteItemRequest.BuildDelete(items, tableName, itemConverter, dynamodbClient);
        }

        public async Task<DeleteItemResponse> DeleteItemAsync(Item item)
        {
            return await BuilderDeleteItemRequest.Build(item, tableName, itemConverter, dynamodbClient);
        }

        public async Task<PutItemResponse> AddItemAsync(Item item)
        {
            return await BuilderPutItemRequest.Build(item, tableName, itemConverter, dynamodbClient);
        }

        public async Task<CreateTableResponse> CreateTableAsync()
        {
            return await BuilderCreateTableRequest.Build(tableName, itemConverter, dynamodbClient);
        }

        public async Task<DeleteTableResponse> DeleteTableAsync()
        {
            return await BuilderDeleteTableRequest.Build(tableName, dynamodbClient);
        }
    }
}
