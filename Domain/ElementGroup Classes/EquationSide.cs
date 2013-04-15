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
            for (int i = 0; i < this.Count; i++)
            {
                sb.Append(this[i].ToString());
                if (i != this.Count - 1)
                    sb.Append("+");
            }
            return sb.ToString();
        }

        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Count; i++)
            {
                sb.Append(this[i].ToHTML());
                if (i != this.Count - 1)
                    sb.Append("+");
            }
            return sb.ToString();
        }
    }
}
