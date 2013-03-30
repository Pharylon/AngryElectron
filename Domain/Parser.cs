using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class Parser : IParser
    {
        ElementGroupBuilder myBuilder = new ElementGroupBuilder();

        public IEquation Parse(string inputString)
        {
            string[] symbolArray = createSymbolArray(inputString);
            ChemicalEquation myChemicalEquation = convertArrayToEquation(symbolArray);
            return myChemicalEquation;  
        }

        private ChemicalEquation convertArrayToEquation(string[] symbolArray)
        {
            ChemicalEquation myChemicalEquation = new ChemicalEquation();
            List<string> moleculeString = new List<string>();
            Side parsingSide = Side.Products;
            for (int i = 0; i < symbolArray.Length; i++)
            {
                if (symbolArray[i] == "+" || symbolArray[i] == ">" || symbolArray[i] == "|")  //Finding one of these operators tells us we're at the end of a molecule.
                {
                    myChemicalEquation.AddToEquation(myBuilder.buildElementGroup(moleculeString, GroupType.Molecule), parsingSide);
                    moleculeString = new List<string>(); //reset for the next loop
                    if (symbolArray[i] == ">")
                        parsingSide = Side.Reactants;
                }
                else
                    moleculeString.Add(symbolArray[i]); //If we didn't find an "end of molecule" operator, the symbol is added to moleculeString. 
            }
            return myChemicalEquation;
        }

        private static string[] createSymbolArray(string inputString)
        {
            inputString = normalizeCharacters(inputString);
            StringBuilder commaSeperatedSymbols = generateCommaSeperatedSymbols(inputString);
            removeUnwantedCharacters(commaSeperatedSymbols);
            string[] symbolArray = commaSeperatedSymbols.ToString().Split(',');
            return symbolArray;
        }

        private static void removeUnwantedCharacters(StringBuilder commaSeperatedSymbols)
        {
            commaSeperatedSymbols.Replace(" ", "");
            commaSeperatedSymbols.Replace("-", "");
        }

        private static StringBuilder generateCommaSeperatedSymbols(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < inputString.Length; i++)
            {
                if ((Char.IsUpper(inputString[i]) || inputString[i] == '+' || inputString[i] == '>' || inputString[i] == ')' || inputString[i] == '(') && sb.Length > 0)
                    sb.Append(",");
                if (char.IsDigit(inputString[i]))
                {
                    if (i == 0)
                        throw new ArgumentException("Parser encountered an error: Equations may not begin with a number");
                    else
                        if (!char.IsDigit(inputString[i - 1])) //Make sure this is the last digit in case of a two digit number.
                            sb.Append(",");
                }
                sb.Append(inputString[i]);
            }
            sb.Append(",|"); //Pipe is used as an "end of equation" character for the parser.
            return sb;
        }

        private static string normalizeCharacters(string inputString)
        {
            inputString = inputString.Replace("=", ">");
            inputString = inputString.Replace("[", "(");
            inputString = inputString.Replace("]", ")");
            return inputString;
        }
    }
}
