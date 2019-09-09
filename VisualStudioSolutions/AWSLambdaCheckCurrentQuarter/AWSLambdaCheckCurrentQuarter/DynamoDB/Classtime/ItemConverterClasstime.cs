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
            /*
            Console.Write("id: {0}", classtime.id);
            Console.Write(", classCode: {0}", classtime.classCode);
            Console.Write(", building: {0}", classtime.building);
            Console.Write(", roomNumber: {0}", classtime.roomNumber);
            Console.Write(", day: {0}", classtime.day);
            Console.Write(", startTime: {0}", classtime.startTime);
            Console.Write(", endTime: {0}", classtime.endTime);
            Console.WriteLine();
            //*/

            Dictionary<string, AttributeValue> itemAsAttributes = new Dictionary<string, AttributeValue>()
            {
                {ClasstimeAsString.id, new AttributeValue(){S = classtime.id} },
                {ClasstimeAsString.classCode, new AttributeValue(){S = classtime.classCode} },
                {ClasstimeAsString.building, new AttributeValue(){S = classtime.building} },
                {ClasstimeAsString.roomNumber, new AttributeValue(){S = classtime.roomNumber} },
                {ClasstimeAsString.day, new AttributeValue(){S = classtime.day} },
                {ClasstimeAsString.startTime, new AttributeValue(){N = classtime.startTime.ToString()} },
                {ClasstimeAsString.endTime, new AttributeValue(){N = classtime.endTime.ToString()} }
            };
            return itemAsAttributes;
        }
    }
}
