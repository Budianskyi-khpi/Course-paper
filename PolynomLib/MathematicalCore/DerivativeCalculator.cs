using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolynomialLib.DataContainers;

namespace PolynomialLib.MathematicalCore
{
    internal static class DerivativeCalculator
    {
        /// <summary>
        /// Calculate derivative from given polynomial
        /// </summary>
        /// <param name="polinomial"></param>
        /// <returns></returns>
        public static Polynomial Differentiate(Polynomial polinomial)
        {
            if (polinomial.Coefficients.Count < 2)
            {
                return new Polynomial(new List<double> { 0.0 });
            }

            List<double> derivativeCoeffs = new List<double>(polinomial.Coefficients.Count - 1);

            for (int i = 1; i < polinomial.Coefficients.Count; i++)
            {
                derivativeCoeffs.Add(polinomial.Coefficients[i] * i);
            }

            return new Polynomial(derivativeCoeffs);
        }
    }
}
