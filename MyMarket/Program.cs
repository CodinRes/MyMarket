using System;
using System.Windows.Forms;
using MyMarket.Datos.Infraestructura;
using MyMarket.Formularios.Principal;

namespace MyMarket;

/// <summary>
///     Punto de entrada de la aplicación de escritorio. Se encarga de preparar el
///     entorno visual y de abrir la ventana principal.
/// </summary>
internal static class Program
{
    [STAThread]
    /// <summary>
    ///     Método principal de la aplicación. Configura WinForms, crea los servicios
    ///     necesarios y maneja cualquier excepción crítica que impida iniciar el programa.
    /// </summary>
    private static void Main()
    {
        // Activa los estilos visuales modernos de Windows Forms.
        Application.EnableVisualStyles();
        // Evita discrepancias en el renderizado de texto entre controles.
        Application.SetCompatibleTextRenderingDefault(false);

        try
        {
            // La aplicación requiere una fábrica de conexiones para acceder a la base de datos.
            var connectionFactory = new SqlConnectionFactory();
            // Inicia la ventana principal pasando la infraestructura necesaria.
            Application.Run(new FrmPrincipal(connectionFactory));
        }
        catch (Exception ex)
        {
            // Si ocurre un error inesperado se informa al usuario antes de cerrar la aplicación.
            MessageBox.Show($"No fue posible iniciar la aplicación. Detalle: {ex.Message}",
                "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
