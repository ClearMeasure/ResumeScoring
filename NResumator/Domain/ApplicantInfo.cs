using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NResumator.Domain
{
    public class ApplicantInfo
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string prospect_phone { get; set; }
        public string apply_date { get; set; }
        public string job_id { get; set; }
        public string job_title { get; set; }
    }
}
