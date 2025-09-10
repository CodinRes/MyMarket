using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MyMarket
{
    public partial class FrmRecibosEmitidos : Form
    {
        public FrmRecibosEmitidos()
        {
            InitializeComponent();
            btnVerDetalle.Click += (s, e) => MessageBox.Show("Detalle del recibo (prototipo).");
            Load += FrmRecibosEmitidos_Load;
        }

        private void FrmRecibosEmitidos_Load(object sender, EventArgs e)
        {
            dgv.Columns.Clear();
            dgv.Columns.Add("Nro", "Nro");
            dgv.Columns.Add("Fecha", "Fecha");
            dgv.Columns.Add("Cliente", "Cliente");
            dgv.Columns.Add("Total", "Total");
            dgv.Rows.Add("0001-00001234", DateTime.Today.ToShortDateString(), "Juan López", 2500);
            dgv.Rows.Add("0001-00001235", DateTime.Today.ToShortDateString(), "Ana Ruiz", 1800);
            dgv.Rows.Add("0001-00001236", DateTime.Today.ToShortDateString(), "Carlos Pérez", 4200);
        }
    }
}
