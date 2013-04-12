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
        Dictionary<string, int> contentCount = new Dictionary<string, int>();
        Dictionary<string, int> elementCount = new Dictionary<string, int>();

        public List<string> ListOfElements 
        { 
            get 
            {
                return new List<string>(elementCount.Keys); 
            } 
        }

        public List<string> ListOfContents
        {
            get
            {
                return new List<string>(elementCount.Keys); 
            }
        }

        public ElementGroup(GroupType type, IParsableSymbols element, int coefficient = 1)
        {
            this.type = type;
            this.Add(element);
        }

        public ElementGroup(GroupType type, int coefficient = 1)
        {
            this.type = type;
        }

        public new void Add(IParsableSymbols itemToAdd)
        {
            base.Add(itemToAdd);

            foreach (string s in itemToAdd.ListOfElements)
            {
                if (!elementCount.ContainsKey(s))
                    elementCount.Add(s, itemToAdd.GetSubscriptCount(s));
                else
                    elementCount[s] += itemToAdd.GetSubscriptCount(s);
            }

            if (!contentCount.ContainsKey(itemToAdd.ToString()))
                contentCount.Add(itemToAdd.ToString(), 1);
            else
                contentCount[itemToAdd.ToString()]++;
        }

        public int GetSubscriptCount(string key)
        {
            if (elementCount.ContainsKey(key))
                return elementCount[key];
            else
                return 0;
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            switch (this.type)
            {
                case GroupType.Molecule:
                    if (Coefficient > 1)
                        sb.Append(Coefficient);
                    sb.Append(generateStringWithCoefficients());
                    return sb.ToString();
                case GroupType.Complex:
                    sb.Append("(");
                    sb.Append(generateStringWithCoefficients());
                    sb.Append(")");
                    return sb.ToString();
                case GroupType.ElementWrapper:
                    return this[0].ToString();
                case GroupType.Products:
                    return generateStringForEquationSide();
                case GroupType.Reactants:
                    return generateStringForEquationSide();
                default:
                    throw new InvalidOperationException("ElementGroup Was Not A Recognized Type");
            }
        }

        public string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            switch (this.type)
            {
                case GroupType.Molecule:
                    if (Coefficient > 1)
                        sb.Append(Coefficient);
                    sb.Append(generateHTMLWithCoefficients());
                    return sb.ToString();
                case GroupType.Complex:
                    sb.Append("(");
                    sb.Append(generateHTMLWithCoefficients());
                    sb.Append(")");
                    return sb.ToString();
                case GroupType.ElementWrapper:
                    return this[0].ToString();
                case GroupType.Products:
                    return generateHTMLForEquationSide();
                case GroupType.Reactants:
                    return generateHTMLForEquationSide();
                default:
                    throw new InvalidOperationException("ElementGroup Was Not A Recognized Type");
            }
        }

        private string generateStringForEquationSide()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Count; i++)
            {
                sb.Append(this[i].ToString());
                if (i != this.Count - 1)
                    sb.Append("+");
            } 
            return sb.ToString();
        }

        private string generateStringWithCoefficients()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, int> pair in contentCount)
            {
                if (pair.Value == 1)
                    sb.Append(pair.Key.ToString());
                else
                    sb.Append(pair.Key.ToString() + pair.Value.ToString());
            }
            return sb.ToString();
        }

        private string generateHTMLForEquationSide()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Count; i++)
            {
                sb.Append(this[i].ToHTML());
                if (i != this.Count - 1)
                    sb.Append("+");
            }
            return sb.ToString();
        }

        private string generateHTMLWithCoefficients()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, int> pair in contentCount)
            {
                if (pair.Value == 1)
                    sb.Append(pair.Key.ToString());
                else
                    sb.Append(pair.Key.ToString() + pair.Value.ToString());
            }
            for (int i = sb.Length - 1; i >= 0; i--)
            {
                if (Char.IsDigit(sb[i]))
                {
                    sb.Insert(i + 1, "</sub>");
                    sb.Insert(i, "<sub>");           
                }
            }
            return sb.ToString();
        }
    }
}
