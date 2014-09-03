using System;
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
            WebRequest request = WebRequest.Create(String.Format("https://api.resumatorapi.com/v1/applicants/{0}?apikey={1}", applicantId, apiKey));
            request.Method = WebRequestMethods.Http.Get;
            request.ContentType = "application/json; charset=utf-8";

            string text;

            var response = request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<Applicant>(text);
        }
    }
}
