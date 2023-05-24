using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Defaults;

namespace GUI
{
    public class Challenge1PlotViewModel
    {

        private ISeries[] series;

        private Dictionary<SolarSystemBody, bool> solarSystemBodies;

        const int NUMBER_OF_PLANETS = 8;

        public ISeries[] Series { get => series; set => series = value; }

        public Challenge1PlotViewModel(Dictionary<SolarSystemBody, bool> solarSystemBodies)
        {
            this.solarSystemBodies = solarSystemBodies;
            (float[] radii, float[] periods) = GetData();
            series = GetSeries(radii, periods);
        }

        private ISeries[] GetSeries(float[] xarr, float[] yarr)
        {
            ObservableCollection<ObservablePoint> points = new ObservableCollection<ObservablePoint>();
            for (int pointNum = 0; pointNum < NUMBER_OF_PLANETS; pointNum++)
            {
                if (solarSystemBodies[(SolarSystemBody)(pointNum+1)])
                {
                    points.Add(new ObservablePoint(xarr[pointNum], yarr[pointNum]));
                }
            }
            return new ISeries[]
            {
                new ScatterSeries<ObservablePoint>()
                {
                    Values = points,
                    Name = "Keppler 3 correlation",
                    TooltipLabelFormatter = (chartPoint) => ((SolarSystemBody)(chartPoint.Context.Index + 1)).ToString()
                }
            };
        }

        private (float[], float[]) GetData()
        {
            float[] data = new float[16] { 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f, 1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f }; //Test data
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
