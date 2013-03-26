using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class ChemicalEquation : IEquation, IParsableSymbols
    {
        //ChemicalEquation will hold two lists - the Reactants and Products. Each one will be a list that implements
        //IParsableSymbols. Therefore, list items may be an Element or an ElementGroup.

        public ElementGroup Reactants = new ElementGroup("reactants");
        public ElementGroup Products = new ElementGroup("products");

        public IEnumerable<string> ParsableSymbols
        {
            get
            {
                List<string> symbols = new List<string>();
                foreach (IParsableSymbols chemical in Reactants)
                {
                    foreach (string symbol in chemical.ParsableSymbols)
                        symbols.Add(symbol);
                    symbols.Add("+");
                }
                symbols.RemoveAt(symbols.Count - 1);
                symbols.Add("->");
                foreach (IParsableSymbols chemical in Products)
                {
                    foreach (string symbol in chemical.ParsableSymbols)
                        symbols.Add(symbol);
                    symbols.Add("+");
                }
                symbols.RemoveAt(symbols.Count - 1);

                return symbols;
            }
        }
    }
}
