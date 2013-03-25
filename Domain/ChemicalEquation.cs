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
        //IParsableSymbols. Therefore, list items may be an element or an ElementGroup.

        public ElementGroup Reactants = new ElementGroup("reactants");
        public ElementGroup Products = new ElementGroup("products");

        public IEnumerable<string> ParsableSymbols
        {
            get
            {
                List<string> symbols = new List<string>();
                foreach (IParsableSymbols molecule in Reactants)
                {
                    foreach (string symbol in molecule.ParsableSymbols)
                        symbols.Add(symbol);
                    symbols.Add("+");
                }
                symbols.RemoveAt(symbols.Count - 1);
                symbols.Add("->");
                foreach (IParsableSymbols molecule in Products)
                {
                    foreach (string symbol in molecule.ParsableSymbols)
                        symbols.Add(symbol);
                    symbols.Add("+");
                }
                symbols.RemoveAt(symbols.Count - 1);

                return symbols;
            }
        }
    }
}
