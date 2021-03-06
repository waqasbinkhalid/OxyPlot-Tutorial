﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot_Tutorial.Model;

namespace OxyPlot_Tutorial.ViewModel
{
    public class OxyPlotDemoViewModel
    {
        public PlotModel PlotModel { get; set; }

        private DateTime lastUpdate = DateTime.Now;

        public OxyPlotDemoViewModel()
        {
            this.PlotModel = new PlotModel();
            SetupPlotModel();
            LoadData();
        }

        private readonly List<OxyColor> colors = new List<OxyColor>
        {
            OxyColors.Green,
            OxyColors.IndianRed,
            OxyColors.Coral,
            OxyColors.Chartreuse,
            OxyColors.Azure
        };

        private readonly List<MarkerType> markerTypes = new List<MarkerType>
        {
            MarkerType.Plus,
            MarkerType.Star,
            MarkerType.Diamond,
            MarkerType.Triangle,
            MarkerType.Cross
        };
        private void SetupPlotModel()
        {
            this.PlotModel.LegendTitle = "Legend";
            this.PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            this.PlotModel.LegendPlacement = LegendPlacement.Outside;
            this.PlotModel.LegendPosition = LegendPosition.TopRight;
            this.PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            this.PlotModel.LegendBorder = OxyColors.Black;

            var dateAxis = new DateTimeAxis(AxisPosition.Bottom, "Date", "HH:MM")
            { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
            this.PlotModel.Axes.Add(dateAxis);

            var valueAxis = new LinearAxis(AxisPosition.Left, 0)
            { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Value" };
            this.PlotModel.Axes.Add(valueAxis);
        }

        private void LoadData()
        {
            List<Measurement> measurements = DataModel.GetData();

            var dataPerDetector = measurements.GroupBy(m => m.DetectorId).OrderBy(m => m.Key).ToList();

            foreach (var data in dataPerDetector)
            {
                var lineSerie = new LineSeries
                {
                    StrokeThickness = 2,
                    MarkerSize = 3,
                    MarkerStroke = colors[data.Key],
                    MarkerType = markerTypes[data.Key],
                    CanTrackerInterpolatePoints = false,
                    Title = string.Format("Detector {0}", data.Key),
                    Smooth = false,
                };

                data.ToList().ForEach(d => lineSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(d.DateTime), d.Value)));
                PlotModel.Series.Add(lineSerie);
            }
            lastUpdate = DateTime.Now;
        }

        public void UpdateModel()
        {
            List<Measurement> measurements = DataModel.GetUpdateData(lastUpdate);
            var dataPerDetector = measurements.GroupBy(m => m.DetectorId).OrderBy(m => m.Key).ToList();

            foreach (var data in dataPerDetector)
            {
                var lineSerie = PlotModel.Series[data.Key] as LineSeries;
                if (lineSerie != null)
                {
                    data.ToList()
                        .ForEach(d => lineSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(d.DateTime), d.Value)));
                }
            }
            lastUpdate = DateTime.Now;
        }

    }
}
