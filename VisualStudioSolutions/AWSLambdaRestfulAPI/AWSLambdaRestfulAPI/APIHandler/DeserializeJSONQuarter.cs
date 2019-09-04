using System;
using System.Collections.Generic;
using System.Text;

namespace AWSLambdaRestfulAPI.CurrentQuarter
{
    public class DeserializeJSONQuarter
    {
        public Quarter quarter { get; set; }
    }

    public class Quarter
    {
        public string quarter { get; set; }
        public string title { get; set; }
    }
}
