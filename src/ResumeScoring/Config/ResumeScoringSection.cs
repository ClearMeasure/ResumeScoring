using System.Configuration;

namespace ResumeScoring.Config
{
    public class ResumeScoringSection : ConfigurationSection
    {

        [ConfigurationProperty("WordGroups", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(WordGroupsCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public WordGroupsCollection WordGroups
        {
            get
            {
                WordGroupsCollection wordGroupsCollection =
                    (WordGroupsCollection)base["WordGroups"];
                return wordGroupsCollection;
            }
        }

    }
}
