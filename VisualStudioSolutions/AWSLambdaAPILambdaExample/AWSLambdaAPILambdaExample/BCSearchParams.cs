using System;
using Newtonsoft.Json.Linq;

namespace AWSLambdaAPILambdaExample
{
    internal class BCSearchParams
    {
        public string buildingLetter;
        public string roomNumber;
        public string day;
        public AfterBefore afterBefore;
        public int time;

        public BCSearchParams(string buildingLetter)
        {
            this.buildingLetter = buildingLetter;
            this.roomNumber = "";
            this.day = DateTime.UtcNow.DayOfWeek.ToString();
            this.afterBefore = AfterBefore.AFTER;
            this.time = (int)DateTime.UtcNow.TimeOfDay.TotalMinutes;
        }

        internal void SetRoomNumber(JToken roomNumber)
        {
            String stringroomNumber = (String)roomNumber;
            if (stringroomNumber != "")
            {
                this.roomNumber = stringroomNumber;
            }
        }

        internal void SetDay(JToken day)
        {
            String stringDay = (String)day;
            if (stringDay != "")
            {
                this.day = stringDay;
            }
        }

        internal void SetAfterBefore(JToken afterBefore)
        {
            String stringAfterBefore = (String)afterBefore;
            stringAfterBefore = stringAfterBefore.ToLower();

            if (stringAfterBefore.Equals("before"))
            {
                this.afterBefore = AfterBefore.BEFORE;
            }
            else if (stringAfterBefore.Equals("after"))
            {
                this.afterBefore = AfterBefore.AFTER;
            }
            else
            {
                Console.WriteLine("Error: {0}, does not match \"before\" or \"after\"", stringAfterBefore);
            }

        }

        internal void SetTime(JToken time)
        {
            String stringTime = (String)time;
            if(stringTime != "")
            {
                this.time = Int32.Parse((String)time);
            }
        }
    }

    enum AfterBefore
    {
        AFTER, BEFORE
    }
}