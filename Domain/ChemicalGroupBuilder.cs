using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    static class ChemicalGroupBuilder
    {
        public static IChemical Build(string[] myStrings, bool isComplex)
        {
            ChemicalGroup myChemicalGroup = InitializeChemicalGroup(isComplex);
            IChemical chemical;
            for (int i = 0; i < myStrings.Length; i++)
            {
                chemical = TurnStringIntoMoleculeOrComplex(myStrings, ref i);
                if (chemical != null)
                {
                    int subscript = SetSubscript(myStrings, i);
                    AddChemicalToChemicalGroup(myChemicalGroup, chemical, subscript);
                }
                else
                {
                    CheckForErrors(myStrings, i);
                }
            }
            if (myChemicalGroup.Count == 1)      //If the final molecule contains only a single element, return the Element directly
                return myChemicalGroup[0];
            else
                return myChemicalGroup;
        }

        private static IChemical TurnStringIntoMoleculeOrComplex(string[] myStrings, ref int i)
        {
            IChemical chemical;
            if (myStrings[i] == "(")
            {
                int endParenthesisLoc = FindClosingParen(i, myStrings);
                chemical = FindNextComplex(myStrings, i, endParenthesisLoc);
                i = endParenthesisLoc;
            }
            else
                chemical = FindNextElement(myStrings[i]);
            return chemical;
        }

        private static ChemicalGroup InitializeChemicalGroup(bool isComplex)
        {
            ChemicalGroup myChemicalGroup;
            if (isComplex)
                myChemicalGroup = new Complex();
            else
                myChemicalGroup = new Molecule();
            return myChemicalGroup;
        }

        private static void AddChemicalToChemicalGroup(ChemicalGroup myChemicalGroup, IChemical chemical, int subscript)
        {
            while (subscript > 0)
            {
                myChemicalGroup.Add(chemical);
                subscript--;
            }
        }

        private static IChemical FindNextElement(string symbol)
        {
            Element myElement = null;
            if (TableOfElements.Instance.Count(x => x.Symbol == symbol) > 0)
                myElement = TableOfElements.Instance.First(x => x.Symbol == symbol);
            return myElement;
        }

        public static void CheckForErrors(string[] myStrings, int i)
        {
            int x;  //We don't use x for anything, but TryParse() requires an out operator.
            if (int.TryParse(myStrings[i], out x))
            {
                if (i == 0 && myStrings.Length > 1)  //Numbers shouldn't appear at the start of the molecule, since we're trying to balance it.
                    throw new ArgumentException("Parser found unexpected number at: " + myStrings[i].ToString() + myStrings[i + 1].ToString());
                else if (i == 0) //This would indicate the molecule to be parsed contained only a single number.
                    throw new ArgumentException("Parser attempted to parse number with no valid symbol attached: " + myStrings[i].ToString());
            }
            else
                throw new ArgumentException("Attempted to Parse Invalid Character: " + myStrings[i].ToString());
        }

        private static IChemical FindNextComplex(string[] myStrings, int i, int endParenthesisLoc)
        {
            List<string> complex = new List<string>();
            for (int n = i + 1; n < endParenthesisLoc; n++)
                complex.Add(myStrings[n]);
            IChemical myComplex = Build(complex.ToArray(), true);
            i = endParenthesisLoc; //We've added everything within the parentheses, so we need to set i to the closing parenthesis location.
            return myComplex;
        }

        private static int SetSubscript(string[] moleculeString, int i)
        {
            int subscript = 1;
            if (i >= moleculeString.Length - 1)
                return subscript;
            int.TryParse(moleculeString[i + 1], out subscript);
            if (subscript <= 0)
                subscript = 1;
            return subscript;
        }

        private static int FindClosingParen(int i, string[] moleculeString)
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