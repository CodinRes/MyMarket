using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Modelos;
using MyMarket.Datos.Repositorios;
using MyMarket.Servicios;
using MyMarket.Servicios.Estado;
using MyMarket.Servicios.Estado.Modelos;

namespace MyMarket.Formularios.Recibos;

/// <summary>
///     Pantalla conectada a la base de datos que lista recibos emitidos recientemente.
/// </summary>
public partial class FrmRecibosEmitidos : Form
{
    private readonly SortableBindingList<FacturaListadoDto> _facturas = new();
    private readonly SortableBindingList<FacturaListadoDto> _facturasFiltradas = new();
    private readonly FacturaRepository _facturaRepository;
    private TextBox? _txtBuscar;
    private ComboBox? _cmbEstado;
    private EstadoEmpleado? _sesionActiva;

    public FrmRecibosEmitidos(FacturaRepository facturaRepository)
    {
        _facturaRepository = facturaRepository ?? throw new ArgumentNullException(nameof(facturaRepository));

        InitializeComponent();

        dgv.AutoGenerateColumns = false;
        dgv.DataSource = bindingSourceFacturas;
        bindingSourceFacturas.DataSource = _facturasFiltradas;

        btnVerDetalle.Click += BtnVerDetalle_Click;
        dgv.CellDoubleClick += Dgv_CellDoubleClick;
        dgv.SelectionChanged += (_, _) => ActualizarEstadoAcciones();
        Load += FrmRecibosEmitidos_Load;
        
        // Habilitar ordenamiento por columnas
        DataGridViewHelper.HabilitarOrdenamientoPorColumna(dgv);

        // cargar sesion activa (persistida) para aplicar filtrado si es cajero
        try
        {
            var almacen = new AlmacenEstadoAplicacion();
            var estado = almacen.Cargar();
            _sesionActiva = estado?.SesionActiva;
        }
        catch
        {
            _sesionActiva = null;
        }
    }

    private void FrmRecibosEmitidos_Load(object? sender, EventArgs e)
    {
        AgregarControlesBusqueda();
        ConfigurarColumnas();
        CargarFacturas();
    }

    private void AgregarControlesBusqueda()
    {
        // Agregar los controles directamente en la barra superior (panelBottom)
        panelBottom.WrapContents = false;

        var lblBuscar = new Label
        {
            Text = "Buscar:",
            AutoSize = true,
            Margin = new Padding(0, 12, 6, 0)
        };

        _txtBuscar = new TextBox
        {
            Width = 250,
            PlaceholderText = "Buscar por cliente, empleado o ID...",
            Margin = new Padding(0, 8, 12, 0)
        };
        _txtBuscar.TextChanged += TxtBuscar_TextChanged;

        var lblEstado = new Label
        {
            Text = "Estado:",
            AutoSize = true,
            Margin = new Padding(0, 12, 6, 0)
        };

        _cmbEstado = new ComboBox
        {
            Width = 120,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Margin = new Padding(0, 8, 12, 0)
        };
        _cmbEstado.Items.AddRange(new object[] { "Todos", "pendiente", "pagada", "cancelada" });
        _cmbEstado.SelectedIndex = 0;
        _cmbEstado.SelectedIndexChanged += CmbEstado_SelectedIndexChanged;

        var btnCambiarEstado = new Button
        {
            Text = "Cambiar Estado",
            Width = 120,
            Margin = new Padding(0, 8, 12, 0)
        };
        btnCambiarEstado.Click += BtnCambiarEstado_Click;

        // FlowDirection es RightToLeft. Para que queden en orden de izquierda a derecha
        // (lblBuscar, txtBuscar, lblEstado, cmbEstado, btnCambiarEstado, [espacio], btnVerDetalle a la derecha),
        // debemos agregarlos en orden inverso.
        panelBottom.Controls.Add(btnCambiarEstado);
        panelBottom.Controls.Add(_cmbEstado);
        panelBottom.Controls.Add(lblEstado);
        panelBottom.Controls.Add(_txtBuscar);
        panelBottom.Controls.Add(lblBuscar);
    }

    private void TxtBuscar_TextChanged(object? sender, EventArgs e)
    {
        FiltrarFacturas();
    }

    private void CmbEstado_SelectedIndexChanged(object? sender, EventArgs e)
    {
        FiltrarFacturas();
    }

    private void FiltrarFacturas()
    {
        var textoBusqueda = _txtBuscar?.Text.Trim().ToLowerInvariant() ?? string.Empty;
        var estadoSeleccionado = _cmbEstado?.SelectedItem?.ToString() ?? "Todos";

        _facturasFiltradas.RaiseListChangedEvents = false;
        _facturasFiltradas.Clear();

        var facturasFiltradas = _facturas.AsEnumerable();

        // Filtrar por texto de búsqueda
        if (!string.IsNullOrWhiteSpace(textoBusqueda))
        {
            facturasFiltradas = facturasFiltradas.Where(f =>
                f.ClienteNombreCompleto.ToLowerInvariant().Contains(textoBusqueda) ||
                f.EmpleadoNombreCompleto.ToLowerInvariant().Contains(textoBusqueda) ||
                f.IdFactura.ToString().Contains(textoBusqueda) ||
                f.IdFacturaFormateado.Contains(textoBusqueda));
        }

        // Filtrar por estado
        if (estadoSeleccionado != "Todos")
        {
            facturasFiltradas = facturasFiltradas.Where(f => f.EstadoVenta == estadoSeleccionado);
        }

        foreach (var factura in facturasFiltradas)
        {
            _facturasFiltradas.Add(factura);
        }

        _facturasFiltradas.RaiseListChangedEvents = true;
        bindingSourceFacturas.ResetBindings(false);
        ActualizarEstadoAcciones();
    }

    private void BtnCambiarEstado_Click(object? sender, EventArgs e)
    {
        if (bindingSourceFacturas.Current is not FacturaListadoDto seleccionada)
        {
            MessageBox.Show("Seleccione una factura para cambiar su estado.", "Cambiar estado",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialogo = new Form
        {
            Text = "Cambiar Estado de Factura",
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            ClientSize = new System.Drawing.Size(300, 120)
        };

        var lblEstado = new Label
        {
            Text = "Nuevo estado:",
            Location = new System.Drawing.Point(10, 20),
            AutoSize = true
        };

        var cmbNuevoEstado = new ComboBox
        {
            Location = new System.Drawing.Point(10, 45),
            Width = 280,
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbNuevoEstado.Items.AddRange(new object[] { "pendiente", "pagada", "cancelada" });
        cmbNuevoEstado.SelectedItem = seleccionada.EstadoVenta;

        var btnAceptar = new Button
        {
            Text = "Aceptar",
            DialogResult = DialogResult.OK,
            Location = new System.Drawing.Point(130, 80),
            Width = 75
        };

        var btnCancelar = new Button
        {
            Text = "Cancelar",
            DialogResult = DialogResult.Cancel,
            Location = new System.Drawing.Point(215, 80),
            Width = 75
        };

        dialogo.Controls.AddRange(new Control[] { lblEstado, cmbNuevoEstado, btnAceptar, btnCancelar });
        dialogo.AcceptButton = btnAceptar;
        dialogo.CancelButton = btnCancelar;

        if (dialogo.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        var nuevoEstado = cmbNuevoEstado.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(nuevoEstado))
        {
            return;
        }

        try
        {
            if (_facturaRepository.ActualizarEstadoFactura(seleccionada.IdFactura, nuevoEstado))
            {
                MessageBox.Show("Estado actualizado correctamente.", "Cambiar estado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarFacturas();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el estado.", "Cambiar estado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (SqlException ex)
        {
            MessageBox.Show($"Error al actualizar el estado. Detalle: {ex.Message}", "Cambiar estado",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ConfigurarColumnas()
    {
        if (dgv.Columns.Count > 0)
        {
            return;
        }

        dgv.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(FacturaListadoDto.IdFacturaFormateado),
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

            // si el usuario en sesion es cajero o vendedor limitar a sus propias facturas
            if (_sesionActiva != null && !string.IsNullOrWhiteSpace(_sesionActiva.RolDescripcion))
            {
                var rol = _sesionActiva.RolDescripcion.Trim().ToLowerInvariant();
                if (rol.Contains("cajer") || rol.Contains("vend"))
                {
                    var idEmpleado = _sesionActiva.IdEmpleado;
                    facturas = facturas.Where(f => f.IdEmpleado == idEmpleado).ToList();
                }
            }

            _facturas.RaiseListChangedEvents = false;
            _facturas.Clear();
            foreach (var factura in facturas)
            {
                _facturas.Add(factura);
            }

            _facturas.RaiseListChangedEvents = true;
            
            // Reaplica filtros si existen
            FiltrarFacturas();
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
            var factura = _facturaRepository.ObtenerFacturaPorId(seleccionada.IdFactura);
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
