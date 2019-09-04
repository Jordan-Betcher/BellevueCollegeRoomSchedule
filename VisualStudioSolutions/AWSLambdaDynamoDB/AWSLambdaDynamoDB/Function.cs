using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.Lambda.Core;
using AWSLambdaDynamoDB.DynamoDB;
using AWSLambdaDynamoDB.DynamoDB.ClassTime;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambdaDynamoDB
{
    public class Function
    {
        private String TableName = "TestTable";
        private String TableNameClasstime = "TestTableClasstime";

        public async Task<String> FunctionHandlerAsync(string input, ILambdaContext context)
        {
            await ContainsItemMatchingRoom();

            return input;
        }

        private async Task ContainsItemMatchingRoom()
        {
            TableHandler<Classtime> requestHandler = new TableHandler<Classtime>(TableNameClasstime, new ItemConverterClasstime());

            List<Classtime> items = new List<Classtime>()
            {
                new Classtime(){id="1", classCode="CS455", building="L", roomNumber="218", day="Monday", startTime=1630, endTime=1720}
            };

            bool tableContains = await requestHandler.ContainsItemMatchingRoom(items[0]);

            Console.WriteLine("Table Contains Item: {0}", tableContains);
        }

        private async Task ClasstimeAddItem()
        {
            TableHandler<Classtime> requestHandler = new TableHandler<Classtime>(TableNameClasstime, new ItemConverterClasstime());

            List<Classtime> items = new List<Classtime>()
            {
                new Classtime(){id="1", classCode="CS455", building="L", roomNumber="218", day="Monday", startTime=1630, endTime=1720}
            };

            await requestHandler.AddItemAsync(items[0]);

            Console.WriteLine("Added Item");
        }

        public async Task ClasstimeCreateTable()
        {
            TableHandler<Classtime> requestHandler = new TableHandler<Classtime>(TableNameClasstime, new ItemConverterClasstime());
            await requestHandler.CreateTableAsync();
            Console.WriteLine("Table Created");
        }

        private async Task DeleteMoreThan25InBatch()
        {
            TableHandler<Item> requestHandler = new TableHandler<Item>(TableName, new ItemConverterItem());

            List<Item> items = new List<Item>();

            for (int i = 0; i < 30; i++)
            {
                items.Add(new Item() { id = i.ToString(), number = i * 10 });
            }

            await requestHandler.DeleteItemsInBatchAsync(items);

            Console.WriteLine("Deleted More Than 25 Items In Batch");
        }

        private async Task AddMoreThan25InBatch()
        {
            TableHandler<Item> requestHandler = new TableHandler<Item>(TableName, new ItemConverterItem());

            List<Item> items = new List<Item>();

            for(int i = 0; i < 30; i++)
            {
                items.Add(new Item() {id = i.ToString(), number = i*10 });
            }

            await requestHandler.AddItemsInBatchAsync(items);

            Console.WriteLine("Added More Than 25 Items In Batch");
        }

        private async Task DoesTableExistAsync()
        {
            TableHandler<Item> requestHandler = new TableHandler<Item>(TableName, new ItemConverterItem());
            bool isExisting = await requestHandler.DoesTableExist();
            Console.WriteLine("Does the Table Exist: {0}", isExisting);
        }

        private async Task RemakeTableAsync()
        {
            TableHandler<Item> requestHandler = new TableHandler<Item>(TableName, new ItemConverterItem());

            await requestHandler.DeleteTableAsync();
            await requestHandler.WaitTillTableExists(false);
            Console.WriteLine("Table Doesn't Exist Anymore");

            await requestHandler.CreateTableAsync();
            await requestHandler.WaitTillTableIsStatus(TableStatus.ACTIVE);
            Console.WriteLine("Table Is Active");

            Item item = new Item() { id = "idfoo", number = 430 };
            await requestHandler.AddItemAsync(item);
            Console.WriteLine("Items Added");
        }

        private async Task GetCountAsync()
        {
            TableHandler<Item> requestHandler = new TableHandler<Item>(TableName, new ItemConverterItem());

            long count = await requestHandler.GetCountAsync();

            Console.WriteLine("Count: {0}", count);
            Console.WriteLine("All Items Retrieved");
        }

        private async Task DeleteItemsInBatchAsync()
        {
            TableHandler<Item> requestHandler = new TableHandler<Item>(TableName, new ItemConverterItem());

            List<Item> items = new List<Item>()
            {
                new Item() { id = "1", number = 11 },
                new Item() { id = "2", number = 22 },
                new Item() { id = "3", number = 33 }
            };

            await requestHandler.DeleteItemsInBatchAsync(items);

            Console.WriteLine("Added Items In Batch");
        }

        private async Task AddItemsInBatchAsync()
        {
            TableHandler<Item> requestHandler = new TableHandler<Item>(TableName, new ItemConverterItem());

            List<Item> items = new List<Item>()
            {
                new Item() { id = "1", number = 11 },
                new Item() { id = "2", number = 22 },
                new Item() { id = "3", number = 33 }
            };

            await requestHandler.AddItemsInBatchAsync(items);
            Console.WriteLine("Added Items In Batch");
        }

        private async Task DeleteItemAsync()
        {
            TableHandler<Item> requestHandler = new TableHandler<Item>(TableName, new ItemConverterItem());

            List<Item> items = new List<Item>()
            {
                new Item() { id = "1", number = 11 },
                new Item() { id = "2", number = 22 },
                new Item() { id = "3", number = 33 }
            };

            await requestHandler.DeleteItemAsync(items[0]);
            await requestHandler.DeleteItemAsync(items[1]);
            await requestHandler.DeleteItemAsync(items[2]);

            Console.WriteLine("Deleted Item");
        }

        private async Task AddItemAsync()
        {
            TableHandler<Item> requestHandler = new TableHandler<Item>(TableName, new ItemConverterItem());

            List<Item> items = new List<Item>()
            {
                new Item() { id = "1", number = 11 },
                new Item() { id = "2", number = 22 },
                new Item() { id = "3", number = 33 }
            };

            await requestHandler.AddItemAsync(items[0]);
            await requestHandler.AddItemAsync(items[1]);
            await requestHandler.AddItemAsync(items[2]);

            Console.WriteLine("Added Item");
        }

        public async Task CreateTableAsync()
        {
            TableHandler<Item> requestHandler = new TableHandler<Item>(TableName, new ItemConverterItem());
            await requestHandler.CreateTableAsync();
            Console.WriteLine("Table Created");
        }

        public async Task DeleteTableAsync()
        {
            TableHandler<Item> requestHandler = new TableHandler<Item>(TableName, new ItemConverterItem());
            await requestHandler.DeleteTableAsync();
            Console.WriteLine("Table Deleted");
        }




    }
}
