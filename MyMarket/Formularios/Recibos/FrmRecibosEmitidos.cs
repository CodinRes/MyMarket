using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Modelos;
using MyMarket.Datos.Repositorios;

namespace MyMarket.Formularios.Recibos;

/// <summary>
///     Pantalla conectada a la base de datos que lista recibos emitidos recientemente.
/// </summary>
public partial class FrmRecibosEmitidos : Form
{
    private readonly BindingList<FacturaListadoDto> _facturas = new();
    private readonly FacturaRepository _facturaRepository;

    public FrmRecibosEmitidos(FacturaRepository facturaRepository)
    {
        _facturaRepository = facturaRepository ?? throw new ArgumentNullException(nameof(facturaRepository));

        InitializeComponent();

        dgv.AutoGenerateColumns = false;
        dgv.DataSource = bindingSourceFacturas;
        bindingSourceFacturas.DataSource = _facturas;

        btnVerDetalle.Click += BtnVerDetalle_Click;
        dgv.CellDoubleClick += Dgv_CellDoubleClick;
        dgv.SelectionChanged += (_, _) => ActualizarEstadoAcciones();
        Load += FrmRecibosEmitidos_Load;
    }

    private void FrmRecibosEmitidos_Load(object? sender, EventArgs e)
    {
        ConfigurarColumnas();
        CargarFacturas();
    }

    private void ConfigurarColumnas()
    {
        if (dgv.Columns.Count > 0)
        {
            return;
        }

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(FacturaListadoDto.CodigoFormateado),
            HeaderText = "Factura",
            FillWeight = 20,
            MinimumWidth = 90,
            ReadOnly = true
        });

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(FacturaListadoDto.FechaEmision),
            HeaderText = "Fecha",
            FillWeight = 25,
            MinimumWidth = 110,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "g" },
            ReadOnly = true
        });

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(FacturaListadoDto.ClienteNombreCompleto),
            HeaderText = "Cliente",
            FillWeight = 40,
            MinimumWidth = 150,
            ReadOnly = true
        });

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(FacturaListadoDto.EstadoVenta),
            HeaderText = "Estado",
            FillWeight = 20,
            MinimumWidth = 100,
            ReadOnly = true
        });

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(FacturaListadoDto.Total),
            HeaderText = "Total",
            FillWeight = 25,
            MinimumWidth = 110,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "C2",
                FormatProvider = CultureInfo.CurrentCulture
            },
            ReadOnly = true
        });

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(FacturaListadoDto.EmpleadoNombreCompleto),
            HeaderText = "Vendedor",
            FillWeight = 35,
            MinimumWidth = 150,
            ReadOnly = true
        });
    }

    private void CargarFacturas()
    {
        try
        {
            var facturas = _facturaRepository.ObtenerFacturasEmitidas();

            _facturas.RaiseListChangedEvents = false;
            _facturas.Clear();
            foreach (var factura in facturas)
            {
                _facturas.Add(factura);
            }

            _facturas.RaiseListChangedEvents = true;
            bindingSourceFacturas.ResetBindings(false);
            ActualizarEstadoAcciones();
        }
        catch (SqlException ex)
        {
            MostrarErrorCarga(ex.Message);
        }
        catch (Exception ex)
        {
            MostrarErrorCarga(ex.Message);
        }
    }

    private void MostrarErrorCarga(string mensaje)
    {
        MessageBox.Show($"No fue posible recuperar las facturas emitidas. Detalle: {mensaje}", "Recibos emitidos",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    /// <summary>
    ///     Abre el formulario con el detalle de la factura seleccionada.
    /// </summary>
    private void BtnVerDetalle_Click(object? sender, EventArgs e)
    {
        MostrarDetalleFacturaSeleccionada();
    }

    private void Dgv_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            MostrarDetalleFacturaSeleccionada();
        }
    }

    private void MostrarDetalleFacturaSeleccionada()
    {
        if (bindingSourceFacturas.Current is not FacturaListadoDto seleccionada)
        {
            MessageBox.Show("Seleccione un recibo para ver el detalle.", "Recibos emitidos",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        try
        {
            var factura = _facturaRepository.ObtenerFacturaPorCodigo(seleccionada.CodigoFactura);
            if (factura is null)
            {
                MessageBox.Show("La factura seleccionada ya no se encuentra disponible.", "Recibos emitidos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var detalle = new FrmDetalleFactura(factura);
            detalle.ShowDialog(this);
        }
        catch (SqlException ex)
        {
            MessageBox.Show($"No fue posible recuperar la factura. Detalle: {ex.Message}", "Recibos emitidos",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ocurrió un error al abrir el comprobante. Detalle: {ex.Message}", "Recibos emitidos",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ActualizarEstadoAcciones()
    {
        btnVerDetalle.Enabled = bindingSourceFacturas.Current is FacturaListadoDto;
    }
}
