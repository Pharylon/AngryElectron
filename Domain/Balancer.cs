using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;
using MathNet.Numerics.LinearAlgebra.Double.Factorization;
using System.Numerics;

namespace AngryElectron.Domain
{
    public static class Balancer
    {
        public static ChemicalEquation Balance(ChemicalEquation myEquation)
        {
            bool equationFlipped = PrepareEquationForProcessing(myEquation); //For some reason, the Matrix solving is failing when all the products have a subscript of 1. This is a HACK to fix this issue.
            List<int> coefficients = GetCoefficients(myEquation);
            AddCoefficients(coefficients, myEquation);
            FinalSanityCheck(myEquation);
            if (equationFlipped) //If we had to flip the equation in the beginning, flip it back before returning to the user.
            {
                FlipEquation(myEquation);
            }
            return myEquation;
        }

        private static bool PrepareEquationForProcessing(ChemicalEquation myEquation) 
        {   
            bool AllProductsHaveSubscriptOf1 = true;
            foreach (var element in myEquation.Elements)
            {
                if (myEquation.Products.GetDeepElementCount(element) > 1)
                    AllProductsHaveSubscriptOf1 = false;
            }
            if (AllProductsHaveSubscriptOf1)
            {
                FlipEquation(myEquation);
            }
            return AllProductsHaveSubscriptOf1;
        }

        private static void FlipEquation(ChemicalEquation myEquation)
        {
            EquationSide placeHolder = myEquation.Reactants;
            myEquation.Reactants = myEquation.Products;
            myEquation.Products = placeHolder;
        }

        private static List<int> GetCoefficients(ChemicalEquation unbalancedEquation)
        {
            DenseMatrix unsolvedMatrix = BuildMatrix(unbalancedEquation);
            DenseVector vector = BuildVector(unbalancedEquation);
            List<double> answers = MatrixSolver.Solve(unsolvedMatrix, vector);
            List<int> coefficients = ConvertAnswersToIntegers(answers);
            return coefficients;
        }

        private static void FinalSanityCheck(ChemicalEquation myEquation)
        {
            if (!myEquation.IsBalanced)
                throw new Exception("Error: Blancer failed to balance the equation!");
        }

        private static void AddCoefficients(List<int> answers, ChemicalEquation unbalancedEquation)
        {
            IChemical currentChemical;
            for (int i = 0; i < unbalancedEquation.MoleculeCount; i++)
            {
                if (i < unbalancedEquation.Reactants.Count)
                {
                    currentChemical = unbalancedEquation.Reactants[i];
                    unbalancedEquation.Reactants.Coefficients[currentChemical] = answers[i];
                }
                else
                {
                    currentChemical = unbalancedEquation.Products[i - unbalancedEquation.Reactants.Count];
                    unbalancedEquation.Products.Coefficients[currentChemical] = answers[i];
                }
            }
        }

        private static DenseVector BuildVector(ChemicalEquation unbalancedEquation)
        {
            DenseVector vector = new DenseVector(unbalancedEquation.Elements.Length);
            for (int i = 0; i < unbalancedEquation.Elements.Length; i++)
            {
                if (unbalancedEquation.Products[unbalancedEquation.Products.Count - 1] is Element)
                    vector[i] = 1;
                else
                {
                    ChemicalGroup lastChemical = (ChemicalGroup)unbalancedEquation.Products[unbalancedEquation.Products.Count - 1];
                    vector[i] = lastChemical.GetDeepElementCount(unbalancedEquation.Elements[i]);
                }
            }
            return vector;
        }

        private static DenseMatrix BuildMatrix(ChemicalEquation unbalancedEquation)
        {
            Element[] myElements = unbalancedEquation.Elements;
            Side processingSide = Side.LeftSide;
            DenseMatrix myMatrix = new DenseMatrix(myElements.Length, unbalancedEquation.MoleculeCount - 1);
            for (int column = 0; column < unbalancedEquation.MoleculeCount - 1; column++)
            {
                for (int row = 0; row < myElements.Length; row++)
                {
                    if (column >= unbalancedEquation.Reactants.Count)
                        processingSide = Side.RightSide;
                    myMatrix[row, column] = GetMatrixPoint(unbalancedEquation, processingSide, column, row, myElements);
                }
            }
            return myMatrix;
        }

        private static double GetMatrixPoint(ChemicalEquation unbalancedEquation, Side processingSide, int column, int row, Element[] elements)
        {
            EquationSide currentSide = SetCurrentProcessingSide(unbalancedEquation, processingSide);
            if (processingSide == Side.RightSide)
                column -= unbalancedEquation.Reactants.Count;
            double matrixPoint = GetElementCountOfChemical(column, row, elements, currentSide);
            if (processingSide == Side.RightSide)
                matrixPoint *= -1.0;
            return matrixPoint;
        }

        private static double GetElementCountOfChemical(int column, int row, Element[] elements, EquationSide currentSide)
        {
            double matrixPoint = 0;
            if (currentSide[column] == elements[row]) //check to see if the current column is a single instance of the element we are searching for.
                matrixPoint = 1.0;
            else if (currentSide[column] is ChemicalGroup)
            {
                ChemicalGroup currentMolecule = (ChemicalGroup)currentSide[column];
                matrixPoint = currentMolecule.GetDeepElementCount(elements[row]);
            }
            return matrixPoint;
        }

        private static EquationSide SetCurrentProcessingSide(ChemicalEquation unbalancedEquation, Side processingSide)
        {
            if (processingSide == Side.LeftSide)
                return unbalancedEquation.Reactants;
            else
                return unbalancedEquation.Products;
        }

        private static List<int> ConvertAnswersToIntegers(List<double> answers)
        {
            List<int> coefficients = new List<int>();
            int numberToMultiplyBy = 1;
            while (numberToMultiplyBy < 10000)
            {
                List<double> checkList = MultiplyAnswers(answers, numberToMultiplyBy);

                if (CheckAllAnswersAreIntegers(checkList))
                {
                    foreach (double d in checkList)
                        coefficients.Add((int)d);
                    return coefficients;
                }
                else
                    numberToMultiplyBy++;
            }
            throw new ArgumentException("Error: Could not determine integer values of coefficients");
        }

        private static bool CheckAllAnswersAreIntegers(List<double> checkList)
        {
            foreach (double d in checkList)
                if (d % 1 != 0)
                    return false;
            return true;
        }

        private static List<double> MultiplyAnswers(List<double> answers, int numberToMultiplyBy)
        {
            List<double> checkList = new List<double>(answers);
            for (int i = 0; i < checkList.Count; i++)
            {
                checkList[i] *= numberToMultiplyBy;
                checkList[i] = Math.Round(checkList[i], 10);
            }
            return checkList;
        }
    }
}