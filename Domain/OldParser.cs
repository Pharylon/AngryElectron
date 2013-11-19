using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public static class OldParser
    {
        public static ChemicalEquation Parse(string inputString)
        {
            string[] symbolArray = CreateSymbolArray(inputString);
            ChemicalEquation myChemicalEquation = ConvertArrayToEquation(symbolArray);
            CheckForValidEquation(myChemicalEquation);
            return myChemicalEquation;  
        }

        private static ChemicalEquation ConvertArrayToEquation(string[] symbolArray)
        {
            int arrowLocation = Array.IndexOf(symbolArray, ">");
            string[][] leftSide = ConvertToArrayofArrays(symbolArray.SubArray(0, arrowLocation));
            string[][] rightSide = ConvertToArrayofArrays(symbolArray.SubArray(arrowLocation + 1, symbolArray.Length - (arrowLocation + 1)));
            ChemicalEquation myChemicalEquation = new ChemicalEquation();
            foreach (string[] s in leftSide)
                myChemicalEquation.Add(ChemicalGroupBuilder.Build(s, false), Side.LeftSide);
            foreach (string[] s in rightSide)
                myChemicalEquation.Add(ChemicalGroupBuilder.Build(s, false), Side.RightSide);
            return myChemicalEquation;
        }

        private static string[][] ConvertToArrayofArrays(string[] symbolArray)
        {
            List<string[]> listofArrays = new List<string[]>();
            List<string> list = new List<string>();
            foreach (string s in symbolArray)
            {
                if (s != "+")
                {
                    list.Add(s);
                }
                else
                {
                    if (list.Count > 0)
                        listofArrays.Add(list.ToArray()); //add the molecule when we hit the + sign and reset the list.
                    list = new List<string>();
                }
            }
            if (list.Count > 0)
                listofArrays.Add(list.ToArray()); //add last molecule when the loop is over.
            return listofArrays.ToArray();
        }

        private static string[] CreateSymbolArray(string inputString)
        {
            inputString = NormalizeCharacters(inputString);
            StringBuilder commaSeperatedSymbols = GenerateCommaSeperatedSymbols(inputString);
            RemoveUnwantedCharacters(commaSeperatedSymbols);
            string[] symbolArray = commaSeperatedSymbols.ToString().Split(',');
            return symbolArray;
        }

        private static void RemoveUnwantedCharacters(StringBuilder commaSeperatedSymbols)
        {
            commaSeperatedSymbols.Replace(" ", "");
            commaSeperatedSymbols.Replace("-", "");
            RemoveExtraArrows(commaSeperatedSymbols);
        }

        private static void RemoveExtraArrows(StringBuilder commaSeperatedSymbols)
        {
            bool foundFirstArrow = false;
            for (int i = commaSeperatedSymbols.Length - 1; i > 0; i--)
            {
                if (commaSeperatedSymbols[commaSeperatedSymbols.Length - i] == '>')
                {
                    if (foundFirstArrow == false)
                        foundFirstArrow = true;
                    else
                        commaSeperatedSymbols.Remove(commaSeperatedSymbols.Length - i, 2);
                }
            }
        }

        private static StringBuilder GenerateCommaSeperatedSymbols(string inputString)
        {
            char[] operatorCharacters = new [] {'>', '+', '(', ')'};
            if (inputString.Length == 0)
            {
                throw new ArgumentException("Please enter some information!");
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < inputString.Length; i++)
            {
                if ((Char.IsUpper(inputString[i]) || operatorCharacters.Contains(inputString[i])) && sb.Length > 0)
                    sb.Append(",");
                if (char.IsDigit(inputString[i]))
                {
                    if (i == 0)
                        throw new ArgumentException("Parser encountered an error: Equations may not begin with a number.");
                    else
                        if (!char.IsDigit(inputString[i - 1])) //Make sure this is the last digit in case of a two digit number.
                            sb.Append(",");
                }
                sb.Append(inputString[i]);
            }
            //sb.Append(",|"); //Pipe is used as an "end of equation" character for the parser.
            return sb;
        }

        private static string NormalizeCharacters(string inputString)
        {
            inputString = inputString.Replace("[", "(");
            inputString = inputString.Replace("]", ")");
            inputString = inputString.Replace("=", ">");
            inputString = inputString.Replace(" ", "+");
            return inputString;
        }

        private static void CheckForValidEquation(ChemicalEquation unbalancedEquation)
        {
            foreach (Element e in unbalancedEquation.Elements)
            {
                if (!unbalancedEquation.Products.Elements.Contains(e) || !unbalancedEquation.Reactants.Elements.Contains(e))
                    throw new ArgumentException("Error: the element or complex " + e.ToString() + " could not be found on both sides of the equation.");
            }
        }
    }
}
