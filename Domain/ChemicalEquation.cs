using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public delegate void EquationDelegate(IParsableSymbols symbol, Side side);

    public class ChemicalEquation : IEquation
    {
        private EquationSide reactants = new EquationSide();
        private EquationSide products = new EquationSide();

        public IParsableSymbols Reactants { get { return reactants; } }
        public IParsableSymbols Products { get { return products; } }

        public int MoleculeCount { get { return reactants.Count + products.Count; } }

        public List<string> ListOfElements
        {
            get
            {
                List<string> content = reactants.ListOfElements.Union(products.ListOfElements).ToList();
                return content;
            }
        }

        public void AddToEquation(IParsableSymbols chemical, Side side)
        {
            if (side == Side.LeftSide)
                reactants.Add(chemical);
            if (side == Side.RightSide)
                products.Add(chemical);
        }
        
        public IEnumerable<string> ParsableSymbols
        {
            get
            {
                List<string> symbols = new List<string>();
                foreach (IParsableSymbols reactant in reactants)
                {
                    foreach (string symbol in reactant.ParsableSymbols)
                        symbols.Add(symbol);
                    symbols.Add("+");
                }
                symbols.RemoveAt(symbols.Count - 1); //trim the last "+" the above loop added.
                symbols.Add("->");
                foreach (IParsableSymbols product in products)
                {
                    foreach (string symbol in product.ParsableSymbols)
                        symbols.Add(symbol);
                    symbols.Add("+");
                }
                symbols.RemoveAt(symbols.Count - 1); //trim the last "+" the above loop added.

                return symbols;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Reactants.ToString());
            sb.Append(" -> ");
            sb.Append(Products.ToString());
            return sb.ToString();
        }

        public string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Reactants.ToHTML());
            sb.Append(" -> ");
            sb.Append(Products.ToHTML());
            return sb.ToString();
        }

        public int GetDeepElementCount(string key)
        {
            return products.GetDeepElementCount(key) + reactants.GetDeepElementCount(key);
        }
    }
}
