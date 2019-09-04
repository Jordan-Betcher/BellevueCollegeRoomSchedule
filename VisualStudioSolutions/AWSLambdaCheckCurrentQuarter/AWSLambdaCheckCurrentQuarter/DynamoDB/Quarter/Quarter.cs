using System;
using System.Collections.Generic;
using System.Text;

namespace AWSLambdaCheckCurrentQuarter.DynamoDB
{
    public class Quarter
    {
        public string quarter { get; set; }
        public string title { get; set; }
    }

    internal class QuarterAsString
    {
        public static readonly string quarter = "quarter";
        public static readonly string title = "title";
    }

    class QuarterType
    {
        public static readonly string quarter = "S";
        public static readonly string title = "S";
    }
}
