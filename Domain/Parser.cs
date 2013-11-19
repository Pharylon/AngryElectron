using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngryElectron.Domain;

namespace AngryElectron.Domain
{
    public static class Parser
    {
        public static ChemicalEquation Parse(string inputString)
        {
            ChemicalEquation myEquation = new ChemicalEquation();
            var equationSideStrings = getEquationSides(inputString);
            myEquation.Reactants = ParseEquationSide(equationSideStrings["leftSide"]);
            myEquation.Products = ParseEquationSide(equationSideStrings["rightSide"]);
            return myEquation;
        }

        private static Dictionary<string, string> getEquationSides(string inputString)
        {
            Dictionary<string, string> equationSideStrings = new Dictionary<string, string>();
            inputString = NormalizeCharacters(inputString);
            int arrowLocation = inputString.IndexOf('>');
            equationSideStrings.Add("leftSide", inputString.Substring(0, arrowLocation));
            equationSideStrings.Add("rightSide", inputString.Substring(arrowLocation + 1, inputString.Length - arrowLocation - 1));
            return equationSideStrings;
        }

        private static EquationSide ParseEquationSide(string inputString)
        {
            EquationSide mySide = new EquationSide();
            var stringArray = inputString.Trim().Split(' ');
            foreach (var myString in stringArray)
            {
                mySide.Add(ParseMolecule(myString));
            }
            return mySide;
        }

        private static IChemical ParseMolecule(string myString)
        {
            Molecule myMolecule = new Molecule();;
            Tuple<IChemical, int>[] myTuples = CreateMoleculeTuple(myString);
            foreach (var myTuple in myTuples)
                for (int i = 0; i < myTuple.Item2; i++)
                    myMolecule.Add(myTuple.Item1);
            return myMolecule;
        }

        private static IChemical ParseComplex(string myString)
        {
            Complex myComplex = new Complex();
            Tuple<IChemical, int>[] myTuples = CreateMoleculeTuple(myString);
            foreach (var myTuple in myTuples)
                for (int i = 0; i < myTuple.Item2; i++)
                    myComplex.Add(myTuple.Item1);
            return myComplex;
        }

        private static Tuple<IChemical, int>[] CreateMoleculeTuple(string myString)
        {
            List<Element> myElements;
            var myTupleList = new List<Tuple<IChemical, int>>();
            var myComplexList = new List<Tuple<IChemical, int>>();
            while (myString.Contains('('))
                myComplexList = ProcessComplex(ref myString);
            string elementString = string.Empty;
            string numberString = string.Empty;
            bool parsingString = true;
            myString = StripCoefficients(myString);
            foreach (char myChar in myString) {
                if (!char.IsDigit(myChar))
                {
                    if (!parsingString)
                    {
                        myElements = ConvertStringToElementList(elementString);
                        myTupleList.AddRange(from IChemical element in myElements select Tuple.Create(element, 1));
                        myTupleList[myTupleList.Count - 1] = Tuple.Create(myTupleList.Last().Item1, ParseOne(numberString));
                        elementString = string.Empty;
                        numberString = string.Empty;
                        parsingString = true;
                    }
                    elementString += myChar;
                }
                else
                {
                    numberString += myChar;
                    parsingString = false;
                }
            }
            myElements = ConvertStringToElementList(elementString);
            myTupleList.AddRange(from IChemical element in myElements select Tuple.Create(element, 1));
            myTupleList[myTupleList.Count - 1] = Tuple.Create(myTupleList.Last().Item1, ParseOne(numberString));
            if (myComplexList.Any())
                myTupleList = myTupleList.Union(myComplexList).ToList();
            return myTupleList.ToArray();
        }

        private static List<Tuple<IChemical, int>> ProcessComplex(ref string myString)
        {
            var myTupleList = new List<Tuple<IChemical, int>>();
            int indexofLeftParen = myString.IndexOf('(');
            int indexofRightParen = myString.IndexOf(')');
            int number = 1;
            string complexString = myString.Substring(indexofLeftParen + 1, indexofRightParen - indexofLeftParen - 1);
            if (myString.Length > indexofRightParen + 1)
                number = ParseOne(myString.Substring(indexofRightParen + 1, 1));
            var myComplex = ParseComplex(complexString);
            myTupleList.Add(Tuple.Create(myComplex, number));
            int trimLength = indexofRightParen - indexofLeftParen + 1 + DigitsInCoefficient(number);
            myString = myString.Remove(indexofLeftParen, trimLength);
            return myTupleList;
        }

        public static int DigitsInCoefficient(int number)
        {
            int returnNumber = 0;
            if (number != 1)
                returnNumber = (number/10) + 1;
            return returnNumber;
        }

        private static string StripCoefficients(string myString)
        {
            while (char.IsDigit(myString[0]))
                myString = myString.Remove(0, 1);
            return myString;
        }

        public static int ParseOne(string myString)
        {
            int myInt = 0;
            return int.TryParse(myString, out myInt) ? myInt : 1;
        }


        private static string NormalizeCharacters(string inputString)
        {
            inputString = inputString.Replace("[", "(");
            inputString = inputString.Replace("-", " ");
            inputString = inputString.Replace("]", ")");
            inputString = inputString.Replace("=", ">");
            while (inputString.Contains(">>"))
                inputString = inputString.Replace(">>", ">");
            inputString = inputString.Replace("+", " ");
            inputString = inputString.TrimInternal();
            inputString = inputString.Trim();
            return inputString;
        }


        public static List<Element> ConvertStringToElementList(string myString)
        {
            var myElements = new List<Element>();
            if (TableOfElements.Instance.Any(x => x.Symbol == myString))
            {
                myElements.Add(TableOfElements.Instance.First(x => x.Symbol == myString));
            }
            else if (myString.Count() > 1 && char.IsUpper(myString[0]) && char.IsLower(myString[1]))
            {
                myElements = AddSubListOfElements(myString, myElements, 2);
            }
            else if (myString.Count() > 1 && TableOfElements.Instance.Any(x => x.Symbol == myString.Substring(0, 1)))
            {
                myElements = AddSubListOfElements(myString, myElements, 1);
            }
            else if (myString.Count() > 2 && TableOfElements.Instance.Any(x => x.Symbol == myString.Substring(0, 2)))
            {
                myElements = AddSubListOfElements(myString, myElements, 2);
            }
            else if (TableOfElements.Instance.Any(x => x.Symbol.ToUpper() == myString.ToUpper()))
            {
                myElements.Add(TableOfElements.Instance.First(x => x.Symbol.ToUpper() == myString.ToUpper()));
            }
            if (!myElements.Any() && TableOfElements.Instance.Any(x => x.Symbol.ToUpper() == myString.ToUpper()))
            {
                myElements.Add(TableOfElements.Instance.First(x => x.Symbol.ToUpper() == myString.ToUpper()));
            }
            return myElements;
        }

        private static List<Element> AddSubListOfElements(string myString, List<Element> myElements, int subStringNumber)
        {
            var mySubList = ConvertStringToElementList(myString.Substring(subStringNumber, myString.Length - subStringNumber));
            if (mySubList.Any())
            {
                myElements.Add(TableOfElements.Instance.First(x => x.Symbol == myString.Substring(0, subStringNumber)));
                myElements = myElements.Union(mySubList).ToList();
            }
            return myElements;
        }
    }
}
