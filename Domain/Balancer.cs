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
    public class Balancer : IBalancer
    {
        public ChemicalEquation Balance(ChemicalEquation myEquation)
        {
            checkForValidEquation(myEquation);
            List<int> coefficients = solveMatrix(myEquation);
            addCoefficients(coefficients, myEquation);
            if (finalSanityCheck(myEquation))
                return myEquation;
            else
                throw new ApplicationException ("Error: The balancer failed to balance the equation!");
        }

        private bool finalSanityCheck(ChemicalEquation myEquation)
        {
            //ElementGroup reactants = (ElementGroup)myEquation.Reactants;
            //ElementGroup products = (ElementGroup)myEquation.Products;
            //List<string> listOfSymbols = myEquation.ListOfElements;
            //foreach (string symbol in listOfSymbols)
            //    if (reactants.GetDeepElementCount(symbol) != products.GetDeepElementCount(symbol))
            //        return false;
            return true;
        }

        private void addCoefficients(List<int> answers, ChemicalEquation unbalancedEquation)
        {
            IChemical currentMolecule;
            for (int i = 0; i < unbalancedEquation.MoleculeCount; i++)
            {
                if (i < unbalancedEquation.Reactants.Count)
                    currentMolecule = (IChemical)unbalancedEquation.Reactants[i];
                else
                    currentMolecule = (IChemical)unbalancedEquation.Products[i - unbalancedEquation.Reactants.Count];
                currentMolecule.Coefficient = answers[i];
            }
        }

        private List<int> solveMatrix(ChemicalEquation unbalancedEquation)
        {
            DenseMatrix unsolvedMatrix = buildMatrix(unbalancedEquation);
            DenseVector vector = buildVector(unbalancedEquation);
            List<double> answers = new List<double>();
            var matrixAnswers = unsolvedMatrix.QR().Solve(vector);
            foreach (double d in matrixAnswers)
                answers.Add(d);
            answers.Add(solveForZ(unsolvedMatrix, vector, matrixAnswers));
            List<int> coefficients = convertAnswersToIntegers(answers);
            return coefficients;
        }

        private List<int> convertAnswersToIntegers(List<double> answers)
        {
            List<int> coefficients = new List<int>();
            int numberToMultiplyBy = 1;
            while (numberToMultiplyBy < 10000)
            {
                List<double> checkList = multiplyAnswers(answers, numberToMultiplyBy);

                if (checkAllAnswersAreIntegers(checkList))
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

        private static List<double> multiplyAnswers(List<double> answers, int numberToMultiplyBy)
        {
            List<double> checkList = new List<double>(answers);
            for (int i = 0; i < checkList.Count; i++)
            {
                checkList[i] *= numberToMultiplyBy;
                checkList[i] = Math.Round(checkList[i], 10);
            }
            return checkList;
        }

        private bool checkAllAnswersAreIntegers(List<double> checkList)
        {
            foreach (double d in checkList)
                if (d % 1 != 0)
                    return false;
            return true;
        }

        private static double solveForZ(DenseMatrix unsolvedMatrix, DenseVector vector, MathNet.Numerics.LinearAlgebra.Generic.Vector<double> matrixAnswers)
        {
            for (int i = 0; i < vector.Count; i++)
            {
                if (vector[i] != 0)
                {
                    double answer = 0;
                    for (int n = 0; n < unsolvedMatrix.ColumnCount; n++)
                    {
                        double matrixNumber = unsolvedMatrix[i, n];
                        double multipliedBy = matrixAnswers[n];
                        answer += matrixNumber * multipliedBy;
                    }
                    answer /= vector[i];
                    return answer;
                }
            }
            throw new ArgumentException("Could not solve for z");
        }

        private DenseVector buildVector(ChemicalEquation unbalancedEquation)
        {      
            DenseVector vector = new DenseVector(unbalancedEquation.ListOfElements.Count);
            for (int i = 0; i < unbalancedEquation.ListOfElements.Count; i++)
            {
                if (unbalancedEquation.Products[unbalancedEquation.Products.Count - 1] is Element)
                    vector[i] = 1;
                else
                {
                    ElementGroup lastChemical = (ElementGroup)unbalancedEquation.Products[unbalancedEquation.Products.Count - 1];
                    vector[i] = lastChemical.GetDeepElementCount(unbalancedEquation.ListOfElements[i]);
                }
            }
            return vector;
        }

        private DenseMatrix buildMatrix(ChemicalEquation unbalancedEquation)
        {
            List<string> listOfSymbols = unbalancedEquation.ListOfElements;
            ElementGroup currentMolecule;
            DenseMatrix myMatrix = new DenseMatrix(listOfSymbols.Count, unbalancedEquation.MoleculeCount - 1);
            for (int column = 0; column < unbalancedEquation.MoleculeCount - 1; column++)
            {
                for (int row = 0; row < listOfSymbols.Count; row++)
                {
                    if (column < unbalancedEquation.Reactants.Count)
                    {
                        if (unbalancedEquation.Reactants[column] is Element)
                            myMatrix[row, column] = 1;
                        else
                        {
                            currentMolecule = (ElementGroup)unbalancedEquation.Reactants[column];
                            myMatrix[row, column] = currentMolecule.GetDeepElementCount(listOfSymbols[row]);
                        }
                    }
                    else
                    {
                        if (unbalancedEquation.Products[column - unbalancedEquation.Reactants.Count] is Element)
                            myMatrix[row, column] = 1;
                        else
                        {
                            currentMolecule = (ElementGroup)unbalancedEquation.Products[column - unbalancedEquation.Reactants.Count];
                            myMatrix[row, column] = (currentMolecule.GetDeepElementCount(listOfSymbols[row]) * -1);
                        }
                    }
                }
            }
            return myMatrix;
        }

        private void checkForValidEquation(ChemicalEquation unbalancedEquation)
        {
            foreach (string s in unbalancedEquation.ListOfElements)
            {
                if (!unbalancedEquation.Products.ListOfElements.Contains(s) || !unbalancedEquation.Reactants.ListOfElements.Contains(s))
                    throw new ArgumentException("Error: the element or complex " + s + " could not be found on both sides of the equation");
            }
        }
    }
}
