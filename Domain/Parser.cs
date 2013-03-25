using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class Parser : IParser
    {
        TableOfElements tableofElements = new TableOfElements();

        public IEquation Parse(string reaction)
        {
            ChemicalEquation myChemicalEquation = new ChemicalEquation();

            if (!validateString(reaction))
            {
                //Do whatever we do when the string isn't valid.
            }

            //This should take our validly formatted string and turn it int an array of strings, each one containing a symbol or operator.
            StringBuilder sb = new StringBuilder();
            foreach (char letter in reaction)
            {
                if ((Char.IsUpper(letter) || Char.IsNumber(letter) || letter == '+' || letter == '>' || letter == '-' || letter == ')' || letter == '(') && sb.Length > 0)
                    sb.Append(",");
                sb.Replace(" ", "");
                sb.Append(letter);
            }
            sb.Replace(" ", "");
            reaction = sb.ToString();
            string[] reactionComponents = reaction.Split(',');


            //OK, now that we have our array of reaction parts, let's loop through it and build our chemical equation. 
            List<string> moleculeString = new List<string>();
            bool parsingReactants = true;
            for (int i = 0; i < reactionComponents.Length; i++)
            {
                if (reactionComponents[i] == "+" || reactionComponents[i] == ">")
                {
                    if (parsingReactants)
                        myChemicalEquation.Reactants.Add(buildElementGroup(moleculeString, "molecule"));
                    else
                        myChemicalEquation.Products.Add(buildElementGroup(moleculeString, "molecule"));
                    moleculeString = new List<string>();
                    if (reactionComponents[i] == ">")
                        parsingReactants = false;
                }
                else
                    moleculeString.Add(reactionComponents[i]);
            }
            myChemicalEquation.Products.Add(buildElementGroup(moleculeString, "molecule"));
            return myChemicalEquation;

            //This is some regex code I found online for seperating capital letters. It has problems, though, and I don't know regular expressions. 
            //I may try to fix this later, though, so I'll keep it commented out here.
            //var regex = new Regex(@"(?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
            //regex.Replace(reaction, ",");   
        }

        private IParsableSymbols buildElementGroup(List<string> moleculeString, string type)
        {
            ElementGroup molecule = new ElementGroup(type);
            int subscript = 1;
            for (int i = 0; i < moleculeString.Count; i++)
            {
                if (i < moleculeString.Count - 1)
                {
                    int.TryParse(moleculeString[i + 1], out subscript);
                    if (subscript <= 0) //This indicates that the TryParse failed and set the subscript to zero
                        subscript = 1;
                }
                if (moleculeString[i] == "(")
                {
                    int endBracketLoc = findClosingParen(i, moleculeString);
                    List<string> complex = new List<string>();
                    for (int n = i + 1; n < endBracketLoc; n++)
                        complex.Add(moleculeString[n]);
                    int.TryParse(moleculeString[endBracketLoc + 1], out subscript);
                    while (subscript > 0)
                    {
                        molecule.Add(buildElementGroup(complex, "complex"));
                        subscript--;
                    }
                    i = endBracketLoc;
                    continue;
                }
                foreach (Element element in tableofElements)
                    if (moleculeString[i] == element.Symbol)
                        for (int n = 0; n < subscript; n++)
                            molecule.Add(element);
                subscript = 1; //Reset subscript for next pass.
            }
            if (molecule.Count == 1)
                return molecule[0];
            else
                return molecule;
        }

        private int findClosingParen(int i, List<string> moleculeString)
        {
            int openBrackets = 1;
            while (openBrackets > 0)
            {
                i++;
                if (moleculeString[i] == ")")
                    openBrackets--;
                if (moleculeString[i] == "(")
                    openBrackets++;
            }
            return i;
        }

        private bool validateString(string reaction)
        {
            return true; //Currently, no logic for this, it just says all inputs are valid.
        }
    }
}
