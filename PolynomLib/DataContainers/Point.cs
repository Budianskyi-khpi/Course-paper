using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialLib.DataContainers
{
    /// <summary>
    /// Pair of numbers X and Y
    /// </summary>
    public class Point
    {
        // Attributes necessary for managing the XML document
        // during future serialization
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double X { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        private Point()
        {
            
        }

        public override string ToString()
        {
            return $"({X.ToString()}, {Y.ToString()})";
        }
    }
}
