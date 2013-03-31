using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public delegate void EquationDelegate(IParsableSymbols symbol, Side side);

    public class ChemicalEquation : IEquation, IParsableSymbols
    {
        private ElementGroup reactants = new ElementGroup(GroupType.Reactants);
        private ElementGroup products = new ElementGroup(GroupType.Products);

        public IEnumerable<IParsableSymbols> Reactants { get { return reactants; } }
        public IEnumerable<IParsableSymbols> Products { get { return products; } }

        public ChemicalEquation(ref EquationDelegate eqCallback)
        {
            eqCallback = AddToEquation;
        }

        private void AddToEquation(IParsableSymbols chemical, Side side)
        {
            if (side == Side.Reactants)
                reactants.Add(chemical);
            if (side == Side.Products)
                products.Add(chemical);
        }
        
        public IEnumerable<string> ParsableSymbols
        {
            get
            {
                List<string> symbols = new List<string>();
                foreach (IParsableSymbols reactant in Reactants)
                {
                    foreach (string symbol in reactant.ParsableSymbols)
                        symbols.Add(symbol);
                    symbols.Add("+");
                }
                symbols.RemoveAt(symbols.Count - 1);
                symbols.Add("->");
                foreach (IParsableSymbols product in Products)
                {
                    foreach (string symbol in product.ParsableSymbols)
                        symbols.Add(symbol);
                    symbols.Add("+");
                }
                symbols.RemoveAt(symbols.Count - 1);

                return symbols;
            }
        }
    }
}
