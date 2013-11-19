using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    class Complex : ChemicalGroup, IComparable<IChemical>
    {
        public override void Add(IChemical chemical)
        {
            if (chemical is Element || chemical is Complex)
                base.Add(chemical);
            else
                throw new ArgumentException("Complexes may only contain other complexes and elements");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            foreach (IChemical chemical in contents.Distinct())
            {
                int subScript = this.GetShallowChemicalCount(chemical);
                sb.Append(chemical.ToString());
                if (subScript > 1)
                    sb.Append(subScript.ToString());
            }
            sb.Append(")");
            return sb.ToString();
        }

        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            foreach (IChemical chemical in contents.Distinct())
            {
                int subScript = GetShallowChemicalCount(chemical);
                sb.Append(chemical.ToString());
                if (subScript > 1)
                    sb.Append("<sub>" + subScript.ToString() + "</sub>");
            }
            sb.Append(")");
            return sb.ToString();
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