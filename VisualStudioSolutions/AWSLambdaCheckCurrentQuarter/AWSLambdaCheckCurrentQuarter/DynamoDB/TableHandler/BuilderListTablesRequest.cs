using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace AWSLambdaDynamoDB
{
    internal class BuilderListTablesRequest
    {
        internal static async Task<ListTablesResponse> Build(AmazonDynamoDBClient dynamodbClient)
        {
            return await dynamodbClient.ListTablesAsync(new ListTablesRequest());
        }

        internal static async Task<bool> BuildExists(String tableName, AmazonDynamoDBClient dynamodbClient)
        {
            ListTablesResponse listOfTables = await Build(dynamodbClient);
            List<String> listOfTableNames = listOfTables.TableNames;
            bool doesTableExist = listOfTableNames.Contains(tableName);
            return doesTableExist;
        }

        internal static async Task BuildWaitForTargetExistance(bool targetExistance, string tableName, AmazonDynamoDBClient dynamodbClient)
        {
            bool actualExistance = await BuildExists(tableName, dynamodbClient);

            while (actualExistance != targetExistance)
            {
                Thread.Sleep(500);
                actualExistance = await BuildExists(tableName, dynamodbClient);
            }
        }
    }
}