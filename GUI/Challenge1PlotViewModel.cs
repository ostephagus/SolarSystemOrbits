using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Defaults;
using System.ComponentModel;
using System;
using System.Linq;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace GUI
{
    public class Challenge1PlotViewModel : INotifyPropertyChanged
    {
        private ISeries[] series;
        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<SolarSystemBody, bool> solarSystemBodies;

        private const int NUMBER_OF_PLANETS = 8;

        private (float[], float[]) data;

        public ISeries[] Series
        {
            get => series;
            set
            {
                series = value;
                OnPropertyChanged(nameof(Series));
            }
        }

        public List<string> currentEnabledPlanets;

        public Challenge1PlotViewModel(Dictionary<SolarSystemBody, bool> solarSystemBodies)
        {
            this.solarSystemBodies = solarSystemBodies;
            data = GetData();
            currentEnabledPlanets = new List<string>();
            Series = GetSeries(data.Item1, data.Item2);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateSeries()
        {
            Series = GetSeries(data.Item1, data.Item2); //Updating does not require recalculation, just filtering of results.
        }

        private ObservableCollection<ObservablePoint> CreateLineOfBestFit(double gradient, double yIntercept, double xMin, double xMax)
        {
            return new ObservableCollection<ObservablePoint>
            {
                new ObservablePoint(xMin, gradient * xMin + yIntercept), //Calculate the min and max points using y=mx+c
                new ObservablePoint(xMax, gradient * xMax + yIntercept)
            };
        }

        private ISeries[] GetSeries(float[] xarr, float[] yarr)
        {
            ObservableCollection<ObservablePoint> points = new ObservableCollection<ObservablePoint>();
            List<double> xarrFiltered = new List<double>();
            List<double> yarrFiltered = new List<double>();
            for (int pointNum = 0; pointNum < NUMBER_OF_PLANETS; pointNum++)
            {
                if (solarSystemBodies[(SolarSystemBody)(pointNum+1)])
                {
                    double xPoint = xarr[pointNum];
                    double yPoint = yarr[pointNum];
                    points.Add(new ObservablePoint(xPoint, yPoint));
                    xarrFiltered.Add(xPoint);
                    yarrFiltered.Add(yPoint);
                    currentEnabledPlanets.Add(((SolarSystemBody)(pointNum + 1)).ToString()); //Get the string representation of the current planet being added.
                }
            }

            RegressionCalculator regressionCalculator = new RegressionCalculator(xarrFiltered.ToArray(), yarrFiltered.ToArray());
            (double gradient, double yIntercept, double rSquared) = regressionCalculator.Calculate();

            return new ISeries[]
            {
                new LineSeries<ObservablePoint>()
                {
                    Values = CreateLineOfBestFit(gradient, yIntercept, xarrFiltered.Min(), xarrFiltered.Max()),
                    Name = "Line of Best fit",
                    GeometryFill = null,
                    GeometryStroke = null,
                    GeometrySize = 0,
                    Fill = null,
                    Stroke = new SolidColorPaint(SKColors.Red, 3)
                },
                new ScatterSeries<ObservablePoint>()
                {
                    Values = points,
                    Name = "Keppler 3 correlation",
                    TooltipLabelFormatter = (chartPoint) => currentEnabledPlanets[chartPoint.Context.Entity.EntityIndex],
                    Fill = new SolidColorPaint(SKColors.Blue)
                }
            };
        }

        private (float[], float[]) GetData()
        {
            float[] data = new float[16] { 1f, 1.9f, 3f, 4.5f, 5f, 6f, 7f, 8f, 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f }; //Test data
            //TODO: Somehow obtain the data

            if (data.Length != 16)
            {
                MessageBox.Show("The data received from backend was not in a standard format", "Backend link error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            float[] radii = data[0..8];
            float[] periods = data[8..16];
            return (radii, periods);
        }
    }
}
