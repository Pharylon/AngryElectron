using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    class ElementGroup : List<IParsableSymbols>, IParsableSymbols
    {
        //ElementGroup is a grouping of Elements. It contains anything that implements IParsableSymbols - including other
        //ElementGroups. This is necessary as ElementGroup is meant to represent both a molecule and a chemical group. So an ElementGroup representing
        //a molecule may contain both individual elements and other ElementGroups that represent chemical groupings.
        string type;

        public ElementGroup(string type)
        {
            this.type = type;
        }

        public IEnumerable<string> ParsableSymbols
        {
            get
            {
                List<string> symbols = new List<string>();
                foreach (IParsableSymbols unit in this)
                    symbols.Union(this.ParsableSymbols).ToList();
                return symbols;
            }
        }
    }
}
