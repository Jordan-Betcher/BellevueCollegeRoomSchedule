using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AWSLambdaBCRoomRestfulAPI.APIHandler;
using AWSLambdaBCRoomRestfulAPI.CurrentQuarter;
using AWSLambdaDynamoDB;
using AWSLambdaDynamoDB.DynamoDB;

namespace AWSLambdaBCRoomRestfulAPI
{
    public class BCAPIHandler
    {
        static int index = 0;

        public static async Task StoreAllClasstimesAsync(TableHandler<Classtime> tableHandlerClasstime)
        {
            Quarter quarter = BuilderQuarter.Build();
            String quarterCode = quarter.quarter;
            Console.WriteLine("Quarter Code: {0}", quarterCode);

            List<Subject> subjects = BuilderSubjects.Build(quarterCode);

            foreach(Subject subject in subjects)
            {
                Console.WriteLine("Subject: {0}", subject.name);

                String subjectAcronym = subject.subject;
                List<Section> classes = BuilderClasses.Build(subjectAcronym, quarterCode);
                Thread.Sleep(100);

                List<Classtime> classtimes = new List<Classtime>();

                foreach(Section section in classes)
                {
                    Console.WriteLine("Section: {0}, {1}", section.title, section.subject);
                    foreach(Class @class in section.sections)
                    {
                        if (@class.schedule.Contains("Online"))
                        {
                            continue;
                        }
                        else if(@class.schedule.Contains("ARRANGED"))
                        {
                            continue;
                        }
                        else
                        {
                            String room = @class.room;
                            Schedule schedule = new Schedule(@class.schedule);

                            if(room.Length < 4)
                            {
                                continue;
                            }
                            
                            String building = room.Substring(0, 1);
                            String roomNumber = room.Substring(1);
                            String classCode = section.subject + section.courseNumber;
                            int startTime = Int32.Parse(schedule.startTime);
                            int endTime = Int32.Parse(schedule.endTime);


                            foreach (String day in schedule.days)
                            {
                                classtimes.Add(new Classtime()
                                {
                                    id = index + "",
                                    building = building,
                                    roomNumber = roomNumber,
                                    classCode = classCode,
                                    day = day,
                                    startTime = startTime,
                                    endTime = endTime
                                });

                                index += 1;
                            }

                            Console.WriteLine("\t{0},\t{1},\t{2}", classCode, room, @class.schedule);
                            
                        }
                    }
                }

                await tableHandlerClasstime.AddItemsInBatchAsync(classtimes);
            }

        }
    }
}
