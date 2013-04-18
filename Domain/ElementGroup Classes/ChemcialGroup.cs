using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public abstract class ChemicalGroup : IChemical, IEnumerator<IChemical>, IEnumerable<IChemical>
    {
        protected List<IChemical> contents = new List<IChemical>();
        int position = -1;

        protected Dictionary<string, Element> dictionaryOfElements;

        public ChemicalGroup(int coefficient = 1)
        {
            initializeDictionaryOfElements();
        }

        public virtual void Add(IChemical chemical)
        {
            contents.Add(chemical);
        }

        //public List<string> ListOfElements 
        //{ 
        //    get 
        //    {
        //        List<string> elementList = new List<string>();
        //        foreach (IChemical chemical in contents)
        //        {
        //            foreach (string symbol in chemical.ParsableSymbols)
        //                if (dictionaryOfElements.ContainsKey(symbol) && !elementList.Contains(symbol))
        //                    elementList.Add(symbol);
        //        }
        //        return elementList;
        //    } 
        //}

        public List<Element> ListOfElements
        {
            get
            {
                List<Element> elementList = new List<Element>();
                foreach (IChemical chemical in contents)
                    elementList = elementList.Union(chemical.ListOfElements).ToList();
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

        public int GetDeepElementCount(Element elementToCheck)
        {
            int count = 0;
            foreach (IChemical chemical in contents)
            {
                if (chemical is Element && chemical.ListOfElements.Contains(elementToCheck))
                    count++;
                else if (chemical is ChemicalGroup)
                {
                    ChemicalGroup chemicalGroup = (ChemicalGroup)chemical;
                    count += chemicalGroup.GetDeepElementCount(elementToCheck);
                }
            }
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

        public abstract string ToHTML();

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



        public double Mass
        {
            get
            {
                double mass = 0;
                foreach (IChemical chemical in contents)
                    mass += chemical.Mass;
                return mass;
            }
        }
    }
}
