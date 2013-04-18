using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    class Molecule : ChemicalGroup
    {
        public override void Add(IChemical chemical)
        {
            if (chemical is IMoleculeContent)
                base.Add(chemical);
            else
                throw new ArgumentException("Error: Molecules may only contain complexes and elements");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string symbol in this.ListOfContents)
            {
                int count = GetShallowCount(symbol);
                if (count == 1)
                    sb.Append(symbol);
                else
                    sb.Append(symbol + count.ToString());
            }
            return sb.ToString();
        }

        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string symbol in this.ListOfContents)
            {
                int count = GetShallowCount(symbol);
                if (count == 1)
                    sb.Append(symbol);
                else
                    sb.Append(symbol + count.ToString());
            }

            for (int i = sb.Length - 1; i >= 0; i--)
            {
                if (Char.IsDigit(sb[i]))
                {
                    sb.Insert(i + 1, "</sub>");
                    sb.Insert(i, "<sub>");
                }
            }
            return sb.ToString();
        }
    }
}
