using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
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
            Start();

            GetOneResume();
            GetApplicants();

            Console.ReadLine();
            Stop();
        }

        public static void GetApplicants()
        {
            Console.WriteLine("Getting applicants.");

            //get a list of new applicants and rip through them
            List<ApplicantInfo> applicants = ApplicantRepository.GetApplicants(API_KEY);

            Console.WriteLine(String.Format("Got {0} applicants.", applicants.Count));
            Console.WriteLine();

            foreach (ApplicantInfo a in applicants)
            {
                Console.WriteLine("------------------------------------------------------------------------");
                Console.WriteLine("Applicant id: " + a.id);
                Console.WriteLine("Name: " + a.first_name + " " + a.last_name);
                Console.WriteLine();

                Applicant applicant = null;

                try
                {
                    applicant = ApplicantRepository.GetApplicant(a.id, API_KEY);
                    if (applicant != null && !string.IsNullOrEmpty(applicant.resume_link))
                    {

                        Console.WriteLine("");
                        Console.WriteLine(applicant.resume_link);
                        string resume = FileService.GetTextFromUrl(RESUME_FOLDER, applicant.resume_link);
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("No resume");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static void GetOneResume()
        {
            string applicantId = "prospect_20140425142614_5UXIDTMYAUZT611H";
            Applicant result = ApplicantRepository.GetApplicant(applicantId, API_KEY);

            string resume = FileService.GetTextFromUrl(RESUME_FOLDER, result.resume_link);
            Console.WriteLine(resume);
        }

        private static void Start()
        {
            if (!Directory.Exists(RESUME_FOLDER))
            {
                Directory.CreateDirectory(RESUME_FOLDER);
            }
        }

        private static void Stop()
        {
            Directory.Delete(RESUME_FOLDER, true);
        }
    }
}
