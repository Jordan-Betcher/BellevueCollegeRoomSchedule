using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWSLambdaDynamoDB.DynamoDB.ClassTime
{
    class ItemConverterClasstime : ItemConverter<Classtime>
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

        public Dictionary<string, AttributeValue> GetItemAsAttributes(Classtime classtime)
        {
            Dictionary<string, AttributeValue> itemAsAttributes = new Dictionary<string, AttributeValue>()
            {
                {ClasstimeAsString.id,          new AttributeValue(){S = classtime.id}                      },
                {ClasstimeAsString.classCode,   new AttributeValue(){S = classtime.classCode}               },
                {ClasstimeAsString.building,    new AttributeValue(){S = classtime.building}                },
                {ClasstimeAsString.roomNumber,  new AttributeValue(){S = classtime.roomNumber}              },
                {ClasstimeAsString.day,         new AttributeValue(){S = classtime.day}                     },
                {ClasstimeAsString.startTime,   new AttributeValue(){N = classtime.startTime.ToString()}    },
                {ClasstimeAsString.endTime,     new AttributeValue(){N = classtime.endTime.ToString()}      }
            };
            return itemAsAttributes;
        }
    }
}
