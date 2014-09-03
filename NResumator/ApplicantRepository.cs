using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using NResumator.Domain;

namespace NResumator
{
    public class ApplicantRepository
    {
        public static Applicant GetApplicant(string applicantId, string apiKey)
        {
            Applicant result =
                GetApiData<Applicant>(String.Format("https://api.resumatorapi.com/v1/applicants/{0}?apikey={1}",
                    applicantId, apiKey));

            return result;
        }

        public static List<ApplicantInfo> GetApplicants(string apiKey, int page = 1)
        {
            List<ApplicantInfo> result =
                GetApiData<List<ApplicantInfo>>(String.Format("https://api.resumatorapi.com/v1/applicants/status/new?apikey={0}&page={1}", apiKey, page));

            return result;
        }

        public static T GetApiData<T>(string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.ContentType = "application/json; charset=utf-8";

            string text;

            var response = request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }

            try
            {
                var result = JsonConvert.DeserializeObject<T>(text);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
