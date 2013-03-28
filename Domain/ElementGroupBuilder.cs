﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    class ElementGroupBuilder
    {
        Dictionary<string, Element> dictionaryOfElements;

        public ElementGroupBuilder()
        {
            initializeDictionaryOfElements();
        }

        private void initializeDictionaryOfElements()
        {
            TableOfElements tableOfElements = new TableOfElements();
            dictionaryOfElements = new Dictionary<string, Element>();
            foreach (Element element in tableOfElements)
                dictionaryOfElements.Add(element.Symbol, element);
        }

        public IParsableSymbols buildElementGroup(List<string> stringList, string type)
        {
            ElementGroup myElementGroup = new ElementGroup(type);
            IParsableSymbols chemical;
            for (int i = 0; i < stringList.Count; i++)
            {
                if (stringList[i] == "(") 
                    chemical = findNextComplex(stringList, ref i);
                else
                    chemical = findNextElement(stringList[i]);
                if (chemical == null)
                    checkForErrors(stringList, i);
                else
                {
                    int subscript = setSubscript(stringList, i);
                    while (subscript > 0 && chemical != null)
                    {
                        myElementGroup.Add(chemical);
                        subscript--;
                    }
                }
            }
            if (myElementGroup.Count == 1)  //If the final molecule contains only a single element, there's no need to return it as a molecule.
                return myElementGroup[0];   //Elements also now implement IParsableSymbol, so it can be returned directly.
            else
                return myElementGroup;
        }

        private IParsableSymbols findNextElement(string stringToCheck)
        {
            if (dictionaryOfElements.ContainsKey(stringToCheck))
                return dictionaryOfElements[stringToCheck];
            else
                return null;
        }

        public void checkForErrors(List<string> stringList, int i)
        {
            int x;  //We don't use x for anything, but TryParse() requires an out operator.
            if (int.TryParse(stringList[i], out x)) 
            {
                if (i == 0 && stringList.Count > 1)  //Numbers shouldn't appear at the start of the molecule, since we're trying to balance it.
                    throw new ArgumentException("Parser found unexpected number at: " + stringList[i].ToString() + stringList[i + 1].ToString());
                else if (i == 0) //This would indicate the molecule to be parsed contained only a single number.
                    throw new ArgumentException("Parser attempted to parse number with no valid symbol attached: " + stringList[i].ToString());
            }
            else
                throw new ArgumentException("Attempted to Parse Invalid Character: " + stringList[i].ToString());
        }

        private IParsableSymbols findNextComplex(List<string> stringList, ref int i)
        {
            int endParenthesisLoc = findClosingParen(i, stringList);
            List<string> complex = new List<string>();
            for (int n = i + 1; n < endParenthesisLoc; n++)
                complex.Add(stringList[n]);
            IParsableSymbols myComplex = buildElementGroup(complex, "complex");
            i = endParenthesisLoc; //We've parsed everything within the parentheses, so we need to set i to the closing parenthesis location.
            return myComplex;
        }

        private int setSubscript(List<string> moleculeString, int i)
        {
            int subscript = 1;
            if (i >= moleculeString.Count - 1)
                return subscript;
            int.TryParse(moleculeString[i + 1], out subscript);
            if (subscript <= 0)
                subscript = 1;
            return subscript;
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
