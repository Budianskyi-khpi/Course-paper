using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolynomialLib;
using PolynomialLib.DataContainers;
using PolynomialLib.MathematicalCore;

namespace PolynomeSolverConsoleApp
{
    internal static partial class ConsoleTests
    {
        internal static void Solve(PolynomialFacade solver)
        {
            Console.Write("Given data: ");
            Console.WriteLine($"f_x = {solver.FxFunction}");
            //Console.WriteLine($"Given points:\n\tX = {string.Join(", ", solver.Coordinates.XYCoordinates.X)}\n\tY = {string.Join(", ", solver.Coordinates.XYCoordinates.Y)}");
            Console.WriteLine($"Given points: {string.Join(", ", solver.Coordinates.XYCoordinates)}");

            IRootFinder solvingMethod = new TangentMethodCalculator();
            var roots = solver.Solve(solvingMethod);
            Console.WriteLine($"Roots of equation: {string.Join(", ", roots)}");
        }
    }
}
