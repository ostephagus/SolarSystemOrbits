using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WPF;

namespace GUI
{
    class Challenge1UserControl : BaseChallengeUserControl
    {
        public Challenge1UserControl() : base("Graph to verify Keppler's 3rd Law") //Set title via constructor
        {
            SetUpGraph();
        }

        public void SetUpGraph()
        {
            Challenge1PlotViewModel viewModel = new Challenge1PlotViewModel();
            CartesianChart graph = new CartesianChart();
            graph.Series = viewModel.Series;
            ChartHolder.Content = graph;
        }
    }
}
