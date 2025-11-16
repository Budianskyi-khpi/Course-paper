using PolynomialLib.DataContainers;

namespace PolynomialLib.ReportGeneration
{
    public class ReportModel
    {
        public Polynomial F_Coefficients { get; set; }
        public Coordinates G_Coordinates { get; set; }
        public List<double> Roots { get; set; }

        public ReportModel(Polynomial f_function, Coordinates g_coordinates, List<double> roots)
        {
            F_Coefficients = f_function;
            G_Coordinates = g_coordinates;
            Roots = roots;
        }

        public ReportModel(EquationData data, List<double> roots)
        {
            F_Coefficients = data.FxCoefficients;
            G_Coordinates = data.GxCoordinates;
            Roots = roots;
        }
    }
}
