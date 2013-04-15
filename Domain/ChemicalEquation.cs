using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class ChemicalEquation
    {
        public EquationSide Reactants = new EquationSide();
        public EquationSide Products = new EquationSide();

        public int MoleculeCount { get { return Reactants.Count + Products.Count; } }

        public List<string> ListOfElements
        {
            get
            {
                List<string> content = Reactants.ListOfElements.Union(Products.ListOfElements).ToList();
                return content;
            }
        }

        public void AddToEquation(IChemical chemical, Side side)
        {
            if (side == Side.LeftSide)
                Reactants.Add(chemical);
            if (side == Side.RightSide)
                Products.Add(chemical);
        }
        
        public IEnumerable<string> ParsableSymbols
        {
            get
            {
                List<string> symbols = new List<string>();
                for (int i = 0; i < Reactants.Count; i++)
                {
                    foreach (string symbol in Reactants[i].ParsableSymbols)
                        symbols.Add(symbol);
                    if (i != Reactants.Count -1)
                        symbols.Add("+");
                }
                symbols.Add("->");
                for (int i = 0; i < Products.Count; i++)
                {
                    foreach (string symbol in Products[i].ParsableSymbols)
                        symbols.Add(symbol);
                    if (i != Products.Count - 1)
                        symbols.Add("+");
                }
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

        public int GetDeepElementCount(string symbol)
        {
            return Products.GetDeepElementCount(symbol) + Reactants.GetDeepElementCount(symbol);
        }


    }
}
