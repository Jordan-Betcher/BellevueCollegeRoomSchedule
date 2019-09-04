using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambdaPrintOnceADay
{
    public class Function
    {
        public string PrintOnceADay()
        {
            Console.WriteLine("Printed Once A Day");

            return "{}";
        }
    }
}
