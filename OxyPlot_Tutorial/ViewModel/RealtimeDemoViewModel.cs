using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Diagnostics;
using System.Threading;

namespace OxyPlot_Tutorial.ViewModel
{
    public enum SimulationType
    {
        Waves,
        TimeSimulation
    }

    public class RealtimeDemoViewModel : INotifyPropertyChanged
    {

        private const int UpdateInterval = 20;

        private readonly Timer timer;
        private readonly Stopwatch watch = new Stopwatch();
        private int numberOfSeries;
        private SimulationType simulationType;

        public event PropertyChangedEventHandler PropertyChanged;

        public RealtimeDemoViewModel()
        {
            this.timer = new Timer(OnTimerElapsed);
            this.Function = (t, x, a) => Math.Cos(t * a) * (x == 0 ? 1 : Math.Sin(x * a) / x);
            this.SimulationType = SimulationType.Waves;
        }

        public SimulationType SimulationType
        {
            get
            {
                return this.simulationType;
            }

            set
            {
                this.simulationType = value;
                this.RaisePropertyChanged("SimulationType");
                this.SetupModel();
            }
        }

        private void SetupModel()
        {
            this.timer.Change(Timeout.Infinite, Timeout.Infinite);

            PlotModel = new PlotModel();
            PlotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -2, Maximum = 2 });

            this.numberOfSeries = this.SimulationType == SimulationType.TimeSimulation ? 1 : 20;

            for (int i = 0; i < this.numberOfSeries; i++)
            {
                PlotModel.Series.Add(new LineSeries { LineStyle = LineStyle.Solid });
            }

            this.watch.Start();

            this.RaisePropertyChanged("PlotModel");

            this.timer.Change(1000, UpdateInterval);
        }

        public int TotalNumberOfPoints { get; private set; }

        private Func<double, double, double, double> Function { get; set; }

        public PlotModel PlotModel { get; private set; }

        private void OnTimerElapsed(object state)
        {
            lock (this.PlotModel.SyncRoot)
            {
                this.Update();
            }

            this.PlotModel.InvalidatePlot(true);
        }

        private void Update()
        {
            double t = this.watch.ElapsedMilliseconds * 0.001;
            int n = 0;

            for (int i = 0; i < PlotModel.Series.Count; i++)
            {
                var s = (LineSeries)PlotModel.Series[i];

                switch (SimulationType)
                {
                    case SimulationType.TimeSimulation:
                        {
                            double x = s.Points.Count > 0 ? s.Points[s.Points.Count - 1].X + 1 : 0;
                            if (s.Points.Count >= 200)
                                s.Points.RemoveAt(0);
                            double y = 0;
                            int m = 80;
                            for (int j = 0; j < m; j++)
                                y += Math.Cos(0.001 * x * j * j);
                            y /= m;
                            s.Points.Add(new DataPoint(x, y));
                            break;
                        }

                    case SimulationType.Waves:
                        s.Points.Clear();
                        double a = 0.5 + i * 0.05;
                        for (double x = -5; x <= 5; x += 0.01)
                        {
                            s.Points.Add(new DataPoint(x, Function(t, x, a)));
                        }

                        break;
                }

                n += s.Points.Count;
            }

            if (this.TotalNumberOfPoints != n)
            {
                this.TotalNumberOfPoints = n;
                this.RaisePropertyChanged("TotalNumberOfPoints");
            }
        }

        protected void RaisePropertyChanged(string property)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}
