using System;
using System.Collections.Generic;
using System.Text;

namespace AWSLambdaDynamoDB.DynamoDB
{
    public class Classtime
    {
        public string id { get; set; }
        public string classCode { get; set; }
        public string building { get; set; }
        public string roomNumber { get; set; }
        public string day { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
    }

    public class ClasstimeAsString
    {
        public static readonly string id = "id";
        public static readonly string classCode = "classCode";
        public static readonly string building = "building";
        public static readonly string roomNumber = "roomCode";
        public static readonly string day = "day";
        public static readonly string startTime = "startTime";
        public static readonly string endTime = "endTime";
    }

    public class ClasstimeType
    {
        public static readonly string id = "S";
        public static readonly string classCode = "S";
        public static readonly string building = "S";
        public static readonly string roomNumber = "S";
        public static readonly string day = "S";
        public static readonly string startTime = "N";
        public static readonly string endTime = "N";
    }
}
