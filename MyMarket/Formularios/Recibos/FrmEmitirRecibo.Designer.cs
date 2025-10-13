namespace MyMarket.Formularios.Recibos;

partial class FrmEmitirRecibo
{
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
        mainLayout = new TableLayoutPanel();
        grpDatosCliente = new GroupBox();
        tableLayoutCliente = new TableLayoutPanel();
        lblCliente = new Label();
        cmbCliente = new ComboBox();
        lblMetodoPago = new Label();
        cmbMetodoPago = new ComboBox();
        lblObservaciones = new Label();
        txtObservaciones = new TextBox();
        grpDetalle = new GroupBox();
        dgvDetalle = new DataGridView();
        footerLayout = new TableLayoutPanel();
        tableLayoutTotales = new TableLayoutPanel();
        lblSubtotal = new Label();
        txtSubtotal = new TextBox();
        lblImpuestos = new Label();
        txtImpuestos = new TextBox();
        lblTotal = new Label();
        txtTotal = new TextBox();
        panelBotones = new FlowLayoutPanel();
        btnEmitirRecibo = new Button();
        btnQuitarItem = new Button();
        btnNuevoItem = new Button();
        mainLayout.SuspendLayout();
        grpDatosCliente.SuspendLayout();
        tableLayoutCliente.SuspendLayout();
        grpDetalle.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
        footerLayout.SuspendLayout();
        tableLayoutTotales.SuspendLayout();
        panelBotones.SuspendLayout();
        SuspendLayout();
        // 
        // mainLayout
        // 
        mainLayout.ColumnCount = 1;
        mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        mainLayout.Controls.Add(grpDatosCliente, 0, 0);
        mainLayout.Controls.Add(grpDetalle, 0, 1);
        mainLayout.Controls.Add(footerLayout, 0, 2);
        mainLayout.Dock = DockStyle.Fill;
        mainLayout.Location = new Point(16, 16);
        mainLayout.Name = "mainLayout";
        mainLayout.RowCount = 3;
        mainLayout.RowStyles.Add(new RowStyle());
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        mainLayout.RowStyles.Add(new RowStyle());
        mainLayout.Size = new Size(928, 568);
        mainLayout.TabIndex = 0;
        // 
        // grpDatosCliente
        // 
        grpDatosCliente.Controls.Add(tableLayoutCliente);
        grpDatosCliente.Dock = DockStyle.Fill;
        grpDatosCliente.Location = new Point(0, 0);
        grpDatosCliente.Margin = new Padding(0, 0, 0, 16);
        grpDatosCliente.Name = "grpDatosCliente";
        grpDatosCliente.Padding = new Padding(12, 16, 12, 12);
        grpDatosCliente.Size = new Size(928, 150);
        grpDatosCliente.TabIndex = 0;
        grpDatosCliente.TabStop = false;
        grpDatosCliente.Text = "Datos del cliente";
        // 
        // tableLayoutCliente
        // 
        tableLayoutCliente.ColumnCount = 4;
        tableLayoutCliente.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
        tableLayoutCliente.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        tableLayoutCliente.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
        tableLayoutCliente.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
        tableLayoutCliente.Controls.Add(lblCliente, 0, 0);
        tableLayoutCliente.Controls.Add(cmbCliente, 1, 0);
        tableLayoutCliente.Controls.Add(lblMetodoPago, 2, 0);
        tableLayoutCliente.Controls.Add(cmbMetodoPago, 3, 0);
        tableLayoutCliente.Controls.Add(lblObservaciones, 0, 1);
        tableLayoutCliente.Controls.Add(txtObservaciones, 1, 1);
        tableLayoutCliente.Dock = DockStyle.Fill;
        tableLayoutCliente.Location = new Point(12, 32);
        tableLayoutCliente.Name = "tableLayoutCliente";
        tableLayoutCliente.RowCount = 2;
        tableLayoutCliente.RowStyles.Add(new RowStyle());
        tableLayoutCliente.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
        tableLayoutCliente.Size = new Size(904, 106);
        tableLayoutCliente.TabIndex = 0;
        // 
        // lblCliente
        // 
        lblCliente.Anchor = AnchorStyles.Left;
        lblCliente.AutoSize = true;
        lblCliente.Location = new Point(3, 7);
        lblCliente.Name = "lblCliente";
        lblCliente.Size = new Size(44, 15);
        lblCliente.TabIndex = 0;
        lblCliente.Text = "Cliente";
        // 
        // cmbCliente
        // 
        cmbCliente.Dock = DockStyle.Fill;
        cmbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbCliente.FormattingEnabled = true;
        cmbCliente.Location = new Point(123, 3);
        cmbCliente.Margin = new Padding(3, 3, 12, 3);
        cmbCliente.Name = "cmbCliente";
        cmbCliente.Size = new Size(210, 23);
        cmbCliente.TabIndex = 1;
        // 
        // lblMetodoPago
        // 
        lblMetodoPago.Anchor = AnchorStyles.Left;
        lblMetodoPago.AutoSize = true;
        lblMetodoPago.Location = new Point(348, 7);
        lblMetodoPago.Name = "lblMetodoPago";
        lblMetodoPago.Size = new Size(95, 15);
        lblMetodoPago.TabIndex = 2;
        lblMetodoPago.Text = "Método de pago";
        // 
        // cmbMetodoPago
        // 
        cmbMetodoPago.Dock = DockStyle.Fill;
        cmbMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbMetodoPago.FormattingEnabled = true;
        cmbMetodoPago.Location = new Point(488, 3);
        cmbMetodoPago.Margin = new Padding(3, 3, 12, 3);
        cmbMetodoPago.Name = "cmbMetodoPago";
        cmbMetodoPago.Size = new Size(404, 23);
        cmbMetodoPago.TabIndex = 3;
        // 
        // lblObservaciones
        // 
        lblObservaciones.Anchor = AnchorStyles.Left;
        lblObservaciones.AutoSize = true;
        lblObservaciones.Location = new Point(3, 61);
        lblObservaciones.Name = "lblObservaciones";
        lblObservaciones.Size = new Size(84, 15);
        lblObservaciones.TabIndex = 4;
        lblObservaciones.Text = "Observaciones";
        // 
        // txtObservaciones
        // 
        tableLayoutCliente.SetColumnSpan(txtObservaciones, 3);
        txtObservaciones.Dock = DockStyle.Fill;
        txtObservaciones.Location = new Point(123, 35);
        txtObservaciones.Margin = new Padding(3, 6, 12, 3);
        txtObservaciones.Multiline = true;
        txtObservaciones.Name = "txtObservaciones";
        txtObservaciones.PlaceholderText = "Notas internas u observaciones para el recibo";
        txtObservaciones.ScrollBars = ScrollBars.Vertical;
        txtObservaciones.Size = new Size(769, 71);
        txtObservaciones.TabIndex = 5;
        // 
        // grpDetalle
        // 
        grpDetalle.Controls.Add(dgvDetalle);
        grpDetalle.Dock = DockStyle.Fill;
        grpDetalle.Location = new Point(0, 166);
        grpDetalle.Margin = new Padding(0, 0, 0, 16);
        grpDetalle.Name = "grpDetalle";
        grpDetalle.Padding = new Padding(12, 16, 12, 12);
        grpDetalle.Size = new Size(928, 287);
        grpDetalle.TabIndex = 1;
        grpDetalle.TabStop = false;
        grpDetalle.Text = "Detalle de productos y servicios";
        // 
        // dgvDetalle
        // 
        dgvDetalle.AllowUserToAddRows = false;
        dgvDetalle.AllowUserToDeleteRows = false;
        dgvDetalle.AllowUserToResizeRows = false;
        dataGridViewCellStyle7.BackColor = Color.FromArgb(245, 245, 245);
        dgvDetalle.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
        dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDetalle.BackgroundColor = Color.White;
        dgvDetalle.BorderStyle = BorderStyle.None;
        dgvDetalle.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvDetalle.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle8.BackColor = Color.FromArgb(55, 130, 200);
        dataGridViewCellStyle8.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle8.ForeColor = Color.White;
        dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(45, 50, 55);
        dataGridViewCellStyle8.SelectionForeColor = Color.White;
        dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
        dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
        dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle9.BackColor = Color.White;
        dataGridViewCellStyle9.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle9.ForeColor = Color.FromArgb(45, 45, 45);
        dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(230, 240, 250);
        dataGridViewCellStyle9.SelectionForeColor = Color.Black;
        dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
        dgvDetalle.DefaultCellStyle = dataGridViewCellStyle9;
        dgvDetalle.Dock = DockStyle.Fill;
        dgvDetalle.EnableHeadersVisualStyles = false;
        dgvDetalle.GridColor = Color.FromArgb(230, 230, 230);
        dgvDetalle.Location = new Point(12, 32);
        dgvDetalle.MultiSelect = false;
        dgvDetalle.Name = "dgvDetalle";
        dgvDetalle.ReadOnly = true;
        dgvDetalle.RowHeadersVisible = false;
        dgvDetalle.RowTemplate.Height = 28;
        dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDetalle.Size = new Size(904, 243);
        dgvDetalle.TabIndex = 0;
        // 
        // footerLayout
        // 
        footerLayout.AutoSize = true;
        footerLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        footerLayout.ColumnCount = 2;
        footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
        footerLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
        footerLayout.Controls.Add(tableLayoutTotales, 0, 0);
        footerLayout.Controls.Add(panelBotones, 1, 0);
        footerLayout.Dock = DockStyle.Fill;
        footerLayout.Location = new Point(0, 469);
        footerLayout.Margin = new Padding(0);
        footerLayout.Name = "footerLayout";
        footerLayout.Padding = new Padding(0, 12, 0, 0);
        footerLayout.RowCount = 1;
        footerLayout.RowStyles.Add(new RowStyle());
        footerLayout.Size = new Size(928, 99);
        footerLayout.TabIndex = 2;
        // 
        // tableLayoutTotales
        // 
        tableLayoutTotales.AutoSize = true;
        tableLayoutTotales.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        tableLayoutTotales.ColumnCount = 2;
        tableLayoutTotales.ColumnStyles.Add(new ColumnStyle());
        tableLayoutTotales.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutTotales.Controls.Add(lblSubtotal, 0, 0);
        tableLayoutTotales.Controls.Add(txtSubtotal, 1, 0);
        tableLayoutTotales.Controls.Add(lblImpuestos, 0, 1);
        tableLayoutTotales.Controls.Add(txtImpuestos, 1, 1);
        tableLayoutTotales.Controls.Add(lblTotal, 0, 2);
        tableLayoutTotales.Controls.Add(txtTotal, 1, 2);
        tableLayoutTotales.Dock = DockStyle.Fill;
        tableLayoutTotales.Location = new Point(0, 12);
        tableLayoutTotales.Margin = new Padding(0, 0, 24, 0);
        tableLayoutTotales.Name = "tableLayoutTotales";
        tableLayoutTotales.RowCount = 3;
        tableLayoutTotales.RowStyles.Add(new RowStyle());
        tableLayoutTotales.RowStyles.Add(new RowStyle());
        tableLayoutTotales.RowStyles.Add(new RowStyle());
        tableLayoutTotales.Size = new Size(532, 87);
        tableLayoutTotales.TabIndex = 0;
        // 
        // lblSubtotal
        // 
        lblSubtotal.Anchor = AnchorStyles.Left;
        lblSubtotal.AutoSize = true;
        lblSubtotal.Location = new Point(3, 4);
        lblSubtotal.Margin = new Padding(3, 0, 12, 8);
        lblSubtotal.Name = "lblSubtotal";
        lblSubtotal.Size = new Size(51, 15);
        lblSubtotal.TabIndex = 0;
        lblSubtotal.Text = "Subtotal";
        // 
        // txtSubtotal
        // 
        txtSubtotal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtSubtotal.BackColor = Color.FromArgb(245, 245, 245);
        txtSubtotal.BorderStyle = BorderStyle.FixedSingle;
        txtSubtotal.Location = new Point(80, 0);
        txtSubtotal.Margin = new Padding(3, 0, 0, 8);
        txtSubtotal.Name = "txtSubtotal";
        txtSubtotal.PlaceholderText = "$ 0,00";
        txtSubtotal.ReadOnly = true;
        txtSubtotal.Size = new Size(452, 23);
        txtSubtotal.TabIndex = 1;
        txtSubtotal.TextAlign = HorizontalAlignment.Right;
        // 
        // lblImpuestos
        // 
        lblImpuestos.Anchor = AnchorStyles.Left;
        lblImpuestos.AutoSize = true;
        lblImpuestos.Location = new Point(3, 35);
        lblImpuestos.Margin = new Padding(3, 0, 12, 8);
        lblImpuestos.Name = "lblImpuestos";
        lblImpuestos.Size = new Size(62, 15);
        lblImpuestos.TabIndex = 2;
        lblImpuestos.Text = "Impuestos";
        // 
        // txtImpuestos
        // 
        txtImpuestos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtImpuestos.BackColor = Color.FromArgb(245, 245, 245);
        txtImpuestos.BorderStyle = BorderStyle.FixedSingle;
        txtImpuestos.Location = new Point(80, 31);
        txtImpuestos.Margin = new Padding(3, 0, 0, 8);
        txtImpuestos.Name = "txtImpuestos";
        txtImpuestos.PlaceholderText = "$ 0,00";
        txtImpuestos.ReadOnly = true;
        txtImpuestos.Size = new Size(452, 23);
        txtImpuestos.TabIndex = 3;
        txtImpuestos.TextAlign = HorizontalAlignment.Right;
        // 
        // lblTotal
        // 
        lblTotal.Anchor = AnchorStyles.Left;
        lblTotal.AutoSize = true;
        lblTotal.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold);
        lblTotal.Location = new Point(3, 66);
        lblTotal.Margin = new Padding(3, 0, 12, 0);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(37, 17);
        lblTotal.TabIndex = 4;
        lblTotal.Text = "Total";
        // 
        // txtTotal
        // 
        txtTotal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTotal.BackColor = Color.FromArgb(245, 245, 245);
        txtTotal.BorderStyle = BorderStyle.FixedSingle;
        txtTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        txtTotal.Location = new Point(80, 62);
        txtTotal.Margin = new Padding(3, 0, 0, 0);
        txtTotal.Name = "txtTotal";
        txtTotal.PlaceholderText = "$ 0,00";
        txtTotal.ReadOnly = true;
        txtTotal.Size = new Size(452, 25);
        txtTotal.TabIndex = 5;
        txtTotal.TextAlign = HorizontalAlignment.Right;
        // 
        // panelBotones
        // 
        panelBotones.AutoSize = true;
        panelBotones.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        panelBotones.Controls.Add(btnEmitirRecibo);
        panelBotones.Controls.Add(btnQuitarItem);
        panelBotones.Controls.Add(btnNuevoItem);
        panelBotones.Dock = DockStyle.Fill;
        panelBotones.FlowDirection = FlowDirection.RightToLeft;
        panelBotones.Location = new Point(556, 12);
        panelBotones.Margin = new Padding(0);
        panelBotones.Name = "panelBotones";
        panelBotones.Padding = new Padding(0, 8, 0, 0);
        panelBotones.Size = new Size(372, 87);
        panelBotones.TabIndex = 1;
        panelBotones.WrapContents = false;
        panelBotones.Paint += panelBotones_Paint;
        // 
        // btnEmitirRecibo
        // 
        btnEmitirRecibo.BackColor = Color.FromArgb(45, 50, 55);
        btnEmitirRecibo.FlatAppearance.BorderSize = 0;
        btnEmitirRecibo.FlatStyle = FlatStyle.Flat;
        btnEmitirRecibo.ForeColor = Color.White;
        btnEmitirRecibo.Location = new Point(273, 8);
        btnEmitirRecibo.Margin = new Padding(6, 0, 0, 0);
        btnEmitirRecibo.Name = "btnEmitirRecibo";
        btnEmitirRecibo.Size = new Size(99, 36);
        btnEmitirRecibo.TabIndex = 2;
        btnEmitirRecibo.Text = "Emitir recibo";
        btnEmitirRecibo.UseVisualStyleBackColor = false;
        // 
        // btnQuitarItem
        // 
        btnQuitarItem.BackColor = Color.FromArgb(100, 110, 120);
        btnQuitarItem.FlatAppearance.BorderSize = 0;
        btnQuitarItem.FlatStyle = FlatStyle.Flat;
        btnQuitarItem.ForeColor = Color.White;
        btnQuitarItem.Location = new Point(172, 8);
        btnQuitarItem.Margin = new Padding(6, 0, 0, 0);
        btnQuitarItem.Name = "btnQuitarItem";
        btnQuitarItem.Size = new Size(95, 36);
        btnQuitarItem.TabIndex = 1;
        btnQuitarItem.Text = "Quitar ítem";
        btnQuitarItem.UseVisualStyleBackColor = false;
        // 
        // btnNuevoItem
        // 
        btnNuevoItem.BackColor = Color.FromArgb(55, 130, 200);
        btnNuevoItem.FlatAppearance.BorderSize = 0;
        btnNuevoItem.FlatStyle = FlatStyle.Flat;
        btnNuevoItem.ForeColor = Color.White;
        btnNuevoItem.Location = new Point(62, 8);
        btnNuevoItem.Margin = new Padding(6, 0, 0, 0);
        btnNuevoItem.Name = "btnNuevoItem";
        btnNuevoItem.Size = new Size(104, 36);
        btnNuevoItem.TabIndex = 0;
        btnNuevoItem.Text = "Nuevo ítem";
        btnNuevoItem.UseVisualStyleBackColor = false;
        // 
        // FrmEmitirRecibo
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(960, 600);
        Controls.Add(mainLayout);
        Name = "FrmEmitirRecibo";
        Padding = new Padding(16);
        Text = "Emitir recibo";
        mainLayout.ResumeLayout(false);
        mainLayout.PerformLayout();
        grpDatosCliente.ResumeLayout(false);
        tableLayoutCliente.ResumeLayout(false);
        tableLayoutCliente.PerformLayout();
        grpDetalle.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
        footerLayout.ResumeLayout(false);
        footerLayout.PerformLayout();
        tableLayoutTotales.ResumeLayout(false);
        tableLayoutTotales.PerformLayout();
        panelBotones.ResumeLayout(false);
        ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.GroupBox grpDatosCliente;
        private System.Windows.Forms.TableLayoutPanel tableLayoutCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.Label lblMetodoPago;
        private System.Windows.Forms.ComboBox cmbMetodoPago;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.GroupBox grpDetalle;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.TableLayoutPanel footerLayout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutTotales;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label lblImpuestos;
        private System.Windows.Forms.TextBox txtImpuestos;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.FlowLayoutPanel panelBotones;
        private System.Windows.Forms.Button btnEmitirRecibo;
        private System.Windows.Forms.Button btnQuitarItem;
        private System.Windows.Forms.Button btnNuevoItem;
}
