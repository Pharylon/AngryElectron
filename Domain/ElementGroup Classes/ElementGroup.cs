using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class ElementGroup : IChemical, IEnumerator<IChemical>, IEnumerable<IChemical>
    {
        public int Coefficient { get; set; }
        protected List<IChemical> contents = new List<IChemical>();
        int position = -1;

        protected Dictionary<string, Element> dictionaryOfElements;

        public ElementGroup(int coefficient = 1)
        {
            initializeDictionaryOfElements();
            Coefficient = 1;
        }

        public List<string> ListOfElements 
        { 
            get 
            {
                List<string> elementList = new List<string>();
                foreach (IChemical chemical in contents)
                {
                    foreach (string symbol in chemical.ParsableSymbols)
                        if (dictionaryOfElements.ContainsKey(symbol) && !elementList.Contains(symbol))
                            elementList.Add(symbol);
                }
                return elementList;
            } 
        }

        public List<string> ListOfContents
        {
            get
            {
                List<string> listOfContents = new List<string>();
                foreach (IChemical chemical in contents)
                {
                    if (!listOfContents.Contains(chemical.ToString()))
                        listOfContents.Add(chemical.ToString());
                }
                return listOfContents;
            }
        }

        private void initializeDictionaryOfElements()
        {
            TableOfElements tableOfElements = new TableOfElements();
            dictionaryOfElements = new Dictionary<string, Element>();
            foreach (Element element in tableOfElements)
                dictionaryOfElements.Add(element.Symbol, element);
        }

        public int GetDeepElementCount(string symbol)
        {
            int count = 0;
            foreach (string s in this.ParsableSymbols)
                if (symbol == s)
                    count++;
            return count;
        }

        public int GetShallowCount(string symbol)
        {
            int count = 0;
            foreach (IChemical unit in contents)
                if (symbol == unit.ToString())
                    count++;
            return count;
        }

        public virtual IEnumerable<string> ParsableSymbols
        {
            get
            {
                List<string> symbols = new List<string>();
                foreach (IChemical unit in contents)
                    foreach (string symbol in unit.ParsableSymbols)
                        symbols.Add(symbol);
                return symbols;
            }
        }

        public virtual string ToHTML()
        {
            throw new NotImplementedException("ToHTML() should only be called from an inherited class.");
        }

        public IChemical Current
        {
            get { return contents[position]; }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return contents[position]; }
        }

        public bool MoveNext()
        {
            position++;
            return (position < contents.Count);
        }

        public void Reset()
        {
            position = 0;
        }

        public IEnumerator<IChemical> GetEnumerator()
        {
            return (IEnumerator<IChemical>)this;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator<IChemical>)this;
        }

        public void Dispose()
        {
        }

        public void Add(IChemical chemical)
        {
            contents.Add(chemical);
        }

        public int Count
        {
            get
            {
                return contents.Count;
            }
        }

        public IChemical this[int i]
        {
            get { return contents[i]; }
            set { contents[i] = value; }
        }

    }
}
