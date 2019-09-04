using System.Collections.Generic;

namespace AWSLambdaBCRoomRestfulAPI
{
    /*
{
	"classes":[
		{
			"title":"Academic Skills Lab",
			"subject":"ABE",
			"courseNumber":"040",
			"description":"Students work independently with instructor support to develop skills in content areas of choice such as reading, writing, math, social studies, science, GED\u00ae and technology. Prerequisite: ABE Orientation and advising.",
			"note":"Prerequisite: ABE Orientation and advising.",
			"credits":2,
			"quarter":"B894",
			"isVariableCredits":true,
			"isCommonCourse":false,
			"sections":[
				{
					"id":"7631B894",
					"section":"A",
					"itemNumber":"7631",
					"instructor":"Dalrymple T",
					"beginDate":"04-08-2019",
					"endDate":"06-19-2019",
					"room":"R109",
					"schedule":"MW 5:00pm-5:50pm"
					},
    //*/
    public class DeserializeClasses
    {
        public List<Section> classes { get; set; }
    }

    public class Section
    {
        public string title { get; set; }
        public string subject { get; set; }
        public string courseNumber { get; set; }
		public string description { get; set; }
		public string note { get; set; }
        public int credits { get; set; }
        public string quarter { get; set; }
		public bool isVariableCredits { get; set; }
        public bool isCommonCourse { get; set; }
        public List<Class> sections { get; set; }
    }

    public class Class
    {
        public string id { get; set; }
        public string section { get; set; }
        public string itemNumber { get; set; }
        public string instructor { get; set; }
        public string beginDate { get; set; }
        public string endDate { get; set; }
        public string room { get; set; }
        public string schedule { get; set; }
    }
}