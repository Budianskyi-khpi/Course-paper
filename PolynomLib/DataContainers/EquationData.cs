using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PolynomialLib.DataContainers
{
    [XmlInclude(typeof(Coordinates))]
    [XmlInclude(typeof(Polynomial))]
    public class EquationData
    {
        public Polynomial FxCoefficients { get; set; }
        public Coordinates GxCoordinates { get; set; }

        public EquationData(Polynomial coeficients, Coordinates coordinates)
        {
            FxCoefficients = coeficients;
            GxCoordinates = coordinates;
        }

        private EquationData()
        {

        }

        public override string ToString()
        {
            return $"f_x = {FxCoefficients}, g_x = {GxCoordinates}";
        }
    }
}
