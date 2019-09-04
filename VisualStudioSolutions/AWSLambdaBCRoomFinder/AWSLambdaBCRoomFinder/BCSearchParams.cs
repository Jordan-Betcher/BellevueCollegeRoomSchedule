using System;
using Newtonsoft.Json.Linq;

namespace AWSLambdaBCRoomFinder
{
    public class BCSearchParams
    {
        public string buildingLetter;
        public string roomNumber;
        public string day;

        //buildingLetter and roomNumber are required,
        //while day is defaulted to the current day
        public BCSearchParams(string buildingLetter, string roomNumber, string day)
        {
            this.buildingLetter = buildingLetter;
            this.roomNumber = roomNumber;
            SetDay(day);
        }

        private void SetDay(JToken day)
        {
            String stringDay = (String)day;
            if (stringDay != "")
            {
                this.day = stringDay;
            }
            else
            {
                this.day = DateTime.UtcNow.DayOfWeek.ToString();
            }
        }
    }
}