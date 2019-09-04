using System;
using System.Collections.Generic;
using AWSLambdaBCRoomRestfulAPI.APIHandler;
using Newtonsoft.Json;

namespace AWSLambdaBCRoomRestfulAPI
{
    internal class BuilderSubjects
    {
        private const string API = "https://www2.bellevuecollege.edu/data/";

        internal static List<Subject> Build(string quarterCode)
        {
            String parameter = GetParemeterSubjects(quarterCode);
            String jsonSubjects = BuilderAPI.GetJsonFromAPI(API, parameter);
            DeserializeSubjects deserializedSubjects = JsonConvert.DeserializeObject<DeserializeSubjects>(jsonSubjects);
            List<Subject> subjects = deserializedSubjects.subjects;
            return subjects;
        }

        private static string GetParemeterSubjects(string quarterCode)
        {
            string parameterSubject = String.Format("api/v1/subjects/{0}", quarterCode);
            return parameterSubject;
        }
    }
}