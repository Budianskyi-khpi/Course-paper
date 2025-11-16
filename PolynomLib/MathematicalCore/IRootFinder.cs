using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolynomialLib.DataContainers;

namespace PolynomialLib.MathematicalCore
{
    public interface IRootFinder
    {
        List<double> FindRoots(Polynomial polynomial);
    }
}
