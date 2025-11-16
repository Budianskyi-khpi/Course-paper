using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolynomialLib;
using PolynomialLib.ReportGeneration;

namespace PolynomeSolverConsoleApp
{
    internal static partial class ConsoleTests
    {
        internal static void PdfGenerate(PolynomialFacade facade, ReportModel reportModel)
        {
            PdfGenerator generator = new PdfGenerator(reportModel);
            var path = facade.GenerateReport(generator);
            Console.WriteLine($"Report was generated on this path: {path}");
        }

        internal static void HtmlGenerate(PolynomialFacade facade, ReportModel reportModel)
        {
            HtmlGenerator generator = new HtmlGenerator(reportModel);
            var path = facade.GenerateReport(generator);
            Console.WriteLine($"Report was generated on this path: {path}");
        }
    }
}
