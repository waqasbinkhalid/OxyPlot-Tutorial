using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using System.ComponentModel;
using System.Windows.Media;

namespace OxyPlot_Tutorial.ViewModel
{
    public class RefreshDemoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private PlotModel plotModel;
        public PlotModel PlotModel
        {
            get
            {
                return this.plotModel;
            }           
            set
            {
                this.plotModel = value;
                this.RaisePropertyChanged("PlotModel");
            }
        }

        public RefreshDemoViewModel()
        {
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private double lastUpdateTime;

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            double seconds = ((RenderingEventArgs)e).RenderingTime.TotalSeconds;
            if(seconds > this.lastUpdateTime + 0.2)
            {
                this.PlotModel = this.CreatePlot();
                this.lastUpdateTime = seconds;
            }
        }

        private double x;

        private PlotModel CreatePlot()
        {
            var pm = new PlotModel
            {
                Title = "Plot Updated: " + DateTime.Now
            };

            pm.Series.Add(new FunctionSeries(Math.Sin, x, x + 4, 0.01));
            x += 0.1;
            return pm;
        }

        protected void RaisePropertyChanged(string property)
        {
            var handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
