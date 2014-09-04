using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using ResumeScoring.Config;

namespace ResumeScoring
{
    public class ScoringService
    {
        public ScoringReport Score(string resume)
        {
            ScoringReport report = new ScoringReport();

            ResumeScoringSection resumeScoringSection =
                    ConfigurationManager.GetSection("ResumeScoring") as ResumeScoringSection;

            if (resumeScoringSection == null)
                throw new ApplicationException("Failed to load ResumeScoringSection.");
            else
            {
                for (int i = 0; i < resumeScoringSection.WordGroups.Count; i++)
                {
                    WordGroupConfigElement wg = resumeScoringSection.WordGroups[i];

                    report.AddReportItem(Score(wg, resume));

                    Console.WriteLine(" Type={0} Name={1} Words={2} Weight={3} CaseSensitive={4}",
                        resumeScoringSection.WordGroups[i].Type,
                        resumeScoringSection.WordGroups[i].Name,
                        resumeScoringSection.WordGroups[i].WordGroup,
                        resumeScoringSection.WordGroups[i].Weight,
                        resumeScoringSection.WordGroups[i].CaseSensitive);
                }
            }

            return report;
        }

        private ReportItem Score(WordGroupConfigElement wge, string resume)
        {
            ReportItem item = new ReportItem();
            string[] words = wge.WordGroup.Split(',');
            int weight = wge.Weight;
            string type = wge.Type;
            bool sensitive = Convert.ToBoolean(wge.CaseSensitive);

            foreach (string word in words)
            {
                int cnt = 0;

                    if(sensitive)
                        cnt = Regex.Matches(resume, word).Count;    
                    else
                        cnt = Regex.Matches(resume, word, RegexOptions.IgnoreCase).Count;

                    item.AddWordStats(type, word, weight, cnt);
            }

            return item;
        }
    }

    public class ScoringReport
    {
        private List<ReportItem> _items { get; set; }

        public ScoringReport()
        {
            _items = new List<ReportItem>();
        }
        public void AddReportItem(ReportItem score)
        {
            _items.Add(score);
        }

        public List<ReportItem> GetReportItems()
        {
            return _items;
        }
    }

    public class ReportItem
    {
        private List<WordStat> _stats { get; set; }

        public ReportItem()
        {
            _stats = new List<WordStat>();
        }
        public void AddWordStats(string type, string word, int weight, int cnt)
        {
            WordStat stat = new WordStat()
            {
                Type = type, 
                Word = word, 
                Weight = weight, 
                Count = cnt
            };

            _stats.Add(stat);
        }

        public List<WordStat> GetStats()
        {
            return _stats;
        }
    }

    public class WordStat
    {
        public string Type { get; set; }
        public string Word { get; set; }
        public int Weight { get; set; }
        public int Count { get; set; }
    }
}
