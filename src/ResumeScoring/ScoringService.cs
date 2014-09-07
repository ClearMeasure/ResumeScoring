using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using ResumeScoring.Config;

namespace ResumeScoring
{
    public class ScoringService
    {
        private string _resume;
        private ScoringReport _report;
        public ScoringService(string resume)
        {
            _resume = resume;
            _report = new ScoringReport();
        }

        public ScoringReport Score()
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

                    report.AddReportItem(ScoreWordGroup(wg, _resume));
                }
            }

            _report = report;
            return report;
        }

        private ReportItem ScoreWordGroup(WordGroupConfigElement wge, string resume)
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

                //TODO: make a word stat first
                //TODO: use wordstat . isimportant
                //TODO: remove comparison logic here so that we only do it in once place

                //filter noise
                if ((type == "HIT" && cnt > 0) || (type == "MISS" && cnt == 0))
                {
                    item.AddWordStats(type, word, weight, cnt);
                }
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

        public int GetScore()
        {
            int score = 0;

            foreach (ReportItem reportItem in GetReportItems())
            {
                foreach (WordStat stat in reportItem.GetStats())
                {
                    if (stat.IsImportant())
                    {
                        score += stat.Weight;
                    }
                }
            }

            return score;
        }

        public List<WordStat> GetItemsOfSignificance()
        {
            List<WordStat> stats =
                GetReportItems()
                    .SelectMany(
                        w =>
                            w.GetStats()
                                .Where(s => s.IsImportant())).ToList();

            return stats;
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

        public bool IsImportant()
        {
            if ((Type == "HIT" && Count > 0) || (Type == "MISS" && Count == 0))
                return true;

            return false;
        }
    }
}
