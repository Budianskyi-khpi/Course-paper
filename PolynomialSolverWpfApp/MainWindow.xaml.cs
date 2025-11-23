using System.Windows;
using System.Windows.Data;
using PolynomialLib;
using PolynomialLib.DataContainers;
using PolynomialLib.ReportGeneration;
using PolynomialLib.ReportGeneration.Factory;

namespace PolynomialSolverWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PolynomialFacade facade = PolynomialFacade.GetInstance();
        public MainWindow()
        {
            InitializeComponent();
            InitTables();
        }

        private void MenuItemNew_Click(object sender, RoutedEventArgs e)
        {
            facade.DoNew();
            TextBoxResults.Clear();
            InitTables();
        }

        private void InitTables()
        {
            InitFTable();
            InitGTable();
        }

        private void InitFTable()
        {
            DataGridF.ItemsSource = null;
            var coefficientsList = facade.FxFunction.Coefficients;

            var displayList = coefficientsList.Select(val => new EditableCoefficient { Value = val })
                                              .ToList(); 

            DataGridF.ItemsSource = displayList;

            ColumnCoefficient.Binding = new Binding("Value");

            DrawGraph();
        }

        private void SyncFTableToModel()
        {
            var displayList = DataGridF.ItemsSource as List<EditableCoefficient>;
            if (displayList == null) return;

            var modelList = facade.FxFunction.Coefficients;

            modelList.Clear();

            foreach (var item in displayList)
            {
                modelList.Add(item.Value);
            }
        }


        private void InitGTable()
        {
            DataGridG.ItemsSource = null;
            // Bind DataGridG to the list of pairs:
            DataGridG.ItemsSource = facade.Coordinates.XYCoordinates;

            // Specify which columns are bound to which properties:
            ColumnX.Binding = new Binding("X");
            ColumnY.Binding = new Binding("Y");

            DrawGraph();
        }

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            // Create an open file dialog and set its properties:
            Microsoft.Win32.OpenFileDialog dlg = new();
            dlg.InitialDirectory = "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    var equation = facade.ReadFromXml(dlg.FileName);
                    PolynomialFacade.GetInstance(equation);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error reading from file");
                }
                TextBoxResults.Clear();
                InitTables();
            }
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            SyncFTableToModel();
            // Confirm changes in the tables:
            DataGridF.CommitEdit();
            DataGridG.CommitEdit();

            // Create a save file dialog and set its properties:
            Microsoft.Win32.SaveFileDialog dlg = new();
            dlg.InitialDirectory = "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\AutoXmlSave\\";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    
                    facade.WriteToXml(dlg.FileName);
                    MessageBox.Show("File saved");
                }
                catch (Exception)
                {
                    MessageBox.Show("Error writing to file");
                }
            }
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Budianskyis semester project\n\nGroup: KN-224a.e\n\nSubject: Object oriented programing", "About");
        }

        private void ButtonAddF_Click(object sender, RoutedEventArgs e)
        {
            SyncFTableToModel();
            // Confirm changes in the table:
            DataGridF.CommitEdit();
            // Add a new row:
            facade.AddNewCoefficientToFx();
            TextBoxResults.Clear();
            InitFTable();
        }

        private void ButtonRemoveF_Click(object sender, RoutedEventArgs e)
        {
            SyncFTableToModel();
            // Confirm changes in the table:
            DataGridF.CommitEdit();
            // Add a new row:
            try
            {
                facade.RemoveFirstCoefficientFromFx();
            }
            catch
            {
                MessageBox.Show("Can't execute this operation!");
            }
            TextBoxResults.Clear();
            InitFTable();
        }

        private void ButtonAddG_Click(object sender, RoutedEventArgs e)
        {
            // Confirm changes in the table:
            DataGridG.CommitEdit();
            // Add a new row:
            facade.AddNewCoordinates();
            TextBoxResults.Clear();
            InitGTable();
        }

        private void ButtonRemoveG_Click(object sender, RoutedEventArgs e)
        {
            // Confirm changes in the table:
            DataGridG.CommitEdit();

            // Remove the last row:
            try
            {
                facade.RemoveLastCoordinate();
            }
            catch
            {
                MessageBox.Show("Can't execute this operation!");
            }
            TextBoxResults.Clear();
            InitGTable();
        }

        private void ButtonSolve_Click(object sender, RoutedEventArgs e)
        {
            SyncFTableToModel();
            facade.Solve();
            string text = facade.Results();
            TextBoxResults.Text = text.Replace("\t", Environment.NewLine);

            DrawGraph();
        }

        private void ReportHtml_Click(object sender, RoutedEventArgs e)
        {
            string filePath;
            AbstractGenerator htmlGenerator;
            ReportFactory factory;

            SyncFTableToModel();
            if (!facade.Solved)
            {
                MessageBox.Show("You need to solve the equation first!");
                return;
            }

            string saveDirectory = "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Reports\\";
            try
            {
                ReportModel reportModel = new(facade.EquationData.FxCoefficients, facade.Coordinates, facade.Roots);
                factory = new HtmlFactory(reportModel);
                htmlGenerator = factory.Create();
                filePath = facade.GenerateReport(htmlGenerator);
                MessageBox.Show("File saved");
            }
            catch (Exception)
            {
                MessageBox.Show("Error writing to file");
                return;
            }

            WindowShowReport windowShowReport = new(filePath, htmlGenerator);
            windowShowReport.ShowDialog();
        }

        private void ReportPdf_Click(object sender, RoutedEventArgs e) 
        {
            string filePath;
            AbstractGenerator pdfGenerator;
            ReportFactory factory;

            SyncFTableToModel();
            if (!facade.Solved)
            {
                MessageBox.Show("You need to solve the equation first!");
                return;
            }

            string saveDirectory = "C:\\KhPI\\OOP\\Course paper\\PolynomSolver\\PolynomLib\\DataManager\\Data\\Reports\\";
            try
            {
                ReportModel reportModel = new(facade.EquationData.FxCoefficients, facade.Coordinates, facade.Roots);
                factory = new PdfFactory(reportModel);
                pdfGenerator = factory.Create();
                filePath = facade.GenerateReport(pdfGenerator);
                MessageBox.Show("File saved");
            }
            catch (Exception)
            {
                MessageBox.Show("Error writing to file");
                return;
            }

            WindowShowReport windowShowReport = new(filePath, pdfGenerator);
            windowShowReport.ShowDialog();
        }

        /// <summary>
        /// Redraw the graph
        /// </summary>
        private void DrawGraph()
        {
            try
            {
                if ( facade.Solved && CanvasGraph != null)
                {
                    (double xMin, double xMax, double yMin, double yMax) = facade.Ranges(xMargin: 2, yMargin: 2);
                    new GraphBuilder().DrawGraph(CanvasGraph, new List<Polynomial>() { facade.FxFunction, facade.GxFunction, facade.FxGxFunction }, 
                        xMin, xMax, yMin, yMax);
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong! Can't build the graph.");
            }
        }

        /// <summary>
        /// Redraw graph when canvas size was changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasGraph_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawGraph();
        }
    }
}