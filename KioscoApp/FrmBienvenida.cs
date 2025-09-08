using System.Drawing;
using System.Windows.Forms;

namespace KioscoApp
{
    public partial class FrmBienvenida : Form
    {
        public FrmBienvenida()
        {
            BackColor = Color.White;
            var lbl = new Label
            {
                Text = "Bienvenido al Sistema de Gestión del Kiosco",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            Controls.Add(lbl);
        }
    }
}
