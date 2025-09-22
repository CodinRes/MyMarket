using System;
using System.Windows.Forms;
using MyMarket.Data.Infrastructure;
using MyMarket.Forms.Main;

namespace MyMarket;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        try
        {
            var connectionFactory = new SqlConnectionFactory();
            Application.Run(new FrmPrincipal(connectionFactory));
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No fue posible iniciar la aplicación. Detalle: {ex.Message}",
                "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
