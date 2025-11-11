using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Infraestructura;
using MyMarket.Datos.Modelos;
using MyMarket.Datos.Repositorios;

namespace MyMarket.Formularios.Recibos;

/// <summary>
///     Formulario para administrar métodos de pago (crear nuevos y activar/desactivar existentes).
/// </summary>
public partial class FrmGestionMetodosPago : Form
{
    private readonly MetodoPagoRepository _metodoPagoRepository;
    private readonly BindingList<MetodoPagoViewModel> _metodosPago = new();
    private readonly BindingSource _bindingSource = new();

    public FrmGestionMetodosPago(SqlConnectionFactory connectionFactory)
    {
        if (connectionFactory is null)
        {
            throw new ArgumentNullException(nameof(connectionFactory));
        }

        _metodoPagoRepository = new MetodoPagoRepository(connectionFactory);

        InitializeComponent();
        ConfigurarDataGridView();

        Load += FrmGestionMetodosPago_Load;
        btnNuevo.Click += BtnNuevo_Click;
        btnActivarDesactivar.Click += BtnActivarDesactivar_Click;
        btnCerrar.Click += (_, _) => Close();
        dgvMetodosPago.SelectionChanged += DgvMetodosPago_SelectionChanged;
    }

    private void FrmGestionMetodosPago_Load(object? sender, EventArgs e)
    {
        CargarMetodosPago();
        ActualizarEstadoBotones();
    }

    private void ConfigurarDataGridView()
    {
        dgvMetodosPago.AutoGenerateColumns = false;
        dgvMetodosPago.Columns.Clear();

        dgvMetodosPago.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colId",
            HeaderText = "ID",
            DataPropertyName = nameof(MetodoPagoViewModel.IdMetodoPago),
            Width = 60,
            ReadOnly = true
        });

        dgvMetodosPago.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colIdentificacion",
            HeaderText = "Identificación",
            DataPropertyName = nameof(MetodoPagoViewModel.IdentificacionPago),
            Width = 120,
            ReadOnly = true
        });

        dgvMetodosPago.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colProveedor",
            HeaderText = "Proveedor",
            DataPropertyName = nameof(MetodoPagoViewModel.ProveedorPago),
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            ReadOnly = true
        });

        dgvMetodosPago.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "colComision",
            HeaderText = "Comisión (%)",
            DataPropertyName = nameof(MetodoPagoViewModel.ComisionProveedor),
            Width = 100,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" },
            ReadOnly = true
        });

        dgvMetodosPago.Columns.Add(new DataGridViewCheckBoxColumn
        {
            Name = "colActivo",
            HeaderText = "Activo",
            DataPropertyName = nameof(MetodoPagoViewModel.Activo),
            Width = 60,
            ReadOnly = true,
            SortMode = DataGridViewColumnSortMode.Automatic
        });

        _bindingSource.DataSource = _metodosPago;
        dgvMetodosPago.DataSource = _bindingSource;
    }

    private void CargarMetodosPago()
    {
        try
        {
            var metodos = _metodoPagoRepository.ObtenerTodosMetodos();
            _metodosPago.Clear();

            foreach (var metodo in metodos)
            {
                _metodosPago.Add(new MetodoPagoViewModel
                {
                    IdMetodoPago = metodo.IdMetodoPago,
                    IdentificacionPago = metodo.IdentificacionPago,
                    ProveedorPago = metodo.ProveedorPago,
                    ComisionProveedor = metodo.ComisionProveedor,
                    Activo = metodo.Activo
                });
            }
        }
        catch (SqlException ex)
        {
            MessageBox.Show($"Error al cargar métodos de pago: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error inesperado: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnNuevo_Click(object? sender, EventArgs e)
    {
        using var form = new FrmNuevoMetodoPago();
        if (form.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        try
        {
            var idNuevo = _metodoPagoRepository.CrearMetodoPago(
                form.IdentificacionPago,
                form.ProveedorPago,
                form.ComisionProveedor);

            _metodosPago.Add(new MetodoPagoViewModel
            {
                IdMetodoPago = idNuevo,
                IdentificacionPago = form.IdentificacionPago,
                ProveedorPago = form.ProveedorPago,
                ComisionProveedor = form.ComisionProveedor,
                Activo = true
            });

            MessageBox.Show("Método de pago creado correctamente.", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (SqlException ex)
        {
            MessageBox.Show($"Error al crear método de pago: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error inesperado: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnActivarDesactivar_Click(object? sender, EventArgs e)
    {
        if (dgvMetodosPago.CurrentRow?.DataBoundItem is not MetodoPagoViewModel metodo)
        {
            return;
        }

        var nuevoEstado = !metodo.Activo;
        var accion = nuevoEstado ? "activar" : "desactivar";

        var confirmacion = MessageBox.Show(
            $"¿Está seguro que desea {accion} el método de pago '{metodo.ProveedorPago}'?",
            "Confirmar cambio",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirmacion != DialogResult.Yes)
        {
            return;
        }

        try
        {
            var exito = _metodoPagoRepository.ActualizarMetodoPago(
                metodo.IdMetodoPago,
                metodo.IdentificacionPago,
                metodo.ProveedorPago,
                metodo.ComisionProveedor,
                nuevoEstado);

            if (exito)
            {
                metodo.Activo = nuevoEstado;
                _bindingSource.ResetBindings(false);
                ActualizarEstadoBotones();

                MessageBox.Show($"Método de pago {accion}do correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el método de pago.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (SqlException ex)
        {
            MessageBox.Show($"Error al actualizar método de pago: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error inesperado: {ex.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void DgvMetodosPago_SelectionChanged(object? sender, EventArgs e)
    {
        ActualizarEstadoBotones();
    }

    private void ActualizarEstadoBotones()
    {
        if (dgvMetodosPago.CurrentRow?.DataBoundItem is MetodoPagoViewModel metodo)
        {
            btnActivarDesactivar.Enabled = true;
            btnActivarDesactivar.Text = metodo.Activo ? "Desactivar" : "Activar";
        }
        else
        {
            btnActivarDesactivar.Enabled = false;
        }
    }

    private class MetodoPagoViewModel
    {
        public long IdMetodoPago { get; set; }
        public long IdentificacionPago { get; set; }
        public string ProveedorPago { get; set; } = string.Empty;
        public decimal ComisionProveedor { get; set; }
        public bool Activo { get; set; }
    }
}
