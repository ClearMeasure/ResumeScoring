namespace NResumator.Domain
{
    public class Comment
    {
        public string id { get; set; }
        public string author_id { get; set; }
        public string author_name { get; set; }
        public string text { get; set; }
        public string date { get; set; }
        public string time { get; set; }
    }
}