using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MyMarket.Formularios.Analitica
{
    partial class FrmAnalisisDatos
    {
        private IContainer components = null;

        private Panel panelTabs;
        private Button btnVendedores;
        private Button btnProductos;
        private ComboBox cmbPeriodo;
        private DataGridView dgvResultados;

        private ComboBox cmbCategoria;
        private Panel panelBottom;
        private TableLayoutPanel tableBottom;
        private Panel panelDaily;
        private DateTimePicker dtpDia;
        private Label lblTotalDia;
        private DataGridView dgvLowStock;
        private Chart chartCategorias;
        private Chart chartVendedores;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new Container();
            panelTabs = new Panel();
            btnVendedores = new Button();
            btnProductos = new Button();
            cmbPeriodo = new ComboBox();
            cmbCategoria = new ComboBox();
            dgvResultados = new DataGridView();
            panelBottom = new Panel();
            tableBottom = new TableLayoutPanel();
            panelDaily = new Panel();
            dtpDia = new DateTimePicker();
            lblTotalDia = new Label();
            dgvLowStock = new DataGridView();
            chartCategorias = new Chart();
            chartVendedores = new Chart();

            panelTabs.SuspendLayout();
            ((ISupportInitialize)dgvResultados).BeginInit();
            ((ISupportInitialize)dgvLowStock).BeginInit();
            ((ISupportInitialize)chartCategorias).BeginInit();
            ((ISupportInitialize)chartVendedores).BeginInit();
            SuspendLayout();
            // 
            // panelTabs
            // 
            panelTabs.BackColor = SystemColors.Control;
            panelTabs.Controls.Add(btnVendedores);
            panelTabs.Controls.Add(btnProductos);
            panelTabs.Controls.Add(cmbPeriodo);
            panelTabs.Controls.Add(cmbCategoria);
            panelTabs.Dock = DockStyle.Top;
            panelTabs.Location = new Point(16, 16);
            panelTabs.Name = "panelTabs";
            panelTabs.Padding = new Padding(12);
            panelTabs.Size = new Size(1148, 64);
            panelTabs.TabIndex = 0;
            // 
            // btnVendedores
            // 
            btnVendedores.BackColor = SystemColors.ControlLight;
            btnVendedores.FlatAppearance.BorderSize = 0;
            btnVendedores.FlatStyle = FlatStyle.Flat;
            btnVendedores.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnVendedores.Location = new Point(3, 3);
            btnVendedores.Name = "btnVendedores";
            btnVendedores.Size = new Size(160, 40);
            btnVendedores.TabIndex = 0;
            btnVendedores.Text = "Vendedores";
            btnVendedores.UseVisualStyleBackColor = false;
            // 
            // btnProductos
            // 
            btnProductos.BackColor = SystemColors.Control;
            btnProductos.FlatAppearance.BorderSize = 0;
            btnProductos.FlatStyle = FlatStyle.Flat;
            btnProductos.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            btnProductos.Location = new Point(169, 3);
            btnProductos.Name = "btnProductos";
            btnProductos.Size = new Size(160, 40);
            btnProductos.TabIndex = 1;
            btnProductos.Text = "Productos";
            btnProductos.UseVisualStyleBackColor = false;
            // 
            // cmbPeriodo
            // 
            cmbPeriodo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbPeriodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPeriodo.Items.AddRange(new object[] { "Día", "Mes", "Año" });
            cmbPeriodo.Location = new Point(980, 18);
            cmbPeriodo.Name = "cmbPeriodo";
            cmbPeriodo.Size = new Size(120, 23);
            cmbPeriodo.TabIndex = 2;
            // 
            // cmbCategoria
            // 
            cmbCategoria.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Location = new Point(820, 18);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(140, 23);
            cmbCategoria.TabIndex = 3;
            // 
            // dgvResultados
            // 
            dgvResultados.Dock = DockStyle.Top;
            dgvResultados.Height = 300;
            dgvResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResultados.Name = "dgvResultados";
            dgvResultados.TabIndex = 4;
            dgvResultados.ReadOnly = true;
            dgvResultados.AllowUserToAddRows = false;
            dgvResultados.AllowUserToDeleteRows = false;
            dgvResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResultados.MultiSelect = false;
            dgvResultados.RowHeadersVisible = false;
            dgvResultados.CellContentClick += new DataGridViewCellEventHandler(this.dgvResultados_CellContentClick);
            // 
            // panelBottom
            // 
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Height = 340;
            panelBottom.Padding = new Padding(8);
            panelBottom.Name = "panelBottom";
            panelBottom.Controls.Add(tableBottom);
            // 
            // tableBottom
            // 
            tableBottom.Dock = DockStyle.Fill;
            tableBottom.ColumnCount = 2;
            tableBottom.RowCount = 1;
            tableBottom.ColumnStyles.Clear();
            tableBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableBottom.RowStyles.Clear();
            tableBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // left: panelDaily or dgvLowStock; right: chart area (we add both charts to same cell and toggle visibility)
            tableBottom.Controls.Add(panelDaily, 0, 0);
            tableBottom.Controls.Add(dgvLowStock, 0, 0);
            tableBottom.Controls.Add(chartCategorias, 1, 0);
            tableBottom.Controls.Add(chartVendedores, 1, 0);
            // 
            // panelDaily
            // 
            panelDaily.Dock = DockStyle.Fill;
            panelDaily.Padding = new Padding(6);
            panelDaily.BackColor = SystemColors.ControlLight;
            // 
            // dtpDia
            // 
            dtpDia.Format = DateTimePickerFormat.Short;
            dtpDia.Dock = DockStyle.Top;
            // 
            // lblTotalDia
            // 
            lblTotalDia.Dock = DockStyle.Fill;
            lblTotalDia.TextAlign = ContentAlignment.MiddleCenter;
            lblTotalDia.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblTotalDia.Text = "Total del día:\n$0.00";
            panelDaily.Controls.Add(lblTotalDia);
            panelDaily.Controls.Add(dtpDia);
            // 
            // dgvLowStock
            // 
            dgvLowStock.Dock = DockStyle.Fill;
            dgvLowStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLowStock.ReadOnly = true;
            dgvLowStock.AllowUserToAddRows = false;
            dgvLowStock.AllowUserToDeleteRows = false;
            dgvLowStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLowStock.MultiSelect = false;
            dgvLowStock.RowHeadersVisible = false;
            dgvLowStock.Visible = false; // start hidden; shown in Productos view
            // 
            // chartCategorias
            // 
            chartCategorias.Dock = DockStyle.Fill;
            chartCategorias.Name = "chartCategorias";
            // 
            // chartVendedores
            // 
            chartVendedores.Dock = DockStyle.Fill;
            chartVendedores.Name = "chartVendedores";
            chartVendedores.Visible = false;
            // 
            // FrmAnalisisDatos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1180, 600);
            Controls.Add(panelBottom);
            Controls.Add(dgvResultados);
            Controls.Add(panelTabs);
            Name = "FrmAnalisisDatos";
            Padding = new Padding(16);
            Text = "Análisis de Datos";

            panelTabs.ResumeLayout(false);
            ((ISupportInitialize)dgvResultados).EndInit();
            ((ISupportInitialize)dgvLowStock).EndInit();
            ((ISupportInitialize)chartCategorias).EndInit();
            ((ISupportInitialize)chartVendedores).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}
