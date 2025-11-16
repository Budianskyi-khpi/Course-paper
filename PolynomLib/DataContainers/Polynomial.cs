using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialLib.DataContainers
{
    public class Polynomial
    {
        public List<double> Coefficients { get; private set; }

        public Polynomial(IList<double> coefficients)
        {
            Coefficients = coefficients.ToList();
        }

        private Polynomial()
        {
            Coefficients = new List<double>();
        }

        public double Evaluate(double x)
        {
            double result = 0.0;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                result += Coefficients[i] * Math.Pow(x, i);
            }
            return result;
        }

        public static Polynomial operator +(Polynomial num1, Polynomial num2)
        {
            int maxCount = Math.Max(num1.Coefficients.Count, num2.Coefficients.Count);
            List<double> resultCoeffs = new List<double>(maxCount);

            for (int i = 0; i < maxCount; i++)
            {
                double c1 = i < num1.Coefficients.Count ? num1.Coefficients[i] : 0;
                double c2 = i < num2.Coefficients.Count ? num2.Coefficients[i] : 0;
                resultCoeffs.Add(c1 + c2);
            }
            return new Polynomial(resultCoeffs);
        }

        public static Polynomial operator -(Polynomial num1, Polynomial num2)
        {
            int maxCount = Math.Max(num1.Coefficients.Count, num2.Coefficients.Count);
            List<double> resultCoeffs = new List<double>(maxCount);

            for (int i = 0; i < maxCount; i++)
            {
                double c1 = i < num1.Coefficients.Count ? num1.Coefficients[i] : 0;
                double c2 = i < num2.Coefficients.Count ? num2.Coefficients[i] : 0;
                resultCoeffs.Add(c1 - c2);
            }
            return new Polynomial(resultCoeffs);
        }

        public static Polynomial operator *(Polynomial num1, Polynomial num2)
        {
            int newCount = num1.Coefficients.Count + num2.Coefficients.Count - 1;
            List<double> resultCoeffs = new List<double>(newCount);

            for (int k = 0; k < newCount; k++)
                resultCoeffs.Add(0.0);

            for (int i = 0; i < num1.Coefficients.Count; i++)
            {
                for (int j = 0; j < num2.Coefficients.Count; j++)
                {
                    resultCoeffs[i + j] += num1.Coefficients[i] * num2.Coefficients[j];
                }
            }
            return new Polynomial(resultCoeffs);
        }

        public static Polynomial operator *(Polynomial poly, double scalar)
        {
            List<double> resultCoeffs = new List<double>(poly.Coefficients.Count);
            for (int i = 0; i < poly.Coefficients.Count; i++)
            {
                resultCoeffs.Add(poly.Coefficients[i] * scalar);
            }
            return new Polynomial(resultCoeffs);
        }

        public static Polynomial operator *(double scalar, Polynomial poly)
        {
            return poly * scalar;
        }

        public override string ToString()
        {
            List<string> parts = new List<string>();

            for (int i = Coefficients.Count - 1; i >= 0; i--)
            {
                double coeff = Coefficients[i];
                string part;

                if (coeff == 0)
                    continue;

                if (i == 0)
                {
                    part = $"{coeff}";
                }
                else if (i == 1)
                {
                    part = $"{coeff}*x";
                }
                else
                {
                    part = $"{coeff}*x^{i}";
                }

                parts.Add(part);
            }

            if (parts.Count == 0)
                return "0";

            return string.Join(" + ", parts);
        }
    }
}
