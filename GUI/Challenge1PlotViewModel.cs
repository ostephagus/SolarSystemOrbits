using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Defaults;

namespace GUI
{
    public class Challenge1PlotViewModel
    {
        private ISeries[] series;

        public ISeries[] Series { get => series; set => series = value; }

        public Challenge1PlotViewModel(Dictionary<SolarSystemBody, bool> solarSystemBodies)
        {
            (float[] radii, float[] periods, float rSquared) = GetData();
            series = GetSeries(radii, periods);
        }

        private ISeries[] GetSeries(float[] xarr, float[] yarr)
        {
            int numOfPoints = xarr.Length;
            ObservableCollection<ObservablePoint> points = new ObservableCollection<ObservablePoint>();
            for (int pointNum = 0; pointNum < numOfPoints; pointNum++)
            {
                points.Add(new ObservablePoint(xarr[pointNum], yarr[pointNum]));
            }
            return new ISeries[]
            {
                new ScatterSeries<ObservablePoint>()
                {
                    Values = points,
                    Name = "Keppler 3 correlation"
                }
            };
        }

        [DllImport("kernel.dll", CallingConvention = CallingConvention.Cdecl)]
        private extern static float[] challengeOne();

        private (float[], float[], float) GetData()
        {

            float[] data = challengeOne();
            if (data.Length != 17)
            {
                MessageBox.Show("The data received from backend is not in a recognisable form.", "Caution: unrecognised data", MessageBoxButton.OK, MessageBoxImage.Error);
                return (new float[8], new float[8], 0f); //Return all zeros when data received is nonstandard
            }
            float[] radii = data[0..7];
            float[] periods = data[8..15];
            float rSquared = data[16];
            return (radii, periods, rSquared);
        }
    }
}
