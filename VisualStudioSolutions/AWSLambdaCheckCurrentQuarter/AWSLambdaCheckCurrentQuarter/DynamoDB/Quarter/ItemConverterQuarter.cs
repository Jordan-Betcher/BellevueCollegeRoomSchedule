using Amazon.DynamoDBv2.Model;
using AWSLambdaCheckCurrentQuarter.DynamoDB;
using System.Collections.Generic;

namespace AWSLambdaDynamoDB.DynamoDB.ClassTime
{
    public class ItemConverterQuarter : ItemConverter<Quarter>
    {
        //keySchema max is 2, attributeDefinitions has to match keySchema
        public List<KeySchemaElement> GetKeySchema()
        {
            List<KeySchemaElement> keySchema = new List<KeySchemaElement>()
            {
                new KeySchemaElement()
                {
                    AttributeName = ClasstimeAsString.id,
                    KeyType = "HASH"
                },
                new KeySchemaElement()
                {
                    AttributeName = ClasstimeAsString.building,
                    KeyType = "RANGE"
                }
            };
            return keySchema;
        }

        public List<AttributeDefinition> GetAttributeDefinitions()
        {
            List<AttributeDefinition> attributeDefinitions = new List<AttributeDefinition>()
            {
                new AttributeDefinition()
                {
                    AttributeName = ClasstimeAsString.id,
                    AttributeType = ClasstimeType.id
                },
                new AttributeDefinition()
                {
                    AttributeName = ClasstimeAsString.building,
                    AttributeType = ClasstimeType.building
                }
            };
            return attributeDefinitions;
        }

        public Dictionary<string, AttributeValue> GetItemAsAttributes(Quarter quarter)
        {
            Dictionary<string, AttributeValue> itemAsAttributes = new Dictionary<string, AttributeValue>()
            {
                {QuarterAsString.quarter, new AttributeValue(){S = quarter.quarter} },
                {QuarterAsString.title, new AttributeValue(){S = quarter.title} }
            };
            return itemAsAttributes;
        }
    }
}
