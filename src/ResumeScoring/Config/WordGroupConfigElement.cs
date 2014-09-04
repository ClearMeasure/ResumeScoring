using System;
using System.Configuration;

namespace ResumeScoring.Config
{
    public class WordGroupConfigElement : ConfigurationElement
    {
        public WordGroupConfigElement(String name, String words)
        {
            this.Name = name;
            this.WordGroup = words;
        }

        public WordGroupConfigElement()
        {

            this.Name = "Testing";
            this.WordGroup = "Foo Bar";
            this.Weight = 0;
            this.Type = "HIT";
        }

        [ConfigurationProperty("name", DefaultValue = "Default Name",
            IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("caseSensitive", DefaultValue = "false",
            IsRequired = false, IsKey = true)]
        [RegexStringValidator(@"true|TRUE|1|false|FALSE|0")]
        public string CaseSensitive
        {
            get { return (string)this["caseSensitive"]; }
            set { this["caseSensitive"] = value; }
        }

        [ConfigurationProperty("wordGroup", DefaultValue = "Default Words",
            IsRequired = true)]
        public string WordGroup
        {
            get { return (string)this["wordGroup"]; }
            set { this["wordGroup"] = value; }
        }

        [ConfigurationProperty("type", DefaultValue = "HIT",
            IsRequired = true)]
        [RegexStringValidator(@"HIT|MISS")]
        public string Type
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("weight", DefaultValue = (int)0, IsRequired = false)]
        [IntegerValidator(MinValue = -100, MaxValue = 100, ExcludeRange = false)]
        public int Weight
        {
            get { return (int)this["weight"]; }
            set { this["weight"] = value; }
        }
    }
}
