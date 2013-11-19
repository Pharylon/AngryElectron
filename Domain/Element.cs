using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class Element : IEquatable<Element>, IComparable<Element>, IComparable<IChemical>, IChemical
    {
        public string Symbol { get; set; }
        public int AtomicNumber { get; set; }
        public virtual double AtomicMass { get; set; }
        public string Name { get; set; }

        public bool Equals(Element other)
        {
            return (this.AtomicNumber == other.AtomicNumber);
        }

        public int CompareTo(Element other)
        {
            if (this.AtomicNumber > other.AtomicNumber)
                return 1;
            else if (this.AtomicNumber == other.AtomicNumber)
                return 0;
            else
                return -1;
        }

        public int CompareTo(IChemical other)
        {
            var myTable = TableOfElements.Instance;
            if (other == this)
                return 0;
            else if (other.ToString() == "C")
                return 1;
            else if (other.ToString() == "H")
            {
                if (this.Symbol == "C")
                    return -1;
                else
                    return 1;
            }
            else if (other is Complex)
                return -1;
            else
                return string.Compare(this.ToString(), other.ToString());
        }

        public override string ToString()
        {
            return Symbol;
        }

        public string ToHTML()
        {
            return Symbol;
        }

        public double Mass
        {
            get
            {
                return AtomicMass;
            }
        }

        #region IChemical Members


        public Element[] Elements
        {
            get { return new Element[] { this }; }
        }

        #endregion
    }
}
