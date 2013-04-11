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
        public IEquation Balance(IEquation unbalancedEquation)
        {
            List<string> listOfSymbols = generateListOfSymobols(unbalancedEquation);
            checkForValidEquation(listOfSymbols, unbalancedEquation);
            DenseMatrix subscriptMatrix = buildMatrix(listOfSymbols, unbalancedEquation);
            DenseVector subscriptVector = buildVector(listOfSymbols, unbalancedEquation);
            List<int> answers = solveMatrix(subscriptMatrix, subscriptVector);
            ChemicalEquation myEquation = addCoefficients(answers, unbalancedEquation);
            return myEquation;
        }

        private ChemicalEquation addCoefficients(List<int> answers, IEquation unbalancedEquation)
        {
            throw new NotImplementedException();
        }

        private List<int> solveMatrix(DenseMatrix subscriptMatrix, DenseVector subscriptVector)
        {
            throw new NotImplementedException();
        }

        private DenseVector buildVector(List<string> listOfSymbols, IEquation unbalancedEquation)
        {
            throw new NotImplementedException();
        }

        private DenseMatrix buildMatrix(List<string> listOfSymbols, IEquation unbalancedEquation)
        {
            throw new NotImplementedException();
        }

        public void checkForValidEquation(List<string> listOfSymbols, IEquation unbalancedEquation)
        {
            foreach (string symbol in listOfSymbols)
                if (!unbalancedEquation.Products.ParsableSymbols.Contains(symbol) || unbalancedEquation.Reactants.ParsableSymbols.Contains(symbol))
                    throw new ArgumentException("Equation contains an element or complex on one side of the equation, but not on the other");       
        }

        public List<string> generateListOfSymobols(IEquation unbalancedEquation)
        {
            List<string> uniqueSymbols = new List<string>();
            foreach (string symbol in unbalancedEquation.ParsableSymbols)
                if (!uniqueSymbols.Contains(symbol))
                    uniqueSymbols.Add(symbol);
            uniqueSymbols.Remove("+");
            uniqueSymbols.Remove("->");
            return uniqueSymbols;
        }
    }
}
