using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolynomialLib.DataContainers;

namespace PolynomialLib.MathematicalCore
{
    internal class LagrangePolynomialCalculator
    {
        public Coordinates Points { get; }

        public LagrangePolynomialCalculator(Coordinates coordinates)
        {
            Points = coordinates;
        }

        /// <summary>
        /// Main method
        /// </summary>
        /// <returns>polynom</returns>
        public Polynomial Calculate()
        {
            List<Polynomial> basisPolynomials = new List<Polynomial>();
            for (int i = 0; i < Points.XYCoordinates.Count; i++)
            {
                basisPolynomials.Add(GetBasisPolynomial(i));
            }
            return CalculateGeneralFormula(basisPolynomials);
        }

        /// <summary>
        /// Calculates basic polynom(l_j(x))
        /// </summary>
        /// <param name="currentIndex"></param>
        /// <returns>polynom</returns>
        private Polynomial GetBasisPolynomial(int currentIndex)
        {
            Polynomial numeratorPoly = new Polynomial(new List<double> { 1.0 });

            for (int i = 0; i < Points.XYCoordinates.Count; i++)
            {
                if (i != currentIndex)
                {
                    double x_i = Points.XYCoordinates[i].X;

                    // (x - x_i)
                    Polynomial termPolynom = new Polynomial(new List<double> { -x_i, 1.0 });

                    numeratorPoly = numeratorPoly * termPolynom;
                }
            }

            double x_j = Points.XYCoordinates[currentIndex].X;
            double denominatorValue = numeratorPoly.Evaluate(x_j);

            return numeratorPoly * (1.0 / denominatorValue);
        }

        /// <summary>
        /// Calculate final formula
        /// </summary>
        private Polynomial CalculateGeneralFormula(IList<Polynomial> basisPolynomials)
        {
            Polynomial generalFunctionPoly = new Polynomial(new List<double> { 0.0 });

            for (int i = 0; i < basisPolynomials.Count; i++)
            {
                Polynomial l_j_poly = basisPolynomials[i];
                double y_j = Points.XYCoordinates[i].Y;

                // g(x) = g(x) + (y_j * l_j(x))
                generalFunctionPoly += y_j * l_j_poly;
            }

            return generalFunctionPoly;
        }
    }
}
