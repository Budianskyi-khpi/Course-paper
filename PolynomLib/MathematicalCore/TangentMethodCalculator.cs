using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolynomialLib.DataContainers;
using System.Linq;

namespace PolynomialLib.MathematicalCore
{
    public class TangentMethodCalculator: IRootFinder
    {
        IList<double> initialGuesses;

        public TangentMethodCalculator(IList<double> initialGuesses = null)
        {
            if (initialGuesses == null)
            {
                initialGuesses = new List<double> { -10, -5, 0, 5, 10 };
            }
            this.initialGuesses = initialGuesses;
        }

        /// <summary>
        /// Find All roots
        /// </summary>
        /// <param name="polynomial"></param>
        /// <returns></returns>
        public List<double>? FindRoots(Polynomial polynomial)
        {
            if (polynomial.Coefficients.All(x => x == 0))
            {
                return null;
            }

            List<double> roots = new List<double>();
            foreach (var initialGuess in initialGuesses)
            {
                try
                {
                    double root = FindRoot(polynomial, initialGuess);
                    if (!roots.Any(r => Math.Abs(r - root) < 1e-4))
                        roots.Add(root);
                }
                catch
                {

                }
            }
            return roots;
        }

        /// <summary>
        /// Find root closest to initial guess
        /// </summary>
        /// <param name="polynomial"></param>
        /// <param name="initialGuess"></param>
        /// <param name="maxIterations"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private double FindRoot(Polynomial polynomial, double initialGuess = 0, int maxIterations = 1000, double tolerance = 1e-7)
        {
            Polynomial derivativePolynomial = DerivativeCalculator.Differentiate(polynomial);
            double currentAssumption = initialGuess;

            for (int i = 0; i < maxIterations; i++)
            {
                double h_value = polynomial.Evaluate(currentAssumption);
                double h_derivative_value = derivativePolynomial.Evaluate(currentAssumption);

                if (Math.Abs(h_derivative_value) < 1e-20)
                {
                    throw new Exception("Derivative is equal to zero! Try another initial guess.");
                }

                // x_n+1 = x_n - h(x_n) / h'(x_n)
                double nextAssumption = currentAssumption - (h_value / h_derivative_value);

                if (Math.Abs(nextAssumption - currentAssumption) < tolerance)
                {
                    return nextAssumption;
                }

                currentAssumption = nextAssumption;
            }

            throw new Exception("Tangent method wasn't able to find roots with" + maxIterations + " iterations");
        }
    }
}
