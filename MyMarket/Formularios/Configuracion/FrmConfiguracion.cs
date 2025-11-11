using System;
using System.Windows.Forms;
using MyMarket.Datos.Modelos;
using MyMarket.Servicios.Estado;

namespace MyMarket.Formularios.Configuracion;

/// <summary>
///     Formulario para configurar los parámetros globales de la aplicación.
/// </summary>
public partial class FrmConfiguracion : Form
{
    private readonly AlmacenConfiguracion _almacenConfiguracion;
    private ConfiguracionDto _configuracion;

    public FrmConfiguracion()
    {
        _almacenConfiguracion = new AlmacenConfiguracion();
        _configuracion = _almacenConfiguracion.Cargar();

        InitializeComponent();

        btnGuardar.Click += BtnGuardar_Click;
        btnCancelar.Click += BtnCancelar_Click;
        Load += FrmConfiguracion_Load;
    }

    private void FrmConfiguracion_Load(object? sender, EventArgs e)
    {
        CargarConfiguracion();
    }

    private void CargarConfiguracion()
    {
        numPorcentajeImpuestos.Value = _configuracion.PorcentajeImpuestos;
        numDiasAntiguedad.Value = _configuracion.DiasAntiguedadMinima;
        numPorcentajeDescuento.Value = _configuracion.PorcentajeDescuentoAntiguedad;
    }

    private void BtnGuardar_Click(object? sender, EventArgs e)
    {
        try
        {
            _configuracion.PorcentajeImpuestos = (byte)numPorcentajeImpuestos.Value;
            _configuracion.DiasAntiguedadMinima = (int)numDiasAntiguedad.Value;
            _configuracion.PorcentajeDescuentoAntiguedad = numPorcentajeDescuento.Value;

            _almacenConfiguracion.Guardar(_configuracion);

            MessageBox.Show("Configuración guardada correctamente.", "Configuración",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al guardar la configuración: {ex.Message}", "Configuración",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnCancelar_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
