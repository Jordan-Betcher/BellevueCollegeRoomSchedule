using System.Collections.Generic;

namespace AWSLambdaBCRoomRestfulAPI.APIHandler
{
    //{"subjects":[{"subject":"ABE","name":"Adult Basic Education"},
    public class DeserializeSubjects
    {
        public List<Subject> subjects { get; set; }
    }

    public class Subject
    {
        public string subject { get; set; }
        public string name { get; set; }
    }
}
