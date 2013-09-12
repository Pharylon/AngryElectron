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
    static class MatrixSolver
    {
        public static List<double> Solve(DenseMatrix denseMatrix, DenseVector denseVector)
        {
            List<double> answers = new List<double>();
            var matrixAnswers = denseMatrix.QR().Solve(denseVector);
            foreach (double d in matrixAnswers)
                answers.Add(d);
            answers.Add(SolveForZ(denseMatrix, denseVector, matrixAnswers));
            return answers;
        }

        private static double SolveForZ(DenseMatrix unsolvedMatrix, DenseVector vector, MathNet.Numerics.LinearAlgebra.Generic.Vector<double> matrixAnswers)
        {
            for (int i = 0; i < vector.Count; i++)
            {
                if (vector[i] != 0) //to solve for the vector, we need to find a row where it wasn't zero.
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
            throw new ArgumentException("Balancer Error: Could not solve for 'z'");
        }

    }
}
