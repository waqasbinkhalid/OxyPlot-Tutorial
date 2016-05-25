using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot.Series;
using OxyPlot;
using System.Diagnostics;
using System.Windows;

namespace OxyPlot_Tutorial.ViewModel
{
    public class StackOverflowViewModel
    {
        public PlotModel PlotModel
        {
            get;set;
        }
        public PlotModel RunTimePlotModel
        {
            get; set;
        }
        public FunctionSeries FunctionSeries
        {
            get; set;
        }
        public StackOverflowViewModel()
        {
            FunctionSeries = new FunctionSeries();

            this.PlotModel = new PlotModel();
            this.RunTimePlotModel = new PlotModel();
            Graph();
        }

        public double GetValue(int x, int y)
        {
            return (10 * x * x + 11 * x * y * y + 12 * x * y);
        }

        public FunctionSeries GetFunction()
        {
            int n = 100;

            FunctionSeries functionSeries = new FunctionSeries();
            for(int x = 0; x < n; x++)
            {
                for(int y = 0; y < n; y++)
                {
                    DataPoint dataPoint = new DataPoint(x, GetValue(x, y));
                    functionSeries.Points.Add(dataPoint);
                }
            }

            functionSeries.MouseDown += FunctionSeries_MouseDown;

            return functionSeries;
        }

        public FunctionSeries GetFunction(double value)
        {
            try
            {
                var functionSeries = new FunctionSeries(Math.Sin, value, value + 10, 1, "SIN(x)");
                functionSeries.Color = OxyColors.Red;
                Debug.WriteLine("Value = " + value);
                return functionSeries;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        private void FunctionSeries_MouseDown(object sender, OxyMouseDownEventArgs e)
        {
            double x = 0.0;
            double y = 0.0;

            x = (sender as FunctionSeries).InverseTransform(e.Position).X;
            y = (sender as FunctionSeries).InverseTransform(e.Position).Y;

            Debug.WriteLine("X and Y Values: " + x + "," + y);

            Graph(x);
        }

        public void Graph()
        {
            var model = new PlotModel
            {
                Title = "Example"
            };

            model.Series.Add(GetFunction());
            this.PlotModel = model;
        }

        public void Graph(double x)
        {
            var model = new PlotModel
            {
                Title = "Example"                
            };

            this.FunctionSeries = GetFunction(x);
            model.Series.Add(this.FunctionSeries);
            this.RunTimePlotModel = model;
            this.RunTimePlotModel.InvalidatePlot(true);
        }
    }
}
