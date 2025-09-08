using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KioscoApp
{
    public partial class FrmRecibosEmitidos : Form
    {
        private DataGridView dgv;
        private Button btnVerDetalle;

        public FrmRecibosEmitidos()
        {
            Text = "Recibos Emitidos";
            Padding = new Padding(16);

            dgv = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
            dgv.Columns.Add("Nro", "Nro");
            dgv.Columns.Add("Fecha", "Fecha");
            dgv.Columns.Add("Cliente", "Cliente");
            dgv.Columns.Add("Total", "Total");
            Controls.Add(dgv);

            var panelBottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 48, FlowDirection = FlowDirection.RightToLeft };
            btnVerDetalle = new Button { Text = "Ver detalle", Width = 120, Height = 28 };
            btnVerDetalle.Click += (s, e) => MessageBox.Show("Detalle del recibo (prototipo).");
            panelBottom.Controls.Add(btnVerDetalle);
            Controls.Add(panelBottom);

            // Datos fake
            dgv.Rows.Add("0001-00001234", DateTime.Today.ToShortDateString(), "Juan López", 2500);
            dgv.Rows.Add("0001-00001235", DateTime.Today.ToShortDateString(), "Ana Ruiz", 1800);
            dgv.Rows.Add("0001-00001236", DateTime.Today.ToShortDateString(), "Carlos Pérez", 4200);
        }
    }
}
