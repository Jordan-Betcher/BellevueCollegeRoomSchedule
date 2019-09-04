using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;

namespace AWSLambdaDynamoDB
{
    interface ItemConverter<Item>
    {
        List<KeySchemaElement> GetKeySchema();
        List<AttributeDefinition> GetAttributeDefinitions();
        Dictionary<string, AttributeValue> GetItemAsAttributes(Item item);
    }
}