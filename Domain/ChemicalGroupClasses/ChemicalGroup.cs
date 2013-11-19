using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public abstract class ChemicalGroup : IChemical, IEnumerator<IChemical>, IEnumerable<IChemical>, IComparable<IChemical>
    {
        protected List<IChemical> contents = new List<IChemical>();

        public virtual void Add(IChemical chemical)
        {
            contents.Add(chemical);
            //contents.Sort(new ChemicalComparer());
        }

        public Element[] Elements
        {
            get
            {
                List<Element> elementList = new List<Element>();
                foreach (IChemical chemical in contents)
                {
                    elementList = elementList.Union(chemical.Elements).ToList();
                }
                return elementList.ToArray();
            }
        }

        public int GetDeepElementCount(Element elementToCheck)
        {
            int count = 0;
            foreach (IChemical chemical in contents)
            {
                if (chemical == elementToCheck)
                    count++;
                else if (chemical is ChemicalGroup)
                {
                    ChemicalGroup chemicalGroup = (ChemicalGroup)chemical;
                    count += chemicalGroup.GetDeepElementCount(elementToCheck);
                }
            }
            return count;
        }

        public int GetShallowChemicalCount(IChemical chemicalToCheck)
        {
            int count = 0;
            foreach (IChemical chemical in contents)
                if (chemicalToCheck == chemical)
                    count++;
            return count;
        }

        public virtual double Mass
        {
            get
            {
                double mass = 0;
                foreach (IChemical chemical in contents)
                    mass += chemical.Mass;
                return mass;
            }
        }

        public abstract string ToHTML();

        int position = -1;

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
            foreach (var chemical in contents)
            {
                if (chemical == null)
                    break;
                else
                    yield return chemical;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
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

        public int CompareTo(IChemical other)
        {
            var myTable = TableOfElements.Instance;
            if (other == this)
                return 0;
            else if (other.ToString() == "C")
                return 1;
            else if (other.ToString() == "H")
                return 1;
            else if (other is Complex)
                return -1;
            else
                return string.Compare(this.ToString(), other.ToString());
        }
    }
}