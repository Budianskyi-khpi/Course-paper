using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace PolynomialLib.ReportGeneration
{
    public class EquationReportDocument : IDocument
    {
        private readonly ReportModel _model;

        public EquationReportDocument(ReportModel model)
        {
            _model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(50);

                page.Header().AlignCenter().Text("Report").SemiBold().FontSize(20);

                // Вміст
                page.Content().Column(column =>
                {
                    // f(x) block
                    column.Item().Text("Input data:").Bold().FontSize(14);
                    //string f_text = "f(x) = " + string.Join(" + ", _model.F_Coefficients.Coefficients.Select((coef, i) => $"{coef}*x^{i}"));
                    string f_text = "f(x) = " + _model.F_Coefficients;
                    column.Item().Text(f_text);

                    // g(x) block
                    column.Item().Text("g(x) given by points:").Bold();
                    for (int i = 0; i < _model.G_Coordinates.XYCoordinates.Count; i++)
                    {
                        column.Item().Text($"  ({_model.G_Coordinates.XYCoordinates[i].X}, {_model.G_Coordinates.XYCoordinates[i].Y})");
                    }

                    // Results
                    //column.Item().Spacing(20); 
                    column.Item().Text("Results:").Bold().FontSize(16);
                    column.Item().Text("Finded roots:");
                    if (_model.Roots != null)
                    {
                        foreach (var root in _model.Roots)
                        {
                            column.Item().Text($"  x = {root:F5}");
                        }
                    }
                    else
                    {
                        column.Item().Text($"  There is infinit number of roots!");
                    }
                    
                });

                page.Footer().AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Page ");
                        text.CurrentPageNumber();
                    });
            });
        }
    }
}
