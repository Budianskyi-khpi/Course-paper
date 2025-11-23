using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomialLib.ReportGeneration.Factory
{
    public abstract class ReportFactory
    {
        public abstract AbstractGenerator Create();
    }
}
