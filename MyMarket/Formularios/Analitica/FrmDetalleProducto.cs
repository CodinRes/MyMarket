using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MyMarket.Datos.Modelos.AnaliticaDTOs;
using MyMarket.Datos.Repositorios;

namespace MyMarket.Formularios.Analitica;

public partial class FrmDetalleProducto : Form
{
    private readonly long _idProducto;
    private readonly AnaliticaRepository _repo;

    public FrmDetalleProducto(long idProducto, AnaliticaRepository repo)
    {
        _idProducto = idProducto;
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));

        InitializeComponent();
        Load += FrmDetalleProducto_Load;
    }

    private void FrmDetalleProducto_Load(object? sender, EventArgs e)
    {
        CargarDetalle();
    }

    private void CargarDetalle()
    {
        var detalle = _repo.GetProductoDetalle(_idProducto);
        if (detalle == null)
        {
            MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Close();
            return;
        }

        lblNombre.Text = detalle.NombreProducto;

        // Totales
        var (totalUnidades, revenue, cost) = _repo.GetTotalesProducto(_idProducto);

        // Tasa de rotación de stock = unidades vendidas / stock actual
        string rotText = "--";
        if (detalle.StockActual > 0)
        {
            var tasa = (decimal)totalUnidades / detalle.StockActual;
            rotText = $"{tasa:F2} (unidades/stock)";
        }
        lblTasaRotacion.Text = $"Tasa rotación stock: {rotText} (stock actual: {detalle.StockActual})";

        // Días desde última venta
        var ultima = _repo.GetFechaUltimaVenta(_idProducto);
        if (ultima.HasValue)
        {
            var dias = (DateTime.Today - ultima.Value.Date).Days;
            lblDiasUltimaVenta.Text = $"Días desde última venta: {dias}";
        }
        else
        {
            lblDiasUltimaVenta.Text = "Días desde última venta: sin ventas";
        }

        // Ventas por mes -> gráfico
        var ventasMes = _repo.GetVentasPorMes(_idProducto);
        var series = chartVentasMes.Series.FirstOrDefault(s => s.Name == "Ventas");
        if (series == null)
        {
            series = new Series("Ventas") { ChartType = SeriesChartType.Column };
            chartVentasMes.Series.Add(series);
        }
        series.Points.Clear();

        foreach (var v in ventasMes)
        {
            var label = $"{v.Year}-{v.Month:D2}";
            series.Points.AddXY(label, v.Unidades);
        }

        if (!ventasMes.Any())
        {
            series.Points.AddXY("sin datos", 0);
        }

        chartVentasMes.Invalidate();
    }
}
