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

        public ChemicalEquation(string inputString)
        {
            ChemicalEquation myEquation = Parser.Parse(inputString);
            Reactants = myEquation.Reactants;
            Products = myEquation.Products;
        }

        public ChemicalEquation()
        {
        }

        public int MoleculeCount { get { return Reactants.Count + Products.Count; } }
        public bool IsBalanced { get { return (Math.Round(Reactants.Mass, 4) == Math.Round(Products.Mass, 4)); } }
        public List<Element> ListOfElements { get { return Reactants.ListOfElements.Union(Products.ListOfElements).ToList(); } }

        public void Add(IChemical chemical, Side side)
        {
            if (side == Side.LeftSide)
                Reactants.Add(chemical);
            if (side == Side.RightSide)
                Products.Add(chemical);
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

        public int GetDeepElementCount(Element element)
        {
            return Products.GetDeepElementCount(element) + Reactants.GetDeepElementCount(element);
        }
    }
}