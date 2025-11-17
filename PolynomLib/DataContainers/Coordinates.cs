namespace PolynomialLib.DataContainers
{
    public class Coordinates
    {
        public List<Point> XYCoordinates { get; set; } = new();

        public Coordinates(IList<double> x, IList<double> y)
        {
            if (x.Count == y.Count)
            {
                for(int i = 0; i < x.Count; i++)
                {
                    Add(x[i], y[i]);
                }
            }
            else
            {
                throw new ArgumentException("Expected equal count of elements in X coordinates array and Y coordinates array");
            }
        }

        private Coordinates()
        {
            XYCoordinates = new List<Point>();
        }

        public void Add(double x, double y)
        {
            XYCoordinates.Add(new Point(x, y));
        }

        public void Remove()
        {
            XYCoordinates.RemoveAt(XYCoordinates.Count - 1);
        }

        public override string ToString()
        {
            string result = "";

            for(int i = 0; i < XYCoordinates.Count; i++)
            {
                result += XYCoordinates[i].ToString();
                if(i != XYCoordinates.Count - 1)
                {
                    result += ", ";
                }
            }

            return result;
        }
    }
}
