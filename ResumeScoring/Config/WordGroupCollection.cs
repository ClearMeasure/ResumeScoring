using System;
using System.Configuration;

namespace ResumeScoring.Config
{
    public class WordGroupsCollection : ConfigurationElementCollection
    {
        public WordGroupsCollection()
        {
            WordGroupConfigElement wordGroup = (WordGroupConfigElement)CreateNewElement();
            Add(wordGroup);
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new WordGroupConfigElement();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((WordGroupConfigElement)element).Name;
        }

        public WordGroupConfigElement this[int index]
        {
            get { return (WordGroupConfigElement)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        public new WordGroupConfigElement this[string Name]
        {
            get { return (WordGroupConfigElement)BaseGet(Name); }
        }

        public int IndexOf(WordGroupConfigElement wordGroup)
        {
            return BaseIndexOf(wordGroup);
        }

        public void Add(WordGroupConfigElement wordGroup)
        {
            BaseAdd(wordGroup);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(WordGroupConfigElement wordGroup)
        {
            if (BaseIndexOf(wordGroup) >= 0)
                BaseRemove(wordGroup.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}
