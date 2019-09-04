using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;

namespace AWSLambdaDynamoDB
{
    internal class BuilderAmazonDynamoDBClient
    {
        internal static AmazonDynamoDBClient Build(RegionEndpoint regionEndpoint)
        {
            SharedCredentialsFile sharedCredentialsFile = new SharedCredentialsFile();
            CredentialProfile defaultProfile = GetDefaultProfile(sharedCredentialsFile);
            AWSCredentials credentials = AWSCredentialsFactory.GetAWSCredentials(defaultProfile, new SharedCredentialsFile());
            return new AmazonDynamoDBClient(credentials, regionEndpoint);
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

    }
}