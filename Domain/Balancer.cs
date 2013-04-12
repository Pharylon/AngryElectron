using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;
using MathNet.Numerics.LinearAlgebra.Double.Factorization;
using Mathematics.RationalNumbers;
using System.Numerics;

namespace AngryElectron.Domain
{
    public class Balancer : IBalancer
    {
        public IEquation Balance(IEquation myEquation)
        {
            checkForValidEquation(myEquation);
            List<int> coefficients = solveMatrix(myEquation);
            addCoefficients(coefficients, myEquation);
            return myEquation;
        }

        private void addCoefficients(List<int> answers, IEquation unbalancedEquation)
        {
            ElementGroup reactants = (ElementGroup)unbalancedEquation.Reactants;
            ElementGroup products = (ElementGroup)unbalancedEquation.Products;
            ElementGroup currentMolecule;
            for (int i = 0; i < unbalancedEquation.MoleculeCount; i++)
            {
                if (i < reactants.Count)
                    currentMolecule = (ElementGroup)reactants[i];
                else
                    currentMolecule = (ElementGroup)products[i - reactants.Count];
                currentMolecule.Coefficient = answers[i];
            }
        }

        private List<int> solveMatrix(IEquation unbalancedEquation)
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
            bool integersSolved = false;
            int numberToMultiplyBy = 1;
            while (!integersSolved || numberToMultiplyBy < 1000)
            {
                List<double> checkList = new List<double>(answers);
                for (int i = 0; i < checkList.Count; i++)
                {
                    checkList[i] *= numberToMultiplyBy;
                    checkList[i] = Math.Round(checkList[i], 10);
                }
                bool allIntegers = true;
                foreach (double d in checkList)
                    if (d % 1 != 0)
                    {
                        allIntegers = false;
                        break;
                    }

                if (allIntegers)
                {
                    foreach (double d in checkList)
                        coefficients.Add((int)d);
                    return coefficients;
                }
                numberToMultiplyBy++;

            }
            throw new ArgumentException("Error: Could not determine integer values of coefficients");
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

        private DenseVector buildVector(IEquation unbalancedEquation)
        {
            ElementGroup products = (ElementGroup)unbalancedEquation.Products;
            ElementGroup lastMolecule = (ElementGroup)products[products.Count - 1];
            DenseVector vector = new DenseVector(unbalancedEquation.ListOfContents.Count);
            for (int i = 0; i < unbalancedEquation.ListOfContents.Count; i++)
            {
                vector[i] = lastMolecule.GetSubscriptCount(unbalancedEquation.ListOfContents[i]);
            }
            return vector;
        }

        private DenseMatrix buildMatrix(IEquation unbalancedEquation)
        {
            List<string> listOfSymbols = unbalancedEquation.ListOfContents;
            ElementGroup reactants = (ElementGroup)unbalancedEquation.Reactants;
            ElementGroup products = (ElementGroup)unbalancedEquation.Products;
            ElementGroup currentMolecule;
            DenseMatrix myMatrix = new DenseMatrix(listOfSymbols.Count, unbalancedEquation.MoleculeCount - 1);
            for (int column = 0; column < unbalancedEquation.MoleculeCount - 1; column++)
            {
                for (int row = 0; row < listOfSymbols.Count; row++)
                {
                    if (column < reactants.Count)
                    {
                        currentMolecule = (ElementGroup)reactants[column];
                        myMatrix[row, column] = currentMolecule.GetSubscriptCount(listOfSymbols[row]);
                    }
                    else
                    {
                        currentMolecule = (ElementGroup)products[column - reactants.Count];
                        myMatrix[row, column] = (currentMolecule.GetSubscriptCount(listOfSymbols[row]) * -1);
                    }
                }
            }
            return myMatrix;
        }

        private void checkForValidEquation(IEquation unbalancedEquation)
        {
            foreach (string s in unbalancedEquation.ListOfContents)
            {
                if (!unbalancedEquation.Products.ListOfContents.Contains(s) || !unbalancedEquation.Reactants.ListOfContents.Contains(s))
                    throw new ArgumentException("Error: the element or complex " + s + " could not be found on both sides of the equation");
            }
        }
    }
}
