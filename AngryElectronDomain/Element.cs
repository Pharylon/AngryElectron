using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectronDomain
{
    public class Element
    {
        public string Symbol { get; private set; }

        public Element(string symbol)
        {
            Symbol = symbol;
        }
    }
}
