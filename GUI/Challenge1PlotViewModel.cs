using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace GUI
{
    public class Challenge1PlotViewModel
    {
        public ISeries[] Series { get; set; } = new ISeries[]
        {
            new ScatterSeries<double>
            {
                Values = new ObservableCollection<double> {1.4, 1.6, 1.8, 2.0, 3.44},
                Name = "Keppler 3 Correlation"
            }
        };
    }
}
