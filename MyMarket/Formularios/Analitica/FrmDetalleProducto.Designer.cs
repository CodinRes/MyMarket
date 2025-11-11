using System.Windows.Forms;

namespace MyMarket.Formularios.Analitica
{
    partial class FrmDetalleProducto
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblNombre;
        private Label lblTasaRotacion;
        private Label lblDiasUltimaVenta;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartVentasMes;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblNombre = new Label();
            lblTasaRotacion = new Label();
            lblDiasUltimaVenta = new Label();
            chartVentasMes = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)chartVentasMes).BeginInit();
            SuspendLayout();
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            lblNombre.Location = new System.Drawing.Point(12, 9);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new System.Drawing.Size(120, 21);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Producto XYZ";
            // 
            // lblTasaRotacion
            // 
            lblTasaRotacion.AutoSize = true;
            lblTasaRotacion.Location = new System.Drawing.Point(12, 45);
            lblTasaRotacion.Name = "lblTasaRotacion";
            lblTasaRotacion.Size = new System.Drawing.Size(150, 15);
            lblTasaRotacion.TabIndex = 1;
            lblTasaRotacion.Text = "Tasa rotación stock: --";
            // 
            // lblDiasUltimaVenta
            // 
            lblDiasUltimaVenta.AutoSize = true;
            lblDiasUltimaVenta.Location = new System.Drawing.Point(12, 70);
            lblDiasUltimaVenta.Name = "lblDiasUltimaVenta";
            lblDiasUltimaVenta.Size = new System.Drawing.Size(150, 15);
            lblDiasUltimaVenta.TabIndex = 2;
            lblDiasUltimaVenta.Text = "Días desde última venta: --";
            // 
            // chartVentasMes
            // 
            chartVentasMes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chartVentasMes.Location = new System.Drawing.Point(12, 90);
            chartVentasMes.Name = "chartVentasMes";
            chartVentasMes.Size = new System.Drawing.Size(560, 260);
            chartVentasMes.TabIndex = 3;
            chartVentasMes.Text = "chartVentasMes";
            chartVentasMes.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("Default"));
            chartVentasMes.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Ventas")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column,
                ChartArea = "Default"
            });
            // 
            // FrmDetalleProducto
            // 
            ClientSize = new System.Drawing.Size(584, 361);
            Controls.Add(chartVentasMes);
            Controls.Add(lblDiasUltimaVenta);
            Controls.Add(lblTasaRotacion);
            Controls.Add(lblNombre);
            Name = "FrmDetalleProducto";
            Text = "Detalle Producto";
            ((System.ComponentModel.ISupportInitialize)chartVentasMes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
