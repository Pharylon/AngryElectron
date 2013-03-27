using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class ElementGroup : List<IParsableSymbols>, IParsableSymbols
    {
        //ElementGroup is a grouping of Elements. It contains anything that implements IParsableSymbols - including other
        //ElementGroups. This is necessary as ElementGroup is meant to represent both a molecule and a complex. 
        public string Type { get; private set; }

        public ElementGroup(string type)
        {
            this.Type = type;
        }

        public IEnumerable<string> ParsableSymbols
        {
            get
            {
                List<string> symbols = new List<string>();
                foreach (IParsableSymbols unit in this)
                    foreach (string symbol in unit.ParsableSymbols)
                        symbols.Add(symbol);
                if (Type == "complex")
                {
                    symbols.Insert(0, "(");
                    symbols.Add(")");
                }
                return symbols;
            }
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
