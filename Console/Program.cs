using System;
using NResumator;
using NResumator.Domain;

namespace ResumatorResumeScoring
{
    class Program
    {
        public const string API_KEY = "5JT8NHLNQJuBQouMyZtgYDFf4bJsHw4v";
        public const string RESUME_FOLDER = @"C:\resumes";
        static void Main(string[] args)
        {
            string applicantId = "prospect_20140425142614_5UXIDTMYAUZT611H";
            Applicant result = ApplicantRepository.GetApplicant(applicantId, API_KEY);
            string resume = FileService.GetTextFromUrl(RESUME_FOLDER, result.resume_link);
            Console.WriteLine(resume);
            Console.ReadLine();
        }
    }
}
