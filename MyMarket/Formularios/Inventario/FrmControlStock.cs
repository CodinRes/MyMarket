using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MyMarket.Datos.Modelos;
using MyMarket.Datos.Repositorios;
using MyMarket.Datos.Infraestructura;

namespace MyMarket.Formularios.Inventario;

/// <summary>
///     Pantalla para gestionar productos y stock.
/// </summary>
public partial class FrmControlStock : Form
{
    private readonly ProductoRepository _productoRepository;
    private List<ProductoDto> _productos = new();

    public FrmControlStock(SqlConnectionFactory connectionFactory)
    {
        _productoRepository = new ProductoRepository(connectionFactory);

        InitializeComponent();

        btnAgregar.Click += BtnAgregar_Click;
        btnEditar.Click += BtnEditar_Click;
        btnEliminar.Click += BtnEliminar_Click;
        btnRefrescar.Click += BtnRefrescar_Click;

        Load += FrmControlStock_Load;
    }

    private void FrmControlStock_Load(object? sender, EventArgs e)
    {
        CargarProductos();
    }

    private void CargarProductos()
    {
        try
        {
            _productos = _productoRepository.ObtenerTodosLosProductos().ToList();
            dgv.DataSource = null;
            dgv.DataSource = _productos;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible cargar los productos. Detalle: {ex.Message}", "Control de stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnRefrescar_Click(object? sender, EventArgs e) => CargarProductos();

    private void BtnAgregar_Click(object? sender, EventArgs e)
    {
        using var dialog = new FrmEditarProductoDialog();
        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        var nuevo = dialog.ToProductoDto();
        try
        {
            _productoRepository.CrearProducto(nuevo);
            CargarProductos();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible crear el producto. Detalle: {ex.Message}", "Control de stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private ProductoDto? ObtenerProductoSeleccionado()
    {
        if (dgv.CurrentRow?.DataBoundItem is ProductoDto p)
            return p;
        return null;
    }

    private void BtnEditar_Click(object? sender, EventArgs e)
    {
        var seleccionado = ObtenerProductoSeleccionado();
        if (seleccionado is null)
        {
            MessageBox.Show("Seleccione un producto para editar.", "Control de stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var dialog = new FrmEditarProductoDialog(seleccionado);
        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        var actualizado = dialog.ToProductoDto();
        try
        {
            if (_productoRepository.ActualizarProducto(actualizado))
            {
                CargarProductos();
            }
            else
            {
                MessageBox.Show("No se pudo actualizar el producto.", "Control de stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible actualizar el producto. Detalle: {ex.Message}", "Control de stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnEliminar_Click(object? sender, EventArgs e)
    {
        var seleccionado = ObtenerProductoSeleccionado();
        if (seleccionado is null)
        {
            MessageBox.Show("Seleccione un producto para eliminar.", "Control de stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (MessageBox.Show($"¿Confirma eliminar el producto {seleccionado.Nombre}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        try
        {
            if (_productoRepository.EliminarProducto(seleccionado.CodigoProducto))
            {
                CargarProductos();
            }
            else
            {
                MessageBox.Show("No se pudo eliminar el producto.", "Control de stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible eliminar el producto. Detalle: {ex.Message}", "Control de stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnToggleActivo_Click(object? sender, EventArgs e)
    {
        var seleccionado = ObtenerProductoSeleccionado();
        if (seleccionado is null)
        {
            MessageBox.Show("Seleccione un producto.", "Control de stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var nuevoEstado = !seleccionado.Estado;
        try
        {
            if (_productoRepository.CambiarEstadoProducto(seleccionado.CodigoProducto, nuevoEstado))
            {
                CargarProductos();
            }
            else
            {
                MessageBox.Show("No se pudo cambiar el estado del producto.", "Control de stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible cambiar el estado del producto. Detalle: {ex.Message}", "Control de stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
