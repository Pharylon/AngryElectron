using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class ElementGroup : List<IChemical>, IChemical
    {
        protected Dictionary<string, Element> dictionaryOfElements;

        public ElementGroup(int coefficient = 1)
        {
            initializeDictionaryOfElements();
        }

        public List<string> ListOfElements 
        { 
            get 
            {
                List<string> elementList = new List<string>();
                foreach (IChemical unit in this)
                {
                    foreach (string symbol in unit.ParsableSymbols)
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
                List<string> contents = new List<string>();
                foreach (IChemical unit in this)
                {
                    if (!contents.Contains(unit.ToString()))
                        contents.Add(unit.ToString());
                }
                return contents;
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
            foreach (IChemical unit in this)
                if (symbol == unit.ToString())
                    count++;
            return count;
        }

        public virtual IEnumerable<string> ParsableSymbols
        {
            get
            {
                List<string> symbols = new List<string>();
                foreach (IChemical unit in this)
                    foreach (string symbol in unit.ParsableSymbols)
                        symbols.Add(symbol);
                return symbols;
            }
        }

        public virtual string ToHTML()
        {
            throw new NotImplementedException("ToHTML() should only be called from an inherited class.");
        }
    }
}
