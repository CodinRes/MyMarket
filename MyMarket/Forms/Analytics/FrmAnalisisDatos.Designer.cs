namespace MyMarket.Forms.Analytics;

partial class FrmAnalisisDatos
{
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "FrmAnalisisDatos";

            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            var area = new System.Windows.Forms.DataVisualization.Charting.ChartArea("Area");
            this.chart.ChartAreas.Add(area);
            var series = new System.Windows.Forms.DataVisualization.Charting.Series("Ventas $")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column
            };
            this.chart.Series.Add(series);
            series.Points.AddXY("Ene", 120000);
            series.Points.AddXY("Feb", 98000);
            series.Points.AddXY("Mar", 143000);
            series.Points.AddXY("Abr", 110000);
            series.Points.AddXY("May", 156000);
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Text = "Análisis de Datos";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.Controls.Add(this.chart);
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
    }
}