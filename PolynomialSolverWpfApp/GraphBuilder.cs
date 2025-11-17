using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PolynomialLib.DataContainers;

namespace PolynomialSolverWpfApp
{
    internal class GraphBuilder
    {
        private Canvas canvasGraph = new(); // Canvas on which the drawing is done

        private double width;  // width of the display area
        private double height; // height of the display area
        private double xScale; // scale (number of points per unit) along the x-axis
        private double yScale; // scale (number of points per unit) along the y-axis
        private double x0;     // x-coordinate of the origin
        private double y0;     // y-coordinate of the origin

        public void DrawGraph( Canvas canvasGraph, Polynomial polynomial,
            double xMin = -5, double xMax = 5, double yMin = -5, double yMax = 5)
        {
            this.canvasGraph = canvasGraph;
            width = this.canvasGraph.ActualWidth;
            height = this.canvasGraph.ActualHeight;
            xScale = width / (xMax - xMin);
            yScale = height / (yMax - yMin);
            x0 = -xMin * xScale;
            y0 = yMax * yScale;

            this.canvasGraph.Children.Clear();
            DrawXGrid(xMin, xMax);
            DrawYGrid(yMin, yMax);
            DrawAxes();

            DrawPolynomial(Brushes.Red, polynomial);
                
        }

        private void DrawXGrid(double xMin, double xMax)
        {
            double xStep = 1; // Grid step
            while (xStep * xScale < 25)
            {
                xStep *= 10;
            }
            while (xStep * xScale > 250)
            {
                xStep /= 10;
            }
            for (double dx = xStep; dx < xMax; dx += xStep)
            {
                double x = x0 + dx * xScale;
                AddLine(Brushes.LightGray, x, 0, x, height);
                AddText(string.Format("{0:0.###}", dx), x + 2, y0 + 2);
            }
            for (double dx = -xStep; dx >= xMin; dx -= xStep)
            {
                double x = x0 + dx * xScale;
                AddLine(Brushes.LightGray, x, 0, x, height);
                AddText(string.Format("{0:0.###}", dx), x + 2, y0 + 2);
            }
        }

        private void DrawYGrid(double yMin, double yMax)
        {
            double yStep = 1;  // Grid step
            while (yStep * yScale < 20)
            {
                yStep *= 10;
            }
            while (yStep * yScale > 200)
            {
                yStep /= 10;
            }
            for (double dy = yStep; dy < yMax; dy += yStep)
            {
                double y = y0 - dy * yScale;
                AddLine(Brushes.LightGray, 0, y, width, y);
                AddText(string.Format("{0:0.###}", dy), x0 + 2, y - 2);
            }
            for (double dy = -yStep; dy > yMin; dy -= yStep)
            {
                double y = y0 - dy * yScale;
                AddLine(Brushes.LightGray, 0, y, width, y);
                AddText(string.Format("{0:0.###}", dy), x0 + 2, y - 2);
            }
        }

        private void AddLine(Brush stroke, double x1, double y1, double x2, double y2)
        {
            canvasGraph.Children.Add(new Line() { X1 = x1, X2 = x2, Y1 = y1, Y2 = y2, Stroke = stroke });            
        }

        private void AddText(string text, double x, double y)
        {
            TextBlock textBlock = new();
            textBlock.Text = text;
            textBlock.Foreground = Brushes.Black;

            // Set the coordinates of the block. "Attached" properties 
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            canvasGraph.Children.Add(textBlock);
        }

        private void DrawAxes()
        {
            AddLine(Brushes.Black, x0, 0, x0, height);
            AddLine(Brushes.Black, 0, y0, width, y0);
            AddText("0", x0 + 2, y0 + 2);
            AddText("X", width - 10, y0 - 14);
            AddText("Y", x0 - 10, 2);
        }

        private void DrawPolynomial(SolidColorBrush solidColor, Polynomial polynomial)
        {
            Polyline polyline = new() { Stroke = solidColor, ClipToBounds = true };

            for (int x = 0; x < width; x++)
            {
                double dy = polynomial.Evaluate((x - x0) / xScale);
                if (double.IsNaN(dy) || double.IsInfinity(dy))
                {
                    continue;
                }
                // We got a "normal" number
                polyline.Points.Add(new System.Windows.Point(x, y0 - dy * yScale)); // - because (0, 0) is top left corner
            }
            canvasGraph.Children.Add(polyline);
        }

    }
}
