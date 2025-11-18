using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolynomialLib.DataContainers;
using PolynomialLib.DataManager;
using PolynomialLib.MathematicalCore;
using PolynomialLib.ReportGeneration;

namespace PolynomialLib
{
    public class PolynomialFacade
    {
        // Implementation of the "Singleton" pattern
        private static PolynomialFacade? instance = null;

        private readonly ISerializer<EquationData> xmlManager = new XmlManager<EquationData>();

        public EquationData EquationData { get; private set; }
        public Polynomial FxFunction => EquationData.FxCoefficients;
        public Coordinates Coordinates => EquationData.GxCoordinates;
        public Polynomial GxFunction { get; private set; }
        public Polynomial FxGxFunction { get; private set; }
        public bool Solved { get; private set; }
        public List<double> Roots { get; private set; }

        private PolynomialFacade(EquationData equationData)
        {
            EquationData = equationData;
            Solved = false;
            Roots = new List<double>();
        }

        private PolynomialFacade()
        {
            var emptyPoly = new Polynomial(new List<double>());
            var emptyCoords = new Coordinates(new List<double>(), new List<double>());

            EquationData = new EquationData(emptyPoly, emptyCoords);
            Polynomial FxGxFunction = null;

            Solved = false;
            Roots = new List<double>();
        }

        /// <summary>
        /// Factory method for creating an empty object
        /// </summary>
        /// <returns>new facade object</returns>
        public static PolynomialFacade GetInstance()
        {
            if (instance == null)
            {
                instance = new PolynomialFacade();
            }
            return instance;
        }

        /// <summary>
        /// Factory method for creating an object with given data
        /// </summary>
        /// <returns>new facade object</returns>
        public static PolynomialFacade GetInstance(EquationData equationData)
        {
            if (instance == null)
            {
                instance = new PolynomialFacade(equationData);
            }
            else
            {
                instance.EquationData = equationData;
            }
            return instance;
        }

        /// <summary>
        /// Find polynom which graph has all points, given to the method
        /// </summary>
        /// <returns></returns>
        private Polynomial FindLagrangePolynomial()
        {
            LagrangePolynomialCalculator lagrangeCalculator = new(Coordinates);
            return lagrangeCalculator.Calculate();
        }


        /// <summary>
        /// Finds equation roots
        /// </summary>
        /// <returns></returns>
        public List<double> Solve(IRootFinder solvingMethod = null, IList<double> initialGuesses = null) // Strategy Pattern
        {
            if(solvingMethod == null)
            {
                solvingMethod = new TangentMethodCalculator(initialGuesses);
            }
            // calculate g(x)
            GxFunction = FindLagrangePolynomial();

            // fx - gx
            FxGxFunction = FxFunction - GxFunction;

            // find roots
            Roots = solvingMethod.FindRoots(FxGxFunction);
            Solved = true;

            return Roots;
        }

        /// <summary>
        /// Return equation roots
        /// </summary>
        /// <returns></returns>
        public string Results()
        {
            if(Solved)
            {
                if(Roots.Count == 0)
                {
                    return $"Equation doesn't have any roots!";
                }
                else if(Roots.Count == 1)
                {
                    return $"Equation root: {string.Join(",", Roots)}";
                }
                return $"Equation roots: {string.Join(",", Roots)}";
            }
            else
            {
                return "Equation wasn't solved yet";
            }
        }

        /// <summary>
        /// Read information form XML file
        /// </summary>
        /// <param name="fullPpath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public EquationData ReadFromXml(string fullPpath)
        {
            if (fullPpath == null)
            {
                throw new ArgumentNullException("Path can't be null!");
            }
            return xmlManager.Read(fullPpath);
        }

        /// <summary>
        /// Write information to XML file
        /// </summary>
        /// <param name="fullPpath"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void WriteToXml(string fullPpath)
        {
            if (fullPpath == null)
            {
                throw new ArgumentNullException("Path can't be null!");
            }
            xmlManager.Write(EquationData, fullPpath);
        }

        /// <summary>
        /// Generate report with given data and calculated equation roots
        /// </summary>
        /// <param name="generatingMethod"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="autoGeneratedPath"></param>
        /// <returns></returns>
        public string GenerateReport(AbstractGenerator generatingMethod, string path = null, string fileName = null, bool autoGeneratedPath = true) // Strategy Pattern
        {
            return generatingMethod.Generate(path, fileName, autoGeneratedPath);
        }

        /// <summary>
        /// Clears the data 
        /// </summary>
        public void DoNew()
        {
            GetInstance(new EquationData(new Polynomial([]), new Coordinates([], [])));
        }

        /// <summary>
        /// Clears the data 
        /// </summary>
        public void AssignNewEquation(EquationData equation)
        {
            EquationData = equation;
            GxFunction = null;
            Roots = new List<double>();
            Solved = false;
        }

        /// <summary>
        /// Add new coeficient to polynominal
        /// </summary>
        public void AddNewCoefficientToFx(double coefficient = 0) 
        {
            FxFunction.Coefficients.Add(coefficient);
            Solved = false;
        }

        /// <summary>
        /// Delete last element form polynomial
        /// </summary>
        public void RemoveFirstCoefficientFromFx() 
        {
            FxFunction.Coefficients.RemoveAt(FxFunction.Coefficients.Count-1);
            Solved = false;
        }

        /// <summary>
        /// Adds new pait of coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddNewCoordinates(double x = 0, double y = 0)
        {
            Coordinates.Add(x, y);
            Solved = false;
        }

        /// <summary>
        /// Remove last coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void RemoveLastCoordinate()
        {
            Coordinates.Remove();
            Solved = false;
        }
        
        /// <summary>
        /// Deletes file from given path
        /// </summary>
        /// <param name="path">Path to file</param>
        public void DeleteFile(string path)
        {
            PathManager.Delete(path);

        }

        /// <summary>
        /// Ranges to adjast graph generation
        /// </summary>
        /// <param name="xMargin"></param>
        /// <param name="yMargin"></param>
        /// <returns></returns>
        public (double, double, double, double) Ranges(double xMargin = 2, double yMargin = 3)
        {
            Solve();
            double xMin = Roots.Count > 0 ? Roots.Min() - xMargin : -xMargin;
            double xMax = Roots.Count > 0 ? Roots.Max() + xMargin : xMargin;
            double yFrom = FxGxFunction.Evaluate(xMin);
            double yTo = FxGxFunction.Evaluate(xMax);
            double yMin = Math.Min(Math.Min(yFrom, yTo), 0) - yMargin;
            double yMax = Math.Max(Math.Max(yFrom, yTo), 0) + yMargin;

            return (xMin, xMax, yMin, yMax);
        }
    }
}
