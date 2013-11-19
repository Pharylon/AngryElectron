using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class ChemicalEquation : IEnumerable<IChemical>
    {
        public EquationSide Reactants = new EquationSide();
        public EquationSide Products = new EquationSide();

        public ChemicalEquation()
        {
        }

        public ChemicalEquation(string inputString)
        {
            ChemicalEquation myEquation = Parser.Parse(inputString);
            Reactants = myEquation.Reactants;
            Products = myEquation.Products;
        }
        
        public int MoleculeCount { get { return Reactants.Count + Products.Count; } }
        public bool IsBalanced { get { return (Math.Round(Reactants.Mass, 4) == Math.Round(Products.Mass, 4)); } }
        public Element[] Elements { get { return Reactants.Elements.Union(Products.Elements).ToArray(); } }

        public void Add(IChemical chemical, Side side)
        {
            if (side == Side.LeftSide)
                Reactants.Add(chemical);
            if (side == Side.RightSide)
                Products.Add(chemical);
        }

        public IEnumerator<IChemical> GetEnumerator()
        {
            var unionList = Products.Union(Reactants).ToList();
            foreach (var chemical in unionList)
            {
                if (chemical == null)
                    break;
                else
                    yield return chemical;
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public string ToStringWithoutCoefficients()
        {
            return Reactants.ToStringWithoutCoefficients() + " -> " + Products.ToStringWithoutCoefficients();
        }

        public string ToHtml()
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