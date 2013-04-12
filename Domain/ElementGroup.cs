using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class ElementGroup : List<IParsableSymbols>, IParsableSymbols
    {
        private GroupType type;
        public GroupType Type { get { return type; } }
        public int Coefficient = 1;

        public ElementGroup(GroupType type, int coefficient = 1)
        {
            this.type = type;
        }

        public ElementGroup(GroupType type, IParsableSymbols element, int coefficient = 1)
        {
            this.type = type;
            this.Add(element);
        }

        public IEnumerable<string> ParsableSymbols
        {
            get
            {
                List<string> symbols = createListOfSymbols();
                if (Type == GroupType.Complex)
                {
                    symbols.Insert(0, "(");
                    symbols.Add(")");
                }
                return symbols;
            }
        }

        private List<string> createListOfSymbols()
        {
            List<string> symbols = new List<string>();
            foreach (IParsableSymbols unit in this)
                foreach (string symbol in unit.ParsableSymbols)
                    symbols.Add(symbol);
            return symbols;
        }

        

        private int stringCount(string symbol)
        {
            int count = 0;
            foreach (IParsableSymbols unit in this)
            {
                string unitToString = unit.ToString();
                if (unitToString == symbol)
                    count++;
            }
            return count;
        }

        public override string ToString()
        {
            List<string> symbolList = createListOfSymbols();
            StringBuilder sb = new StringBuilder();
            switch (this.type)
            {
                case GroupType.Molecule:
                    return generateStringForMolecule(symbolList, sb);
                case GroupType.Complex:
                    return createComplexStringSymbol(symbolList, sb);
                case GroupType.ElementWrapper:
                    return symbolList[0];
                case GroupType.Products:
                    return generateStringForEquationSide(symbolList, sb);
                case GroupType.Reactants:
                    return generateStringForEquationSide(symbolList, sb);
                default:
                    throw new InvalidOperationException("ElementGroup Was Not A Recognized Type");
            }
        }

        public string ToHTML()
        {
            List<string> symbolList = createListOfSymbols();
            StringBuilder sb = new StringBuilder();
            switch (this.type)
            {
                case GroupType.Molecule:
                    return generateStringForMolecule(symbolList, sb, "html");
                case GroupType.Complex:
                    return createComplexStringSymbol(symbolList, sb, "html");
                case GroupType.ElementWrapper:
                    return symbolList[0];
                case GroupType.Products:
                    return generateStringForEquationSide(symbolList, sb, "html");
                case GroupType.Reactants:
                    return generateStringForEquationSide(symbolList, sb, "html");
                default:
                    throw new InvalidOperationException("ElementGroup Was Not A Recognized Type");
            }
        }

        private string generateStringForEquationSide(List<string> symbolList, StringBuilder sb, string format = "toString")
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (format == "html")
                    sb.Append(this[i].ToHTML());
                else
                sb.Append(this[i].ToString());
                if (i != this.Count - 1)
                    sb.Append("+");
            } 
            return sb.ToString();
        }

        private string generateStringForMolecule(List<string> symbolList, StringBuilder sb, string format = "toString")
        {
            if (Coefficient > 1)
                sb.Append(Coefficient);
            List<string> uniqueSymbols = generateUniqueString();
            foreach (string symbol in uniqueSymbols)
            {
                sb.Append(symbol);
                int symbolCount = this.stringCount(symbol);
                if (symbolCount > 1)
                {
                    if (format == "html")
                        sb.Append("<sub>");
                    sb.Append(symbolCount);
                    if (format == "html")
                        sb.Append("</sub>");
                }
            }
            return sb.ToString();
        }

        private string createComplexStringSymbol(List<string> symbolList, StringBuilder sb, string format = "toString")
        {
            symbolList = generateUniqueParsableSymbols();
            foreach (string symbol in symbolList)
            {
                sb.Append(symbol);
                int symbolCount = this.stringCount(symbol);
                if (symbolCount > 1)
                {
                    if (format == "html")
                        sb.Append("<sub>");
                    sb.Append(symbolCount);
                    if (format == "html")
                        sb.Append("</sub>");
                }
            }
            return sb.ToString();
        }

        private List<string> generateUniqueString()
        {
            List<string> uniqueSymbols = new List<string>();
            foreach (IParsableSymbols unit in this)
                if (!uniqueSymbols.Contains(unit.ToString()))
                    uniqueSymbols.Add(unit.ToString());
            return uniqueSymbols;
        }

        public List<string> generateUniqueParsableSymbols()
        {
            List<string> uniqueSymbols = new List<string>();
            foreach (string symbol in this.ParsableSymbols)
                if (!uniqueSymbols.Contains(symbol))
                    uniqueSymbols.Add(symbol);
            return uniqueSymbols;
        }
    }
}
