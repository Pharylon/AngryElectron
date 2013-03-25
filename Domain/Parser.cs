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

            //This should take our string and turn it int an array of strings, each one containing a symbol or operator.
            StringBuilder sb = new StringBuilder();
            foreach (char letter in reaction)
            {
                if ((Char.IsUpper(letter) || Char.IsNumber(letter) || letter == '+' || letter == '>' || letter == ')' || letter == '(') && sb.Length > 0)
                    sb.Append(",");
                sb.Append(letter);
            }
            sb.Replace(" ", "");
            sb.Replace("-", "");
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
                        subscript = 1; //It needs to be set back to 1 since that's how many times the molecule will be added.
                }
                if (moleculeString[i] == "(")  //If we find an opening parenthesis, we'll need to parse this as a seperate ElementGroup. Yay recursion, I guess!
                {
                    int endBracketLoc = findClosingParen(i, moleculeString);
                    List<string> complex = new List<string>();
                    for (int n = i + 1; n < endBracketLoc; n++)
                        complex.Add(moleculeString[n]);
                    int.TryParse(moleculeString[endBracketLoc + 1], out subscript); //Complexes always have subscripts, so we need that.
                    while (subscript > 0)
                    {
                        molecule.Add(buildElementGroup(complex, "complex"));
                        subscript--;
                    }
                    i = endBracketLoc; //We've parsed everything within the parenthesis, so we need to set i to the closing paren location.
                }
                else
                {
                    bool foundValidSymbol = false;
                    foreach (Element element in tableofElements)   //We're going to take the moleculeString element and compare it to each
                        if (moleculeString[i] == element.Symbol)   //element in the tableOfElements to see what element it is.
                            while (subscript > 0)                  //Once we find the right element, it gets added to the molecule.
                            {
                                foundValidSymbol = true;
                                molecule.Add(element);
                                subscript--;
                            }
                    int x;  //We don't use x for anything, but TryParse() requires an out operator.
                    if (int.TryParse(moleculeString[i], out x)) // If the symbol isn't a valid element, we check to see if it's a number
                    {
                        if (i == 0)
                            throw new ArgumentException("Invalid leading number found in string: " + moleculeString[i].ToString() + moleculeString[i+1].ToString()); //Numbers shouldn't appear at the start of the equation. We're trying to balance it.
                        else
                            foundValidSymbol = true; //If the number is anywhere else, that's fine.
                    }
                    if (!foundValidSymbol)
                        throw new ArgumentException("Invalid character found: " + moleculeString[i].ToString());
                }
                subscript = 1; //Reset subscript for next pass.
            }
            if (molecule.Count == 1)  //If the final molecule contains only a single element, there's no need to return it as a molecule
                return molecule[0];   //Elements also now implement IParsableSymbol, so it can be returned directly.
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
    }
}
