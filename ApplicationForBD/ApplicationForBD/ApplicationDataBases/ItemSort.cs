using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForBD.ApplicationDataBases
{
    internal class ItemSortCollection : IEnumerable
    {
        private Dictionary<string, ItemSort> listItemSorts = new Dictionary<string, ItemSort>();

        
        public ItemSort this[string index]
        {
            get => (ItemSort)listItemSorts[index];
            set => listItemSorts[index] = value;
        }

        public int Count => listItemSorts.Count;

        public IEnumerator GetEnumerator()
            => listItemSorts.GetEnumerator();
        
    }
    internal class ItemSort
    {
        private bool containsDescInSentence;
        private string nameSortText;


        public string NameSortText
        {
            get => nameSortText;
            set => nameSortText = value;
        }
        public bool ContainsDescInSentence
        {
            get => containsDescInSentence;
            set => containsDescInSentence = value;
        }
        public ItemSort(bool containsDescInSentence, string nameText)
        {
            ContainsDescInSentence = containsDescInSentence;
            NameSortText = nameText;
        }
        public override string ToString()
        {
            return NameSortText;
        }
    }
}
