namespace PolynomialLib.DataContainers
{
    public class Coordinates
    {
        public List<double> XCoordinates { get; set; }
        public List<double> YCoordinates { get; set; }

        public Coordinates(IList<double> x, IList<double> y)
        {
            if (x.Count == y.Count)
            {
                XCoordinates = x.ToList();
                YCoordinates = y.ToList();
            }
            else
            {
                throw new ArgumentException("Expected equal count of elements in X coordinates array and Y coordinates array");
            }
        }
        private Coordinates()
        {
            XCoordinates = new List<double>();
            YCoordinates = new List<double>();
        }

        public void Add(double x, double y)
        {
            XCoordinates.Add(x);
            YCoordinates.Add(y);
        }

        public override string ToString()
        {
            string result = "";

            for(int i = 0; i < XCoordinates.Count; i++)
            {
                result += $"({XCoordinates[i].ToString()}, {YCoordinates[i].ToString()})";
                if(i != XCoordinates.Count - 1)
                {
                    result += ", ";
                }
            }

            return result;
        }
    }
}
