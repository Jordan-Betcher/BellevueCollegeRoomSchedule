using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AWSLambdaBCRoomRestfulAPI
{
    internal class BuilderClasses
    {
        private const string API = "https://www2.bellevuecollege.edu/data/";

        internal static List<Section> Build(string subjectAcronym, string quarterCode)
        {
            String parameter = GetParameterClasses(subjectAcronym, quarterCode);
            String jsonCurrentQuarter = BuilderAPI.GetJsonFromAPI(API, parameter);
            DeserializeClasses deserializeClasses = JsonConvert.DeserializeObject<DeserializeClasses>(jsonCurrentQuarter);

            if(deserializeClasses == null)
            {
                return new List<Section>();
            }
            else
            {
                List<Section> classes = deserializeClasses.classes;
                return classes;
            }
        }

        private static string GetParameterClasses(string subjectAcronym, string jsonCodeCurrentQuarter)
        {
            string parameterSubject = String.Format("api/v1/classes/{0}/{1}", jsonCodeCurrentQuarter, subjectAcronym);
            return parameterSubject;
        }
    }
}