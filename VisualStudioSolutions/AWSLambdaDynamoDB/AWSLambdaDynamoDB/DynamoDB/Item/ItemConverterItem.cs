using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;

namespace AWSLambdaDynamoDB
{
    class ItemConverterItem : ItemConverter<Item>
    {
        public List<KeySchemaElement> GetKeySchema()
        {
            List<KeySchemaElement> keySchema = new List<KeySchemaElement>()
            {
                new KeySchemaElement()
                {
                    AttributeName = "id",
                    KeyType = "HASH"
                },
                new KeySchemaElement()
                {
                    AttributeName = "number",
                    KeyType = "RANGE"
                }
            };
            return keySchema;
        }

        public List<AttributeDefinition> GetAttributeDefinitions()
        {
            List<AttributeDefinition> attributes = new List<AttributeDefinition>()
            {
                new AttributeDefinition()
                {
                    AttributeName = "id",
                    AttributeType = "S"
                },
                new AttributeDefinition()
                {
                    AttributeName = "number",
                    AttributeType = "N"
                }
            };
            return attributes;
        }

        public Dictionary<string, AttributeValue> GetItemAsAttributes(Item item)
        {
            Dictionary<string, AttributeValue> itemAsAttributes = new Dictionary<string, AttributeValue>()
            {
                {"id", new AttributeValue(){S = item.id} },
                {"number", new AttributeValue(){N = item.number.ToString()} }
            };
            return itemAsAttributes;
        }
    }
}
