using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolynomialLib;
using PolynomialLib.DataContainers;

namespace PolynomeSolverConsoleApp
{
    internal static partial class ConsoleTests
    {
        public static void Write(PolynomialFacade facade, string path)
        {
            facade.WriteToXml(path);
            Console.WriteLine($"Equation data was written to: {path}");
        }

        public static void Read(PolynomialFacade facade, string path)
        {
            EquationData equation = facade.ReadFromXml(path);
            Console.WriteLine("Readed data:");
            Console.WriteLine(equation.ToString());
        }
    }
}
