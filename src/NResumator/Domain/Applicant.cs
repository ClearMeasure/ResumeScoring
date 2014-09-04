using System.Collections.Generic;

namespace NResumator.Domain
{
    public class Applicant
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string location { get; set; }
        public string phone { get; set; }
        public string linkedin_url { get; set; }
        public string eeo_gender { get; set; }
        public string eeo_race { get; set; }
        public string eeo_disability { get; set; }
        public string website { get; set; }
        public string desired_salary { get; set; }
        public string desired_start_date { get; set; }
        public string referrer { get; set; }
        public string languages { get; set; }
        public string wmyu { get; set; }
        public string has_driver_license { get; set; }
        public string willing_to_relocate { get; set; }
        public string citizenship_status { get; set; }
        public string education_level { get; set; }
        public string has_cdl { get; set; }
        public string over_18 { get; set; }
        public string can_work_weekends { get; set; }
        public string can_work_evenings { get; set; }
        public string can_work_overtime { get; set; }
        public string has_felony { get; set; }
        public string felony_explanation { get; set; }
        public string twitter_username { get; set; }
        public string college_gpa { get; set; }
        public string college { get; set; }
        public string references { get; set; }
        public string notes { get; set; }
        public string apply_date { get; set; }
        public string comments_count { get; set; }
        public string source { get; set; }
        public string recruiter_id { get; set; }
        public string eeoc_veteran { get; set; }
        public string eeoc_disability { get; set; }
        public string eeoc_disability_signature { get; set; }
        public string eeoc_disability_date { get; set; }
        //public List<object> jobs { get; set; }
        //public List<Comment> comments { get; set; }
        //public List<object> feedback { get; set; }
        //public List<object> rating { get; set; }
        public string resume_link { get; set; }
        //public List<object> activities { get; set; }
        //public List<object> messages { get; set; }
        //public List<object> questionnaire { get; set; }
        //public List<Evaluation> evaluation { get; set; }
        //public List<object> categories { get; set; }
    }
}