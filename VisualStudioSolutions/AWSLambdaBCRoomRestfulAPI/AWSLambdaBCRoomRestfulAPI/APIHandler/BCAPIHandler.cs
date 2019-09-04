using System;
using System.Collections.Generic;
using System.Threading;
using AWSLambdaBCRoomRestfulAPI.APIHandler;
using AWSLambdaBCRoomRestfulAPI.CurrentQuarter;

namespace AWSLambdaBCRoomRestfulAPI
{
    public class BCAPIHandler
    {
        internal static void PrintAll()
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
                            String classAcronym = section.subject + section.courseNumber;
                            Schedule schedule = new Schedule(@class.schedule);

                            Console.WriteLine("\t{0},\t{1},\t{2}", classAcronym, room, @class.schedule);
                            
                        }
                    }
                }
            }

        }
    }
}
