using System.Runtime.InteropServices.Marshalling;
using PolynomialLib;
using PolynomialLib.DataContainers;
using PolynomialLib.ReportGeneration;

namespace PolynomeSolverConsoleApp
{
    using static ConsoleTests;
    internal class Program
    {
        static void Main(string[] args)
        {
            // Test 1: "Лінійний" (Проста перевірка)
            // f(x) = 2x + 1
            // g(x): Дві точки (0, 0) та (1, 1)
            Console.WriteLine("============== Test 1 ==============");
            EquationData equation = new(new Polynomial([1, 2]), new Coordinates([0, 1], [0, 1]));
            var fasade = PolynomialFacade.GetInstance(equation);
            Solve(fasade);
            Write(fasade, "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Default\\example1.xml");
            Read(fasade, "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Default\\example1.xml");
            // Pdf report generation
            PdfGenerate(fasade, new ReportModel(equation, fasade.Roots));
            //Html report generation
            HtmlGenerate(fasade, new ReportModel(equation, fasade.Roots));

            // Test 2: "Парабола і лінія" (Два корені)
            // f(x) = x^2
            // Дві точки (0, 4) та (2, 4)
            Console.WriteLine("\n============== Test 2 ==============");
            equation = new(new Polynomial([0, 0, 1]), new Coordinates([0, 2], [4, 4]));
            fasade = PolynomialFacade.GetInstance(equation);
            Solve(fasade);
            Write(fasade, "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Default\\example2.xml");
            Read(fasade, "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Default\\example2.xml");
            // Pdf report generation
            PdfGenerate(fasade, new ReportModel(equation, fasade.Roots));
            //Html report generation
            HtmlGenerate(fasade, new ReportModel(equation, fasade.Roots));

            // Test 3: "Скорочення степенів" (Хитрий випадок)
            // f(x) = x^2 + 3x + 5
            // Три точки: (0, 1), (1, 2), (2, 5)
            Console.WriteLine("\n============== Test 3 ==============");
            equation = new(new Polynomial([5, 3, 1]), new Coordinates([0, 1, 2], [1, 2, 5]));
            fasade = PolynomialFacade.GetInstance(equation);
            Solve(fasade);
            Write(fasade, "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Default\\example3.xml");
            Read(fasade, "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Default\\example3.xml");
            // Pdf report generation
            PdfGenerate(fasade, new ReportModel(equation, fasade.Roots));
            //Html report generation
            HtmlGenerate(fasade, new ReportModel(equation, fasade.Roots));

            // Test 4: "Немає коренів"
            // f(x) = x^2 + 10
            // Дві точки (0, 1) та (1, 1)
            Console.WriteLine("\n============== Test 4 ==============");
            equation = new(new Polynomial([10, 0, 1]), new Coordinates([0, 1], [1, 1]));
            fasade = PolynomialFacade.GetInstance(equation);
            Solve(fasade);
            Write(fasade, "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Default\\example4.xml");
            Read(fasade, "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Default\\example4.xml");
            // Pdf report generation
            PdfGenerate(fasade, new ReportModel(equation, fasade.Roots));
            //Html report generation
            HtmlGenerate(fasade, new ReportModel(equation, fasade.Roots));

            // Test 5: Дійсний тест з дробовими коренями
            // f(x) = x^2 - 10
            // Три точки (1, 3), (2, 8), (3, 6)
            Console.WriteLine("\n============== Test 5 ==============");
            equation = new(new Polynomial([-10, 0, 1]), new Coordinates([1, 2, 3], [3, 8, 6]));
            fasade = PolynomialFacade.GetInstance(equation);
            Solve(fasade);
            Write(fasade, "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Default\\example5.xml");
            Read(fasade, "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Default\\example5.xml");
            // Pdf report generation
            PdfGenerate(fasade, new ReportModel(equation, fasade.Roots));
            //Html report generation
            HtmlGenerate(fasade, new ReportModel(equation, fasade.Roots));

            // Test 6: "Дотик" (Один корінь у параболи)
            // f(x) = x^2 - 4x + 4
            // Дві точки (0, 0), (1, 0)
            Console.WriteLine("\n============== Test 6 ==============");
            equation = new(new Polynomial([4, -4, 1]), new Coordinates([0, 1], [0, 0]));
            fasade = PolynomialFacade.GetInstance(equation);
            Solve(fasade);
            Write(fasade, "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Default\\example6.xml");
            Read(fasade, "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Default\\example6.xml");
            // Pdf report generation
            PdfGenerate(fasade, new ReportModel(equation, fasade.Roots));
            //Html report generation
            HtmlGenerate(fasade, new ReportModel(equation, fasade.Roots));
        }
    }
}
