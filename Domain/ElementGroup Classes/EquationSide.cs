using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class EquationSide : ElementGroup
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < contents.Count; i++)
            {
                sb.Append(contents[i].ToString());
                if (i != contents.Count - 1)
                    sb.Append("+");
            }
            return sb.ToString();
        }

        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < contents.Count; i++)
            {
                sb.Append(contents[i].ToHTML());
                if (i != contents.Count - 1)
                    sb.Append("+");
            }
            return sb.ToString();
        }
    }
}
