using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolynomialLib.DataContainers;

namespace PolynomialLib.ReportGeneration.Factory
{
    public class PdfFactory: ReportFactory
    {
        ReportModel _model;

        public PdfFactory(ReportModel model)
        {
            _model = model;
        }

        public PdfFactory(Polynomial f, Coordinates g, List<double> roots)
        {
            _model = new ReportModel(f, g, roots);
        }

        public override AbstractGenerator Create()
        {
            return new PdfGenerator(_model);
        }
    }
}
