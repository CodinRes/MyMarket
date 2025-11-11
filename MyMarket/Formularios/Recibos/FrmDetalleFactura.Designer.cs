namespace MyMarket.Formularios.Recibos;

partial class FrmDetalleFactura
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblNumeroFactura = new System.Windows.Forms.Label();
            this.lblFechaEmision = new System.Windows.Forms.Label();
            this.grpInformacion = new System.Windows.Forms.GroupBox();
            this.tableLayoutInfo = new System.Windows.Forms.TableLayoutPanel();
            this.labelCliente = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.labelDni = new System.Windows.Forms.Label();
            this.lblDni = new System.Windows.Forms.Label();
            this.labelEmpleado = new System.Windows.Forms.Label();
            this.lblEmpleado = new System.Windows.Forms.Label();
            this.labelMetodoPago = new System.Windows.Forms.Label();
            this.lblMetodoPago = new System.Windows.Forms.Label();
            this.labelEstado = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.tableLayoutTotales = new System.Windows.Forms.TableLayoutPanel();
            this.labelSubtotal = new System.Windows.Forms.Label();
            this.lblSubtotalValor = new System.Windows.Forms.Label();
            this.labelDescuento = new System.Windows.Forms.Label();
            this.lblDescuentoValor = new System.Windows.Forms.Label();
            this.labelComision = new System.Windows.Forms.Label();
            this.lblComisionValor = new System.Windows.Forms.Label();
            this.labelImpuestos = new System.Windows.Forms.Label();
            this.lblImpuestosValor = new System.Windows.Forms.Label();
            this.labelTotal = new System.Windows.Forms.Label();
            this.lblTotalValor = new System.Windows.Forms.Label();
            this.bindingSourceDetalle = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutRoot.SuspendLayout();
            this.tableLayoutHeader.SuspendLayout();
            this.grpInformacion.SuspendLayout();
            this.tableLayoutInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.tableLayoutTotales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutRoot
            // 
            this.tableLayoutRoot.ColumnCount = 1;
            this.tableLayoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.Controls.Add(this.tableLayoutHeader, 0, 0);
            this.tableLayoutRoot.Controls.Add(this.grpInformacion, 0, 1);
            this.tableLayoutRoot.Controls.Add(this.dgvDetalle, 0, 2);
            this.tableLayoutRoot.Controls.Add(this.tableLayoutTotales, 0, 3);
            this.tableLayoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRoot.Location = new System.Drawing.Point(16, 16);
            this.tableLayoutRoot.Name = "tableLayoutRoot";
            this.tableLayoutRoot.RowCount = 4;
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.Size = new System.Drawing.Size(808, 648);
            this.tableLayoutRoot.TabIndex = 0;
            // 
            // tableLayoutHeader
            // 
            this.tableLayoutHeader.ColumnCount = 2;
            this.tableLayoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutHeader.Controls.Add(this.lblTitulo, 0, 0);
            this.tableLayoutHeader.Controls.Add(this.lblNumeroFactura, 0, 1);
            this.tableLayoutHeader.Controls.Add(this.lblFechaEmision, 1, 1);
            this.tableLayoutHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutHeader.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutHeader.Name = "tableLayoutHeader";
            this.tableLayoutHeader.RowCount = 2;
            this.tableLayoutHeader.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutHeader.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutHeader.Size = new System.Drawing.Size(802, 74);
            this.tableLayoutHeader.TabIndex = 0;
            this.tableLayoutHeader.SetColumnSpan(this.lblTitulo, 2);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitulo.Location = new System.Drawing.Point(3, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblTitulo.Size = new System.Drawing.Size(251, 40);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "MyMarket - Recibo";
            // 
            // lblNumeroFactura
            // 
            this.lblNumeroFactura.AutoSize = true;
            this.lblNumeroFactura.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblNumeroFactura.Location = new System.Drawing.Point(3, 40);
            this.lblNumeroFactura.Name = "lblNumeroFactura";
            this.lblNumeroFactura.Size = new System.Drawing.Size(136, 20);
            this.lblNumeroFactura.TabIndex = 1;
            this.lblNumeroFactura.Text = "Factura #00000000";
            // 
            // lblFechaEmision
            // 
            this.lblFechaEmision.AutoSize = true;
            this.lblFechaEmision.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblFechaEmision.Location = new System.Drawing.Point(404, 40);
            this.lblFechaEmision.Name = "lblFechaEmision";
            this.lblFechaEmision.Size = new System.Drawing.Size(129, 20);
            this.lblFechaEmision.TabIndex = 2;
            this.lblFechaEmision.Text = "01/01/2024 12:00";
            this.lblFechaEmision.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFechaEmision.Anchor = System.Windows.Forms.AnchorStyles.Right;
            // 
            // grpInformacion
            // 
            this.grpInformacion.Controls.Add(this.tableLayoutInfo);
            this.grpInformacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInformacion.Location = new System.Drawing.Point(3, 83);
            this.grpInformacion.Name = "grpInformacion";
            this.grpInformacion.Padding = new System.Windows.Forms.Padding(12, 8, 12, 12);
            this.grpInformacion.Size = new System.Drawing.Size(802, 178);
            this.grpInformacion.TabIndex = 1;
            this.grpInformacion.TabStop = false;
            this.grpInformacion.Text = "Datos del comprobante";
            // 
            // tableLayoutInfo
            // 
            this.tableLayoutInfo.ColumnCount = 2;
            this.tableLayoutInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutInfo.Controls.Add(this.labelCliente, 0, 0);
            this.tableLayoutInfo.Controls.Add(this.lblCliente, 1, 0);
            this.tableLayoutInfo.Controls.Add(this.labelDni, 0, 1);
            this.tableLayoutInfo.Controls.Add(this.lblDni, 1, 1);
            this.tableLayoutInfo.Controls.Add(this.labelEmpleado, 0, 2);
            this.tableLayoutInfo.Controls.Add(this.lblEmpleado, 1, 2);
            this.tableLayoutInfo.Controls.Add(this.labelMetodoPago, 0, 3);
            this.tableLayoutInfo.Controls.Add(this.lblMetodoPago, 1, 3);
            this.tableLayoutInfo.Controls.Add(this.labelEstado, 0, 4);
            this.tableLayoutInfo.Controls.Add(this.lblEstado, 1, 4);
            this.tableLayoutInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutInfo.Location = new System.Drawing.Point(12, 24);
            this.tableLayoutInfo.Name = "tableLayoutInfo";
            this.tableLayoutInfo.RowCount = 5;
            this.tableLayoutInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutInfo.Size = new System.Drawing.Size(778, 142);
            this.tableLayoutInfo.TabIndex = 0;
            // 
            // labelCliente
            // 
            this.labelCliente.AutoSize = true;
            this.labelCliente.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCliente.Location = new System.Drawing.Point(3, 0);
            this.labelCliente.Margin = new System.Windows.Forms.Padding(3, 0, 8, 8);
            this.labelCliente.Name = "labelCliente";
            this.labelCliente.Size = new System.Drawing.Size(54, 19);
            this.labelCliente.TabIndex = 0;
            this.labelCliente.Text = "Cliente";
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCliente.Location = new System.Drawing.Point(68, 0);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(111, 19);
            this.lblCliente.TabIndex = 1;
            this.lblCliente.Text = "Cliente ocasional";
            // 
            // labelDni
            // 
            this.labelDni.AutoSize = true;
            this.labelDni.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelDni.Location = new System.Drawing.Point(3, 27);
            this.labelDni.Margin = new System.Windows.Forms.Padding(3, 0, 8, 8);
            this.labelDni.Name = "labelDni";
            this.labelDni.Size = new System.Drawing.Size(34, 19);
            this.labelDni.TabIndex = 2;
            this.labelDni.Text = "DNI";
            // 
            // lblDni
            // 
            this.lblDni.AutoSize = true;
            this.lblDni.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDni.Location = new System.Drawing.Point(68, 27);
            this.lblDni.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(87, 19);
            this.lblDni.TabIndex = 3;
            this.lblDni.Text = "No informado";
            // 
            // labelEmpleado
            // 
            this.labelEmpleado.AutoSize = true;
            this.labelEmpleado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelEmpleado.Location = new System.Drawing.Point(3, 54);
            this.labelEmpleado.Margin = new System.Windows.Forms.Padding(3, 0, 8, 8);
            this.labelEmpleado.Name = "labelEmpleado";
            this.labelEmpleado.Size = new System.Drawing.Size(74, 19);
            this.labelEmpleado.TabIndex = 4;
            this.labelEmpleado.Text = "Vendedor";
            // 
            // lblEmpleado
            // 
            this.lblEmpleado.AutoSize = true;
            this.lblEmpleado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblEmpleado.Location = new System.Drawing.Point(68, 54);
            this.lblEmpleado.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.lblEmpleado.Name = "lblEmpleado";
            this.lblEmpleado.Size = new System.Drawing.Size(45, 19);
            this.lblEmpleado.TabIndex = 5;
            this.lblEmpleado.Text = "-- --";
            // 
            // labelMetodoPago
            // 
            this.labelMetodoPago.AutoSize = true;
            this.labelMetodoPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelMetodoPago.Location = new System.Drawing.Point(3, 81);
            this.labelMetodoPago.Margin = new System.Windows.Forms.Padding(3, 0, 8, 8);
            this.labelMetodoPago.Name = "labelMetodoPago";
            this.labelMetodoPago.Size = new System.Drawing.Size(118, 19);
            this.labelMetodoPago.TabIndex = 6;
            this.labelMetodoPago.Text = "Método de pago";
            // 
            // lblMetodoPago
            // 
            this.lblMetodoPago.AutoSize = true;
            this.lblMetodoPago.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMetodoPago.Location = new System.Drawing.Point(68, 81);
            this.lblMetodoPago.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
            this.lblMetodoPago.Name = "lblMetodoPago";
            this.lblMetodoPago.Size = new System.Drawing.Size(50, 19);
            this.lblMetodoPago.TabIndex = 7;
            this.lblMetodoPago.Text = "-- --";
            // 
            // labelEstado
            // 
            this.labelEstado.AutoSize = true;
            this.labelEstado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelEstado.Location = new System.Drawing.Point(3, 108);
            this.labelEstado.Margin = new System.Windows.Forms.Padding(3, 0, 8, 0);
            this.labelEstado.Name = "labelEstado";
            this.labelEstado.Size = new System.Drawing.Size(54, 19);
            this.labelEstado.TabIndex = 8;
            this.labelEstado.Text = "Estado";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblEstado.Location = new System.Drawing.Point(68, 108);
            this.lblEstado.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(57, 19);
            this.lblEstado.TabIndex = 9;
            this.lblEstado.Text = "Pagada";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.AllowUserToResizeRows = false;
            this.dgvDetalle.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalle.Location = new System.Drawing.Point(3, 267);
            this.dgvDetalle.MultiSelect = false;
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.RowHeadersVisible = false;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(802, 314);
            this.dgvDetalle.TabIndex = 2;
            // 
            // tableLayoutTotales
            // 
            this.tableLayoutTotales.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tableLayoutTotales.AutoSize = true;
            this.tableLayoutTotales.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutTotales.ColumnCount = 2;
            this.tableLayoutTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutTotales.Controls.Add(this.labelSubtotal, 0, 0);
            this.tableLayoutTotales.Controls.Add(this.lblSubtotalValor, 1, 0);
            this.tableLayoutTotales.Controls.Add(this.labelDescuento, 0, 1);
            this.tableLayoutTotales.Controls.Add(this.lblDescuentoValor, 1, 1);
            this.tableLayoutTotales.Controls.Add(this.labelComision, 0, 2);
            this.tableLayoutTotales.Controls.Add(this.lblComisionValor, 1, 2);
            this.tableLayoutTotales.Controls.Add(this.labelImpuestos, 0, 3);
            this.tableLayoutTotales.Controls.Add(this.lblImpuestosValor, 1, 3);
            this.tableLayoutTotales.Controls.Add(this.labelTotal, 0, 4);
            this.tableLayoutTotales.Controls.Add(this.lblTotalValor, 1, 4);
            this.tableLayoutTotales.Location = new System.Drawing.Point(412, 587);
            this.tableLayoutTotales.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.tableLayoutTotales.Name = "tableLayoutTotales";
            this.tableLayoutTotales.RowCount = 5;
            this.tableLayoutTotales.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutTotales.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutTotales.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutTotales.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutTotales.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutTotales.Size = new System.Drawing.Size(393, 58);
            this.tableLayoutTotales.TabIndex = 3;
            // 
            // labelSubtotal
            // 
            this.labelSubtotal.AutoSize = true;
            this.labelSubtotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelSubtotal.Location = new System.Drawing.Point(3, 0);
            this.labelSubtotal.Margin = new System.Windows.Forms.Padding(3, 0, 16, 4);
            this.labelSubtotal.Name = "labelSubtotal";
            this.labelSubtotal.Size = new System.Drawing.Size(64, 19);
            this.labelSubtotal.TabIndex = 0;
            this.labelSubtotal.Text = "Subtotal";
            // 
            // lblSubtotalValor
            // 
            this.lblSubtotalValor.AutoSize = true;
            this.lblSubtotalValor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubtotalValor.Location = new System.Drawing.Point(86, 0);
            this.lblSubtotalValor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.lblSubtotalValor.Name = "lblSubtotalValor";
            this.lblSubtotalValor.Size = new System.Drawing.Size(49, 19);
            this.lblSubtotalValor.TabIndex = 1;
            this.lblSubtotalValor.Text = "$ 0,00";
            this.lblSubtotalValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDescuento
            // 
            this.labelDescuento.AutoSize = true;
            this.labelDescuento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelDescuento.Location = new System.Drawing.Point(3, 23);
            this.labelDescuento.Margin = new System.Windows.Forms.Padding(3, 0, 16, 4);
            this.labelDescuento.Name = "labelDescuento";
            this.labelDescuento.Size = new System.Drawing.Size(75, 19);
            this.labelDescuento.TabIndex = 2;
            this.labelDescuento.Text = "Descuento";
            // 
            // lblDescuentoValor
            // 
            this.lblDescuentoValor.AutoSize = true;
            this.lblDescuentoValor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDescuentoValor.Location = new System.Drawing.Point(86, 23);
            this.lblDescuentoValor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.lblDescuentoValor.Name = "lblDescuentoValor";
            this.lblDescuentoValor.Size = new System.Drawing.Size(49, 19);
            this.lblDescuentoValor.TabIndex = 3;
            this.lblDescuentoValor.Text = "$ 0,00";
            this.lblDescuentoValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelComision
            // 
            this.labelComision.AutoSize = true;
            this.labelComision.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelComision.Location = new System.Drawing.Point(3, 46);
            this.labelComision.Margin = new System.Windows.Forms.Padding(3, 0, 16, 4);
            this.labelComision.Name = "labelComision";
            this.labelComision.Size = new System.Drawing.Size(66, 19);
            this.labelComision.TabIndex = 4;
            this.labelComision.Text = "Comisión";
            // 
            // lblComisionValor
            // 
            this.lblComisionValor.AutoSize = true;
            this.lblComisionValor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblComisionValor.Location = new System.Drawing.Point(86, 46);
            this.lblComisionValor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.lblComisionValor.Name = "lblComisionValor";
            this.lblComisionValor.Size = new System.Drawing.Size(49, 19);
            this.lblComisionValor.TabIndex = 5;
            this.lblComisionValor.Text = "$ 0,00";
            this.lblComisionValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelImpuestos
            // 
            this.labelImpuestos.AutoSize = true;
            this.labelImpuestos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelImpuestos.Location = new System.Drawing.Point(3, 69);
            this.labelImpuestos.Margin = new System.Windows.Forms.Padding(3, 0, 16, 4);
            this.labelImpuestos.Name = "labelImpuestos";
            this.labelImpuestos.Size = new System.Drawing.Size(73, 19);
            this.labelImpuestos.TabIndex = 6;
            this.labelImpuestos.Text = "Impuestos";
            // 
            // lblImpuestosValor
            // 
            this.lblImpuestosValor.AutoSize = true;
            this.lblImpuestosValor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblImpuestosValor.Location = new System.Drawing.Point(86, 69);
            this.lblImpuestosValor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 4);
            this.lblImpuestosValor.Name = "lblImpuestosValor";
            this.lblImpuestosValor.Size = new System.Drawing.Size(49, 19);
            this.lblImpuestosValor.TabIndex = 7;
            this.lblImpuestosValor.Text = "$ 0,00";
            this.lblImpuestosValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelTotal.Location = new System.Drawing.Point(3, 92);
            this.labelTotal.Margin = new System.Windows.Forms.Padding(3, 0, 16, 0);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(42, 20);
            this.labelTotal.TabIndex = 8;
            this.labelTotal.Text = "Total";
            // 
            // lblTotalValor
            // 
            this.lblTotalValor.AutoSize = true;
            this.lblTotalValor.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotalValor.Location = new System.Drawing.Point(86, 92);
            this.lblTotalValor.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.lblTotalValor.Name = "lblTotalValor";
            this.lblTotalValor.Size = new System.Drawing.Size(59, 20);
            this.lblTotalValor.TabIndex = 9;
            this.lblTotalValor.Text = "$ 0,00";
            this.lblTotalValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmDetalleFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(840, 680);
            this.Controls.Add(this.tableLayoutRoot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDetalleFactura";
            this.Padding = new System.Windows.Forms.Padding(16);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detalle de factura";
            this.tableLayoutRoot.ResumeLayout(false);
            this.tableLayoutHeader.ResumeLayout(false);
            this.tableLayoutHeader.PerformLayout();
            this.grpInformacion.ResumeLayout(false);
            this.tableLayoutInfo.ResumeLayout(false);
            this.tableLayoutInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.tableLayoutTotales.ResumeLayout(false);
            this.tableLayoutTotales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDetalle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutRoot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNumeroFactura;
        private System.Windows.Forms.Label lblFechaEmision;
        private System.Windows.Forms.GroupBox grpInformacion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutInfo;
        private System.Windows.Forms.Label labelCliente;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label labelDni;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.Label labelEmpleado;
        private System.Windows.Forms.Label lblEmpleado;
        private System.Windows.Forms.Label labelMetodoPago;
        private System.Windows.Forms.Label lblMetodoPago;
        private System.Windows.Forms.Label labelEstado;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutTotales;
        private System.Windows.Forms.Label labelSubtotal;
        private System.Windows.Forms.Label lblSubtotalValor;
        private System.Windows.Forms.Label labelDescuento;
        private System.Windows.Forms.Label lblDescuentoValor;
        private System.Windows.Forms.Label labelComision;
        private System.Windows.Forms.Label lblComisionValor;
        private System.Windows.Forms.Label labelImpuestos;
        private System.Windows.Forms.Label lblImpuestosValor;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label lblTotalValor;
        private System.Windows.Forms.BindingSource bindingSourceDetalle;
}
