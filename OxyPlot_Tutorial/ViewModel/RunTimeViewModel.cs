using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;

namespace OxyPlot_Tutorial.ViewModel
{
    public class RunTimeViewModel
    {
        public PlotView SinPlotView
        {
            get; set;
        }

        public PlotModel SinPlotModel
        {
            get; set;
        }

        public RunTimeViewModel()
        {
            this.SinPlotView = new PlotView();
            this.SinPlotView = PlotData();

            this.SinPlotModel = new PlotModel();
            PlotData(0);

        }

        private PlotView PlotData()
        {
            PlotView plotView = new PlotView();            

            var plotModel = new PlotModel();
            var functionSeries = new FunctionSeries(Math.Sin, 0, 100, 0.1, "SIN(x)")
            {
                Color = OxyColors.Blue
            };

            plotModel.Series.Add(functionSeries);
            plotView.Model = plotModel;

            return plotView;
        }

        private void PlotData(double x)
        {
            var plotModel = new PlotModel();
            var functionSeries = new FunctionSeries(Math.Sin, x, 100, 0.1, "SIN(x)")
            {
                Color = OxyColors.Blue
            };

            plotModel.Series.Add(functionSeries);

            SinPlotModel = plotModel;
        }
    }
}
