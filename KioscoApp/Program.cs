using System;
using System.Windows.Forms;

namespace KioscoApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Cambiá estos valores para probar distinto usuario/rol
            string empleado = "Juan Pérez";
            string rol = "Vendedor";

            Application.Run(new FrmPrincipal(empleado, rol));
        }
    }
}
