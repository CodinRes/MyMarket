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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.grpDatosCliente = new System.Windows.Forms.GroupBox();
            this.tableLayoutCliente = new System.Windows.Forms.TableLayoutPanel();
            this.lblCliente = new System.Windows.Forms.Label();
            this.cmbCliente = new System.Windows.Forms.ComboBox();
            this.lblMetodoPago = new System.Windows.Forms.Label();
            this.cmbMetodoPago = new System.Windows.Forms.ComboBox();
            this.btnGestionarMetodosPago = new System.Windows.Forms.Button();
            this.chkPagoEfectivo = new System.Windows.Forms.CheckBox();
            this.grpDetalle = new System.Windows.Forms.GroupBox();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.footerLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutTotales = new System.Windows.Forms.TableLayoutPanel();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.lblImpuestos = new System.Windows.Forms.Label();
            this.txtImpuestos = new System.Windows.Forms.TextBox();
            this.lblComision = new System.Windows.Forms.Label();
            this.txtComision = new System.Windows.Forms.TextBox();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.txtDescuento = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.panelBotones = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEmitirRecibo = new System.Windows.Forms.Button();
            this.btnQuitarItem = new System.Windows.Forms.Button();
            this.btnNuevoItem = new System.Windows.Forms.Button();
            this.mainLayout.SuspendLayout();
            this.grpDatosCliente.SuspendLayout();
            this.tableLayoutCliente.SuspendLayout();
            this.grpDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.footerLayout.SuspendLayout();
            this.tableLayoutTotales.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.grpDatosCliente, 0, 0);
            this.mainLayout.Controls.Add(this.grpDetalle, 0, 1);
            this.mainLayout.Controls.Add(this.footerLayout, 0, 2);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(16, 16);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 3;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.mainLayout.Size = new System.Drawing.Size(928, 568);
            this.mainLayout.TabIndex = 0;
            // 
            // grpDatosCliente
            // 
            this.grpDatosCliente.Controls.Add(this.tableLayoutCliente);
            this.grpDatosCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDatosCliente.Location = new System.Drawing.Point(0, 0);
            this.grpDatosCliente.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.grpDatosCliente.Name = "grpDatosCliente";
            this.grpDatosCliente.Padding = new System.Windows.Forms.Padding(12, 16, 12, 12);
            this.grpDatosCliente.Size = new System.Drawing.Size(928, 110);
            this.grpDatosCliente.TabIndex = 0;
            this.grpDatosCliente.TabStop = false;
            this.grpDatosCliente.Text = "Datos del cliente";
            // 
            // tableLayoutCliente
            // 
            this.tableLayoutCliente.ColumnCount = 5;
            this.tableLayoutCliente.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutCliente.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutCliente.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutCliente.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutCliente.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutCliente.Controls.Add(this.lblCliente, 0, 0);
            this.tableLayoutCliente.Controls.Add(this.cmbCliente, 1, 0);
            this.tableLayoutCliente.Controls.Add(this.lblMetodoPago, 2, 0);
            this.tableLayoutCliente.Controls.Add(this.cmbMetodoPago, 3, 0);
            this.tableLayoutCliente.Controls.Add(this.btnGestionarMetodosPago, 4, 0);
            this.tableLayoutCliente.Controls.Add(this.chkPagoEfectivo, 2, 1);
            this.tableLayoutCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutCliente.Location = new System.Drawing.Point(12, 32);
            this.tableLayoutCliente.Name = "tableLayoutCliente";
            this.tableLayoutCliente.RowCount = 2;
            this.tableLayoutCliente.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutCliente.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutCliente.Size = new System.Drawing.Size(904, 66);
            this.tableLayoutCliente.TabIndex = 0;
            // 
            // lblCliente
            // 
            this.lblCliente.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(3, 7);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(46, 15);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "Cliente";
            // 
            // cmbCliente
            // 
            this.cmbCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCliente.FormattingEnabled = true;
            this.cmbCliente.Location = new System.Drawing.Point(123, 3);
            this.cmbCliente.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.cmbCliente.Name = "cmbCliente";
            this.cmbCliente.Size = new System.Drawing.Size(256, 23);
            this.cmbCliente.TabIndex = 1;
            // 
            // lblMetodoPago
            // 
            this.lblMetodoPago.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMetodoPago.AutoSize = true;
            this.lblMetodoPago.Location = new System.Drawing.Point(395, 7);
            this.lblMetodoPago.Name = "lblMetodoPago";
            this.lblMetodoPago.Size = new System.Drawing.Size(99, 15);
            this.lblMetodoPago.TabIndex = 2;
            this.lblMetodoPago.Text = "Método de pago";
            // 
            // cmbMetodoPago
            // 
            this.cmbMetodoPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMetodoPago.FormattingEnabled = true;
            this.cmbMetodoPago.Location = new System.Drawing.Point(535, 3);
            this.cmbMetodoPago.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.cmbMetodoPago.Name = "cmbMetodoPago";
            this.cmbMetodoPago.Size = new System.Drawing.Size(207, 23);
            this.cmbMetodoPago.TabIndex = 3;
            // 
            // btnGestionarMetodosPago
            // 
            this.btnGestionarMetodosPago.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
            this.btnGestionarMetodosPago.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGestionarMetodosPago.FlatAppearance.BorderSize = 0;
            this.btnGestionarMetodosPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionarMetodosPago.ForeColor = System.Drawing.Color.White;
            this.btnGestionarMetodosPago.Location = new System.Drawing.Point(757, 3);
            this.btnGestionarMetodosPago.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.btnGestionarMetodosPago.Name = "btnGestionarMetodosPago";
            this.btnGestionarMetodosPago.Size = new System.Drawing.Size(147, 23);
            this.btnGestionarMetodosPago.TabIndex = 4;
            this.btnGestionarMetodosPago.Text = "Gestionar métodos";
            this.btnGestionarMetodosPago.UseVisualStyleBackColor = false;
            // 
            // chkPagoEfectivo
            // 
            this.chkPagoEfectivo.AutoSize = true;
            this.tableLayoutCliente.SetColumnSpan(this.chkPagoEfectivo, 3);
            this.chkPagoEfectivo.Location = new System.Drawing.Point(395, 32);
            this.chkPagoEfectivo.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.chkPagoEfectivo.Name = "chkPagoEfectivo";
            this.chkPagoEfectivo.Size = new System.Drawing.Size(122, 19);
            this.chkPagoEfectivo.TabIndex = 5;
            this.chkPagoEfectivo.Text = "Pago en efectivo";
            this.chkPagoEfectivo.UseVisualStyleBackColor = true;
            // 
            // grpDetalle
            // 
            this.grpDetalle.Controls.Add(this.dgvDetalle);
            this.grpDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDetalle.Location = new System.Drawing.Point(0, 126);
            this.grpDetalle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.grpDetalle.Name = "grpDetalle";
            this.grpDetalle.Padding = new System.Windows.Forms.Padding(12, 16, 12, 12);
            this.grpDetalle.Size = new System.Drawing.Size(928, 426);
            this.grpDetalle.TabIndex = 1;
            this.grpDetalle.TabStop = false;
            this.grpDetalle.Text = "Detalle de productos y servicios";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.dgvDetalle.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalle.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetalle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDetalle.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDetalle.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(45, 50, 55);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(45, 45, 45);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(230, 240, 250);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDetalle.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalle.EnableHeadersVisualStyles = false;
            this.dgvDetalle.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);
            this.dgvDetalle.Location = new System.Drawing.Point(12, 32);
            this.dgvDetalle.MultiSelect = false;
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.RowHeadersVisible = false;
            this.dgvDetalle.RowTemplate.Height = 28;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(904, 382);
            this.dgvDetalle.TabIndex = 0;
            // 
            // footerLayout
            // 
            this.footerLayout.AutoSize = true;
            this.footerLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.footerLayout.ColumnCount = 2;
            this.footerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.footerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.footerLayout.Controls.Add(this.tableLayoutTotales, 0, 0);
            this.footerLayout.Controls.Add(this.panelBotones, 1, 0);
            this.footerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.footerLayout.Location = new System.Drawing.Point(0, 568);
            this.footerLayout.Margin = new System.Windows.Forms.Padding(0);
            this.footerLayout.Name = "footerLayout";
            this.footerLayout.RowCount = 1;
            this.footerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.footerLayout.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.footerLayout.Size = new System.Drawing.Size(928, 0);
            this.footerLayout.TabIndex = 2;
            // 
            // tableLayoutTotales
            // 
            this.tableLayoutTotales.AutoSize = true;
            this.tableLayoutTotales.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutTotales.ColumnCount = 2;
            this.tableLayoutTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutTotales.Controls.Add(this.lblSubtotal, 0, 0);
            this.tableLayoutTotales.Controls.Add(this.txtSubtotal, 1, 0);
            this.tableLayoutTotales.Controls.Add(this.lblImpuestos, 0, 1);
            this.tableLayoutTotales.Controls.Add(this.txtImpuestos, 1, 1);
            this.tableLayoutTotales.Controls.Add(this.lblComision, 0, 2);
            this.tableLayoutTotales.Controls.Add(this.txtComision, 1, 2);
            this.tableLayoutTotales.Controls.Add(this.lblDescuento, 0, 3);
            this.tableLayoutTotales.Controls.Add(this.txtDescuento, 1, 3);
            this.tableLayoutTotales.Controls.Add(this.lblTotal, 0, 4);
            this.tableLayoutTotales.Controls.Add(this.txtTotal, 1, 4);
            this.tableLayoutTotales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutTotales.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutTotales.Margin = new System.Windows.Forms.Padding(0, 0, 24, 0);
            this.tableLayoutTotales.Name = "tableLayoutTotales";
            this.tableLayoutTotales.RowCount = 5;
            this.tableLayoutTotales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutTotales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutTotales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutTotales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutTotales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutTotales.Size = new System.Drawing.Size(556, 1);
            this.tableLayoutTotales.TabIndex = 0;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(3, 0);
            this.lblSubtotal.Margin = new System.Windows.Forms.Padding(3, 0, 12, 8);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(55, 15);
            this.lblSubtotal.TabIndex = 0;
            this.lblSubtotal.Text = "Subtotal";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubtotal.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.txtSubtotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSubtotal.Location = new System.Drawing.Point(73, 0);
            this.txtSubtotal.Margin = new System.Windows.Forms.Padding(3, 0, 0, 8);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.PlaceholderText = "$ 0,00";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(483, 23);
            this.txtSubtotal.TabIndex = 1;
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblImpuestos
            // 
            this.lblImpuestos.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblImpuestos.AutoSize = true;
            this.lblImpuestos.Location = new System.Drawing.Point(3, 23);
            this.lblImpuestos.Margin = new System.Windows.Forms.Padding(3, 0, 12, 8);
            this.lblImpuestos.Name = "lblImpuestos";
            this.lblImpuestos.Size = new System.Drawing.Size(64, 15);
            this.lblImpuestos.TabIndex = 2;
            this.lblImpuestos.Text = "Impuestos";
            // 
            // txtImpuestos
            // 
            this.txtImpuestos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImpuestos.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.txtImpuestos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtImpuestos.Location = new System.Drawing.Point(73, 23);
            this.txtImpuestos.Margin = new System.Windows.Forms.Padding(3, 0, 0, 8);
            this.txtImpuestos.Name = "txtImpuestos";
            this.txtImpuestos.PlaceholderText = "$ 0,00";
            this.txtImpuestos.ReadOnly = true;
            this.txtImpuestos.Size = new System.Drawing.Size(483, 23);
            this.txtImpuestos.TabIndex = 3;
            this.txtImpuestos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblComision
            // 
            this.lblComision.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblComision.AutoSize = true;
            this.lblComision.Location = new System.Drawing.Point(3, 46);
            this.lblComision.Margin = new System.Windows.Forms.Padding(3, 0, 12, 8);
            this.lblComision.Name = "lblComision";
            this.lblComision.Size = new System.Drawing.Size(59, 15);
            this.lblComision.TabIndex = 4;
            this.lblComision.Text = "Comisión";
            // 
            // txtComision
            // 
            this.txtComision.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComision.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.txtComision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtComision.Location = new System.Drawing.Point(73, 46);
            this.txtComision.Margin = new System.Windows.Forms.Padding(3, 0, 0, 8);
            this.txtComision.Name = "txtComision";
            this.txtComision.PlaceholderText = "$ 0,00";
            this.txtComision.ReadOnly = true;
            this.txtComision.Size = new System.Drawing.Size(483, 23);
            this.txtComision.TabIndex = 5;
            this.txtComision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDescuento
            // 
            this.lblDescuento.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.Location = new System.Drawing.Point(3, 69);
            this.lblDescuento.Margin = new System.Windows.Forms.Padding(3, 0, 12, 8);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(63, 15);
            this.lblDescuento.TabIndex = 6;
            this.lblDescuento.Text = "Descuento";
            // 
            // txtDescuento
            // 
            this.txtDescuento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescuento.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.txtDescuento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescuento.Location = new System.Drawing.Point(73, 69);
            this.txtDescuento.Margin = new System.Windows.Forms.Padding(3, 0, 0, 8);
            this.txtDescuento.Name = "txtDescuento";
            this.txtDescuento.PlaceholderText = "$ 0,00";
            this.txtDescuento.ReadOnly = true;
            this.txtDescuento.Size = new System.Drawing.Size(483, 23);
            this.txtDescuento.TabIndex = 7;
            this.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.Location = new System.Drawing.Point(3, 92);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(3, 0, 12, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(36, 17);
            this.lblTotal.TabIndex = 8;
            this.lblTotal.Text = "Total";
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtTotal.Location = new System.Drawing.Point(73, 92);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.PlaceholderText = "$ 0,00";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(483, 25);
            this.txtTotal.TabIndex = 9;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panelBotones
            // 
            this.panelBotones.AutoSize = true;
            this.panelBotones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelBotones.Controls.Add(this.btnEmitirRecibo);
            this.panelBotones.Controls.Add(this.btnQuitarItem);
            this.panelBotones.Controls.Add(this.btnNuevoItem);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelBotones.Location = new System.Drawing.Point(580, 0);
            this.panelBotones.Margin = new System.Windows.Forms.Padding(0);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelBotones.Size = new System.Drawing.Size(348, 1);
            this.panelBotones.TabIndex = 1;
            this.panelBotones.WrapContents = false;
            // 
            // btnEmitirRecibo
            // 
            this.btnEmitirRecibo.BackColor = System.Drawing.Color.FromArgb(45, 50, 55);
            this.btnEmitirRecibo.FlatAppearance.BorderSize = 0;
            this.btnEmitirRecibo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmitirRecibo.ForeColor = System.Drawing.Color.White;
            this.btnEmitirRecibo.Location = new System.Drawing.Point(228, 8);
            this.btnEmitirRecibo.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnEmitirRecibo.Name = "btnEmitirRecibo";
            this.btnEmitirRecibo.Size = new System.Drawing.Size(120, 36);
            this.btnEmitirRecibo.TabIndex = 2;
            this.btnEmitirRecibo.Text = "Emitir recibo";
            this.btnEmitirRecibo.UseVisualStyleBackColor = false;
            // 
            // btnQuitarItem
            // 
            this.btnQuitarItem.BackColor = System.Drawing.Color.FromArgb(100, 110, 120);
            this.btnQuitarItem.FlatAppearance.BorderSize = 0;
            this.btnQuitarItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitarItem.ForeColor = System.Drawing.Color.White;
            this.btnQuitarItem.Location = new System.Drawing.Point(102, 8);
            this.btnQuitarItem.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnQuitarItem.Name = "btnQuitarItem";
            this.btnQuitarItem.Size = new System.Drawing.Size(120, 36);
            this.btnQuitarItem.TabIndex = 1;
            this.btnQuitarItem.Text = "Quitar ítem";
            this.btnQuitarItem.UseVisualStyleBackColor = false;
            // 
            // btnNuevoItem
            // 
            this.btnNuevoItem.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
            this.btnNuevoItem.FlatAppearance.BorderSize = 0;
            this.btnNuevoItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoItem.ForeColor = System.Drawing.Color.White;
            this.btnNuevoItem.Location = new System.Drawing.Point(0, 8);
            this.btnNuevoItem.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnNuevoItem.Name = "btnNuevoItem";
            this.btnNuevoItem.Size = new System.Drawing.Size(120, 36);
            this.btnNuevoItem.TabIndex = 0;
            this.btnNuevoItem.Text = "Nuevo ítem";
            this.btnNuevoItem.UseVisualStyleBackColor = false;
            // 
            // FrmEmitirRecibo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(960, 600);
            this.Controls.Add(this.mainLayout);
            this.Name = "FrmEmitirRecibo";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.Text = "Emitir recibo";
            this.mainLayout.ResumeLayout(false);
            this.grpDatosCliente.ResumeLayout(false);
            this.tableLayoutCliente.ResumeLayout(false);
            this.tableLayoutCliente.PerformLayout();
            this.grpDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.footerLayout.ResumeLayout(false);
            this.tableLayoutTotales.ResumeLayout(false);
            this.tableLayoutTotales.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.GroupBox grpDatosCliente;
        private System.Windows.Forms.TableLayoutPanel tableLayoutCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cmbCliente;
        private System.Windows.Forms.Label lblMetodoPago;
        private System.Windows.Forms.ComboBox cmbMetodoPago;
        private System.Windows.Forms.Button btnGestionarMetodosPago;
        private System.Windows.Forms.GroupBox grpDetalle;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.TableLayoutPanel footerLayout;
        private System.Windows.Forms.TableLayoutPanel tableLayoutTotales;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label lblImpuestos;
        private System.Windows.Forms.TextBox txtImpuestos;
        private System.Windows.Forms.Label lblComision;
        private System.Windows.Forms.TextBox txtComision;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.TextBox txtDescuento;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.FlowLayoutPanel panelBotones;
        private System.Windows.Forms.Button btnEmitirRecibo;
        private System.Windows.Forms.Button btnQuitarItem;
        private System.Windows.Forms.Button btnNuevoItem;
        private System.Windows.Forms.CheckBox chkPagoEfectivo;
}
