using System;
using System.Collections.Generic;

namespace AWSLambdaBCRoomRestfulAPI
{
    class Schedule
    {
        public String startTime { get; }
        public String endTime { get; }
        public List<String> days { get; }
        public bool exists { get; }

        //schedule is something like "TTh 6:00pm-9:10pm" or "ARRANGED 6:50pm-6:50pm" or "Online"
        public Schedule(string schedule)
        {
            if (schedule.Contains("Online") || schedule.Contains("ARRANGED"))
            {
                exists = false;
                return;
            }
            else
            {
                String[] tokens = schedule.Split(' ');
                String dayCode = tokens[0];
                String time = tokens[1];

                String[] timeTokens = time.Split('-');
                String startTimeString = timeTokens[0];
                String endTimeString = timeTokens[1];

                days = getDays(dayCode);
                startTime = getTimeFromString(startTimeString);
                endTime = getTimeFromString(endTimeString);
                exists = true;
            }
        }

        //startTimeString is somthing like "6:50pm"
        private static string getTimeFromString(string startTimeString)
        {
            //remove pm / am
            String t = startTimeString.Substring(0, startTimeString.Length - 2); ;

            String[] tt = t.Split(':');
            int hours = Int32.Parse(tt[0]);
            String minutes = tt[1];

            if (hours == 12 && startTimeString.Contains("pm"))
            {
                hours = 12;
            }
            else if (hours == 12 && startTimeString.Contains("am"))
            {
                hours = 0;
            }
            else if (startTimeString.Contains("pm"))
            {
                hours += 12;
            }


            String startTime = "";
            if (hours < 10)
            {
                startTime += "0";
            }

            startTime += hours;
            startTime += minutes;

            return startTime;
        }

        private static List<string> getDays(string dayCode)
        {
            if (dayCode.Contains("DAILY"))
            {
                return new List<string>()
                {
                    "Monday",
                    "Tuesday",
                    "Wednesday",
                    "Thursday",
                    "Friday"
                };
            }

            List<String> days = new List<String>();

            if (dayCode.Contains("T"))
            {
                if (dayCode.Contains("TTh"))
                {
                    days.Add("Tuesday");
                    days.Add("Thursday");
                }
                else if (dayCode.Contains("Th"))
                {
                    days.Add("Thursday");
                }
                else if (dayCode.Contains("T"))
                {
                    days.Add("Tuesday");
                }
            }

            if (dayCode.Contains("M"))
            {
                days.Add("Monday");
            }

            if (dayCode.Contains("W"))
            {
                days.Add("Wednesday");
            }

            if (dayCode.Contains("F"))
            {
                days.Add("Friday");
            }

            return days;
        }
    }
}