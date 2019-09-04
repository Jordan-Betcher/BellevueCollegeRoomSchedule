using System;
using System.Collections.Generic;
using System.Text;

namespace AWSLambdaBCRoomRestfulAPI.CurrentQuarter
{
    public class DeserializeQuarter
    {
        public Quarter quarter { get; set; }
    }

    public class Quarter
    {
        public string quarter { get; set; }
        public string title { get; set; }
    }
}
