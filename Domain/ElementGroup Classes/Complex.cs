using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    class Complex : ElementGroup
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            foreach (string symbol in this.ListOfContents)
            {
                int count = this.GetShallowCount(symbol);
                if (count == 1)
                    sb.Append(symbol);
                else
                    sb.Append(symbol + count.ToString());
            }
            sb.Append(")");
            return sb.ToString();
        }

        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            foreach (string symbol in this.ListOfContents)
            {
                int count = this.GetShallowCount(symbol);
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
            sb.Append(")");
            return sb.ToString();
        }

        public override IEnumerable<string> ParsableSymbols
        {
            get
            {
                List<string> symbols = new List<string>();
                symbols.Add("(");
                foreach (IChemical unit in contents)
                    foreach (string symbol in unit.ParsableSymbols)
                        symbols.Add(symbol);
                symbols.Add(")");
                return symbols;
            }
        }
    }
}
