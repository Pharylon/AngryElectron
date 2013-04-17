using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class EquationSide : ChemicalGroup
    {
        public Dictionary<string, int> Coefficients = new Dictionary<string, int>();

        public override void Add(IChemical chemical)
        {
            Coefficients.Add(chemical.ToString(), 1);
            base.Add(chemical);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < contents.Count; i++)
            {
                if (Coefficients[contents[i].ToString()] > 1)
                    sb.Append(Coefficients[contents[i].ToString()].ToString());
                sb.Append(contents[i].ToString());
                if (i != contents.Count - 1)
                    sb.Append(" + ");
            }
            return sb.ToString();
        }

        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < contents.Count; i++)
            {
                if (Coefficients[contents[i].ToString()] > 1)
                    sb.Append(Coefficients[contents[i].ToString()].ToString());
                sb.Append(contents[i].ToHTML());
                if (i != contents.Count - 1)
                    sb.Append(" + ");
            }
            return sb.ToString();
        }
    }
}
