using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Input;

namespace OxyPlot_Tutorial.ViewModel
{
    public class TrigonometricPlotViewModel : INotifyPropertyChanged, ICommand
    {
        public PlotModel TrigonometricPlotModel { get; set; }

        private PlotModel runTimePlotModel;
        public PlotModel RunTimePlotModel
        {
            get
            {
                return this.runTimePlotModel;
            }
            set
            {
                this.runTimePlotModel = value;
                this.RaisePropertyChanged("RunTimePlotModel");
            }
        }

        private FunctionSeries functionSeries;

        private FunctionSeries functionSeries_2;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public TrigonometricPlotViewModel()
        {
            this.TrigonometricPlotModel = new PlotModel();

            this.RunTimePlotModel = new PlotModel();
            //this.RunTimePlotModel.ResetAllAxes();

            this.functionSeries_2 = new FunctionSeries();

            this.functionSeries = new FunctionSeries(x => Math.Sin(x) / x, 0, 100, 0.1, "SIN(x)");

            this.TrigonometricPlotModel.Series.Add(functionSeries);

            functionSeries.MouseDown += FunctionSeries_MouseDown;
       
        }

        private void UpdatePlot(double x)
        {
            LinearAxis xAxis = new LinearAxis();
            xAxis.StartPosition = x;
            xAxis.EndPosition = x + 10;

            LinearAxis yAxis = new LinearAxis();
            yAxis.StartPosition = 0;

            var plotModel = new PlotModel();
            Debug.WriteLine("Value of X = " + x);
            try
            {
                var functionSeries_2 = new FunctionSeries(Math.Sin, x, x + 10, 1, "SIN(x)")
                {
                    Color = OxyColors.Red
                };
            }
            finally
            {        
            }

            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);

            plotModel.Series.Add(functionSeries_2);

            RunTimePlotModel = plotModel;
            RunTimePlotModel.InvalidatePlot(true);

            Debug.WriteLine("Exiting Test Button Clicked");

            //this.RunTimePlotModel.ResetAllAxes();
        }

        private void FunctionSeries_MouseDown(object sender, OxyMouseDownEventArgs e)
        {
            double x = 0.0;
            double y = 0.0;        

            x = (sender as FunctionSeries).InverseTransform(e.Position).X;
            y = (sender as FunctionSeries).InverseTransform(e.Position).Y;

            Debug.WriteLine("X and Y Values: " + x +"," + y);

            //this.functionSeries_2.Points.Add(Math.Sin, x, x + 10, 1, "SIN(x)");

            this.functionSeries_2.Color = OxyColors.Red;



            RunTimePlotModel.Series.Add(functionSeries_2);
            RunTimePlotModel.InvalidatePlot(true);
       
            //UpdatePlot(x,1);
        }

        private void UpdatePlot(double x, int y)
        {
            //var plotModel = new PlotModel();

            Debug.WriteLine("Value of X = " + x);
            try
            {
                functionSeries_2 = new FunctionSeries(Math.Sin, x, x + 10, 1, "SIN(x)")
                {
                    Color = OxyColors.Red
                };
            }
            finally
            {
            }

            RunTimePlotModel.Series.Add(functionSeries_2);
            RunTimePlotModel.InvalidatePlot(true);

            this.RunTimePlotModel.ResetAllAxes();
        }

        protected void RaisePropertyChanged(string property)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        private double x = 5.5;

        public void Execute(object parameter)
        {
            Debug.WriteLine("Test Button Clicked");

            LinearAxis xAxis = new LinearAxis();
            xAxis.StartPosition = 0;

            LinearAxis yAxis = new LinearAxis();
            yAxis.StartPosition = 0;


            var plotModel = new PlotModel();
            var functionSeries_2 = new FunctionSeries(Math.Sin, 0, 5, 1)
            {
                Color = OxyColors.Red
            };

            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);

            plotModel.Series.Add(functionSeries_2);

            RunTimePlotModel = plotModel;

            //RunTimePlotModel = null;

            RunTimePlotModel.InvalidatePlot(true);

            Debug.WriteLine("Exiting Test Button Clicked");

            this.RunTimePlotModel.ResetAllAxes();
        }
    }
}
