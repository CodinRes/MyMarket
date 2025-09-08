using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace KioscoApp
{
    public partial class FrmAnalisisDatos : Form
    {
        public FrmAnalisisDatos()
        {
            Text = "Análisis de Datos";
            Padding = new Padding(16);

            var chart = new Chart { Dock = DockStyle.Fill };
            var area = new ChartArea("Area");
            chart.ChartAreas.Add(area);

            var series = new Series("Ventas $")
            {
                ChartType = SeriesChartType.Column
            };
            chart.Series.Add(series);

            // Datos fake por mes
            series.Points.AddXY("Ene", 120000);
            series.Points.AddXY("Feb", 98000);
            series.Points.AddXY("Mar", 143000);
            series.Points.AddXY("Abr", 110000);
            series.Points.AddXY("May", 156000);

            Controls.Add(chart);
        }
    }
}
