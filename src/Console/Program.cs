using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using NResumator;
using NResumator.Domain;
using ResumeScoring;
using ResumeScoring.Config;

namespace ResumatorResumeScoring
{
    class Program
    {
        public static string ApiKey;
        public static string ResumeFolder;
        static void Main(string[] args)
        {
            ApiKey = ConfigurationManager.AppSettings["ApiKey"];
            ResumeFolder = ConfigurationManager.AppSettings["ResumeFolder"];

            //TestConfigReader();

            Start();
            //GetOneResume();
            GetApplicants();
            //TestScoring();
            Stop();

            Console.ReadLine();

        }

        private static void TestScoring()
        {
            Console.WriteLine("Testing scoring...");
            string applicantId = "prospect_20140425142614_5UXIDTMYAUZT611H";
            Applicant result = ApplicantRepository.GetApplicant(applicantId, ApiKey);

            string resume = FileService.GetTextFromUrl(ResumeFolder, result.resume_link);
            ScoringService scoring = new ScoringService();
            var report = scoring.Score(resume);

            int score = 0;

            foreach (ReportItem reportItem in report.GetReportItems())
            {
                foreach (WordStat stat in reportItem.GetStats())
                {
                    Console.WriteLine("Word: " + stat.Word + " Count: " + stat.Count + " Weight: " + stat.Weight + " " + stat.Type);
                    
                    if(stat.Count > 0)
                        score += stat.Weight;
                }
            }
            Console.WriteLine("SCORE: " + score);
            Console.WriteLine("Done testing scoring.");
        }

        private static void TestConfigReader()
        {
            try
            {
                // Read and display the custom section.
                ResumeScoringSection resumeScoringSection =
                    ConfigurationManager.GetSection("ResumeScoring") as ResumeScoringSection;

                if (resumeScoringSection == null)
                    throw new ApplicationException("Failed to load ResumeScoringSection.");
                else
                {
                    Console.WriteLine("WordGroups defined in the configuration file:");
                    for (int i = 0; i < resumeScoringSection.WordGroups.Count; i++)
                    {
                        Console.WriteLine(" Type={0} Name={1} Words={2} Weight={3} CaseSensitive={4}",
                            resumeScoringSection.WordGroups[i].Type,
                            resumeScoringSection.WordGroups[i].Name,
                            resumeScoringSection.WordGroups[i].WordGroup,
                            resumeScoringSection.WordGroups[i].Weight,
                            resumeScoringSection.WordGroups[i].CaseSensitive);
                    }
                }

            }
            catch (ConfigurationErrorsException err)
            {
                Console.WriteLine("ReadCustomSection(string): {0}", err.ToString());
            }
        }

        public static void GetApplicants()
        {
            Console.WriteLine("Getting applicants.");

            //get a list of new applicants and rip through them
            List<ApplicantInfo> applicants = ApplicantRepository.GetApplicants(ApiKey);

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
                    applicant = ApplicantRepository.GetApplicant(a.id, ApiKey);
                    if (applicant != null && !string.IsNullOrEmpty(applicant.resume_link))
                    {

                        Console.WriteLine("");
                        Console.WriteLine(applicant.resume_link);
                        string resume = FileService.GetTextFromUrl(ResumeFolder, applicant.resume_link);
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
            Applicant result = ApplicantRepository.GetApplicant(applicantId, ApiKey);

            string resume = FileService.GetTextFromUrl(ResumeFolder, result.resume_link);
            Console.WriteLine(resume);
        }

        private static void Start()
        {
            if (!Directory.Exists(ResumeFolder))
            {
                Directory.CreateDirectory(ResumeFolder);
            }
        }

        private static void Stop()
        {
            Directory.Delete(ResumeFolder, true);
        }
    }
}
