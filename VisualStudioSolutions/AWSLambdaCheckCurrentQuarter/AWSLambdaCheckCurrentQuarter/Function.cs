using System;
using Amazon.Lambda.Core;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;
using Amazon;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Transform;
using AWSLambdaDynamoDB;
using AWSLambdaDynamoDB.DynamoDB.ClassTime;
using AWSLambdaCheckCurrentQuarter.DynamoDB;
using AWSLambdaCheckCurrentQuarter.DynamoDB.TableHandler;
using AWSLambdaDynamoDB.DynamoDB;
using AWSLambdaBCRoomRestfulAPI;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambdaCheckCurrentQuarter
{
    public class Function
    {
        const string quarterTableName = "DynamoDBQuarter";
        const string classtimeTableName = "DynamoDBClasstime";

        public async Task<string> CheckCurrentQuarterAsync()
        {
            Quarter currentQuarter = GetCurrentQuarter();
            String currentQuarterCode = currentQuarter.quarter;
            Console.WriteLine("Current Quarter Code: '{0}', Title: {1}", currentQuarterCode, currentQuarter.title);

            AmazonDynamoDBClient dynamodbClient = createDynamoDBClient();
            TableHandlerQuarter tableHandlerQuarter = new TableHandlerQuarter(quarterTableName);
            bool quarterExist = await tableHandlerQuarter.IsItemInTable(currentQuarter);
            Console.WriteLine("Quarter Exists: {0}", quarterExist);
            
            if (quarterExist) // do nothing
            {
                return currentQuarter.title + " quarter up to date.";
            }
            else // update table
            {
                TableHandler<Classtime> tableClasstime = new TableHandler<Classtime>(classtimeTableName, new ItemConverterClasstime());
                
                bool tableExists = await tableClasstime.DoesTableExist();

                if(tableExists)
                {
                    await tableClasstime.EmptyTable();
                }
                else
                {
                    await tableClasstime.CreateTable();
                }

                await BCAPIHandler.StoreAllClasstimesAsync(tableClasstime);

                await tableHandlerQuarter.AddItemAsync(currentQuarter);
                Console.WriteLine("{0} Updated", currentQuarter.quarter);
                return currentQuarterCode + " Updated";
            }
        }

        private AmazonDynamoDBClient createDynamoDBClient()
        {
            AmazonDynamoDBClient dynamodbClient = new AmazonDynamoDBClient(RegionEndpoint.USWest2);
            return dynamodbClient;
        }

        private static CredentialProfile GetDefaultProfile(SharedCredentialsFile sharedCredentialsFile)
        {
            if (sharedCredentialsFile == null)
            {
                throw new ArgumentNullException("Argument sharedCredentialsFile is null");
            }

            foreach (CredentialProfile credentialProfile in sharedCredentialsFile.ListProfiles())
            {
                if (String.Compare(credentialProfile.Name, "default", false) == 0)
                {
                    return credentialProfile;
                }
            }

            return null;
        }

        public Quarter GetCurrentQuarter()
        {
            DeserializeJSONQuarter deserializeCurrentQuarter = CurrentQuarter.GetCurrentQuarter();
            Quarter quarter = deserializeCurrentQuarter.quarter;
            return quarter;
        }
    }
}
