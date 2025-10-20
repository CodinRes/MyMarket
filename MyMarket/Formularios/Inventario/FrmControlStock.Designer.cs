namespace MyMarket.Formularios.Inventario;

partial class FrmControlStock
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
        components = new System.ComponentModel.Container();
        panelBusqueda = new Panel();
        lblBuscar = new Label();
        txtBuscar = new TextBox();
        btnBuscar = new Button();
        dgv = new DataGridView();
        bindingSourceProductos = new BindingSource(components);
        dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
        dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
        panelAcciones = new Panel();
        btnActualizar = new Button();
        btnEditar = new Button();
        btnNuevo = new Button();
        grpFormulario = new GroupBox();
        tableLayoutFormulario = new TableLayoutPanel();
        lblCodigo = new Label();
        txtCodigo = new TextBox();
        lblProducto = new Label();
        txtProducto = new TextBox();
        lblPrecio = new Label();
        txtPrecio = new TextBox();
        lblStock = new Label();
        txtStock = new TextBox();
        lblCategoria = new Label();
        cboCategoria = new ComboBox();
        lblDescripcion = new Label();
        txtDescripcion = new TextBox();
        lblEstado = new Label();
        cboEstado = new ComboBox();
        panelAccionesFormulario = new Panel();
        btnCancelar = new Button();
        btnGuardar = new Button();
        flowStock = new FlowLayoutPanel();
        chkAgregarStock = new CheckBox();
        nudAgregarStock = new NumericUpDown();
        panelBusqueda.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
        ((System.ComponentModel.ISupportInitialize)bindingSourceProductos).BeginInit();
        panelAcciones.SuspendLayout();
        grpFormulario.SuspendLayout();
        tableLayoutFormulario.SuspendLayout();
        panelAccionesFormulario.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudAgregarStock).BeginInit();
        SuspendLayout();
        // 
        // panelBusqueda
        // 
        panelBusqueda.Controls.Add(lblBuscar);
        panelBusqueda.Controls.Add(txtBuscar);
        panelBusqueda.Controls.Add(btnBuscar);
        panelBusqueda.Dock = DockStyle.Top;
        panelBusqueda.Location = new Point(0, 0);
        panelBusqueda.Name = "panelBusqueda";
        panelBusqueda.Padding = new Padding(12, 8, 12, 8);
        panelBusqueda.Size = new Size(800, 44);
        panelBusqueda.TabIndex = 3;
        // 
        // lblBuscar
        // 
        lblBuscar.AutoSize = true;
        lblBuscar.Location = new Point(12, 12);
        lblBuscar.Name = "lblBuscar";
        lblBuscar.Size = new Size(97, 15);
        lblBuscar.TabIndex = 0;
        lblBuscar.Text = "Buscar producto:";
        // 
        // txtBuscar
        // 
        txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtBuscar.Location = new Point(120, 9);
        txtBuscar.Name = "txtBuscar";
        txtBuscar.Size = new Size(500, 23);
        txtBuscar.TabIndex = 0;
        // 
        // btnBuscar
        // 
        btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnBuscar.Location = new Point(630, 8);
        btnBuscar.Name = "btnBuscar";
        btnBuscar.Size = new Size(100, 26);
        btnBuscar.TabIndex = 1;
        btnBuscar.Text = "Buscar";
        btnBuscar.UseVisualStyleBackColor = true;
        // 
        // dgv
        // 
        dgv.AllowUserToAddRows = false;
        dgv.AllowUserToDeleteRows = false;
        dgv.AutoGenerateColumns = false;
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgv.BackgroundColor = Color.White;
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgv.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6 });
        dgv.DataSource = bindingSourceProductos;
        dgv.Dock = DockStyle.Top;
        dgv.Location = new Point(0, 44);
        dgv.MultiSelect = false;
        dgv.Name = "dgv";
        dgv.ReadOnly = true;
        dgv.RowHeadersVisible = false;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.Size = new Size(800, 260);
        dgv.TabIndex = 0;
        // 
        // dataGridViewTextBoxColumn1
        // 
        dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
        dataGridViewTextBoxColumn1.ReadOnly = true;
        // 
        // dataGridViewTextBoxColumn2
        // 
        dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
        dataGridViewTextBoxColumn2.ReadOnly = true;
        // 
        // dataGridViewTextBoxColumn3
        // 
        dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
        dataGridViewTextBoxColumn3.ReadOnly = true;
        // 
        // dataGridViewTextBoxColumn4
        // 
        dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
        dataGridViewTextBoxColumn4.ReadOnly = true;
        // 
        // dataGridViewTextBoxColumn5
        // 
        dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
        dataGridViewTextBoxColumn5.ReadOnly = true;
        // 
        // dataGridViewTextBoxColumn6
        // 
        dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
        dataGridViewTextBoxColumn6.ReadOnly = true;
        // 
        // panelAcciones
        // 
        panelAcciones.Controls.Add(btnActualizar);
        panelAcciones.Controls.Add(btnEditar);
        panelAcciones.Controls.Add(btnNuevo);
        panelAcciones.Dock = DockStyle.Top;
        panelAcciones.Location = new Point(0, 304);
        panelAcciones.Name = "panelAcciones";
        panelAcciones.Padding = new Padding(12);
        panelAcciones.Size = new Size(800, 60);
        panelAcciones.TabIndex = 1;
        // 
        // btnActualizar
        // 
        btnActualizar.Anchor = AnchorStyles.Right;
        btnActualizar.BackColor = Color.FromArgb(45, 50, 55);
        btnActualizar.FlatStyle = FlatStyle.Flat;
        btnActualizar.ForeColor = Color.White;
        btnActualizar.Location = new Point(668, 15);
        btnActualizar.Name = "btnActualizar";
        btnActualizar.Size = new Size(120, 30);
        btnActualizar.TabIndex = 4;
        btnActualizar.Text = "Actualizar lista";
        btnActualizar.UseVisualStyleBackColor = false;
        // 
        // btnEditar
        // 
        btnEditar.BackColor = Color.FromArgb(100, 110, 120);
        btnEditar.FlatStyle = FlatStyle.Flat;
        btnEditar.ForeColor = Color.White;
        btnEditar.Location = new Point(144, 15);
        btnEditar.Name = "btnEditar";
        btnEditar.Size = new Size(120, 30);
        btnEditar.TabIndex = 1;
        btnEditar.Text = "Editar";
        btnEditar.UseVisualStyleBackColor = false;
        // 
        // btnNuevo
        // 
        btnNuevo.BackColor = Color.FromArgb(55, 130, 200);
        btnNuevo.FlatStyle = FlatStyle.Flat;
        btnNuevo.ForeColor = Color.White;
        btnNuevo.Location = new Point(12, 15);
        btnNuevo.Name = "btnNuevo";
        btnNuevo.Size = new Size(120, 30);
        btnNuevo.TabIndex = 0;
        btnNuevo.Text = "Nuevo";
        btnNuevo.UseVisualStyleBackColor = false;
        // 
        // grpFormulario
        // 
        grpFormulario.Controls.Add(tableLayoutFormulario);
        grpFormulario.Dock = DockStyle.Fill;
        grpFormulario.Location = new Point(0, 364);
        grpFormulario.Name = "grpFormulario";
        grpFormulario.Padding = new Padding(12, 16, 12, 12);
        grpFormulario.Size = new Size(800, 256);
        grpFormulario.TabIndex = 2;
        grpFormulario.TabStop = false;
        grpFormulario.Text = "Detalle de producto";
        // 
        // tableLayoutFormulario
        // 
        tableLayoutFormulario.ColumnCount = 2;
        tableLayoutFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        tableLayoutFormulario.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
        tableLayoutFormulario.Controls.Add(lblCodigo, 0, 0);
        tableLayoutFormulario.Controls.Add(txtCodigo, 1, 0);
        tableLayoutFormulario.Controls.Add(lblProducto, 0, 1);
        tableLayoutFormulario.Controls.Add(txtProducto, 1, 1);
        tableLayoutFormulario.Controls.Add(lblPrecio, 0, 2);
        tableLayoutFormulario.Controls.Add(txtPrecio, 1, 2);
        tableLayoutFormulario.Controls.Add(lblStock, 0, 3);
        tableLayoutFormulario.Controls.Add(flowStock, 1, 3);
        tableLayoutFormulario.Controls.Add(lblCategoria, 0, 4);
        tableLayoutFormulario.Controls.Add(cboCategoria, 1, 4);
        tableLayoutFormulario.Controls.Add(lblDescripcion, 0, 5);
        tableLayoutFormulario.Controls.Add(txtDescripcion, 1, 5);
        tableLayoutFormulario.Controls.Add(lblEstado, 0, 6);
        tableLayoutFormulario.Controls.Add(cboEstado, 1, 6);
        tableLayoutFormulario.Controls.Add(panelAccionesFormulario, 1, 7);
        tableLayoutFormulario.Dock = DockStyle.Fill;
        tableLayoutFormulario.Location = new Point(12, 32);
        tableLayoutFormulario.Name = "tableLayoutFormulario";
        tableLayoutFormulario.RowCount = 8;
        tableLayoutFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        tableLayoutFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        tableLayoutFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        tableLayoutFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        tableLayoutFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        tableLayoutFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
        tableLayoutFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        tableLayoutFormulario.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        tableLayoutFormulario.Size = new Size(776, 212);
        tableLayoutFormulario.TabIndex = 0;
        // 
        // lblCodigo
        // 
        lblCodigo.Anchor = AnchorStyles.Left;
        lblCodigo.AutoSize = true;
        lblCodigo.Location = new Point(3, 10);
        lblCodigo.Name = "lblCodigo";
        lblCodigo.Size = new Size(49, 15);
        lblCodigo.TabIndex = 0;
        lblCodigo.Text = "Código:";
        // 
        // txtCodigo
        // 
        txtCodigo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtCodigo.Location = new Point(235, 6);
        txtCodigo.MaxLength = 50;
        txtCodigo.Name = "txtCodigo";
        txtCodigo.Size = new Size(538, 23);
        txtCodigo.TabIndex = 1;
        // 
        // lblProducto
        // 
        lblProducto.Anchor = AnchorStyles.Left;
        lblProducto.AutoSize = true;
        lblProducto.Location = new Point(3, 46);
        lblProducto.Name = "lblProducto";
        lblProducto.Size = new Size(59, 15);
        lblProducto.TabIndex = 2;
        lblProducto.Text = "Producto:";
        // 
        // txtProducto
        // 
        txtProducto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtProducto.Location = new Point(235, 42);
        txtProducto.MaxLength = 200;
        txtProducto.Name = "txtProducto";
        txtProducto.Size = new Size(538, 23);
        txtProducto.TabIndex = 3;
        // 
        // lblPrecio
        // 
        lblPrecio.Anchor = AnchorStyles.Left;
        lblPrecio.AutoSize = true;
        lblPrecio.Location = new Point(3, 82);
        lblPrecio.Name = "lblPrecio";
        lblPrecio.Size = new Size(43, 15);
        lblPrecio.TabIndex = 8;
        lblPrecio.Text = "Precio:";
        // 
        // txtPrecio
        // 
        txtPrecio.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtPrecio.Location = new Point(235, 78);
        txtPrecio.MaxLength = 20;
        txtPrecio.Name = "txtPrecio";
        txtPrecio.Size = new Size(538, 23);
        txtPrecio.TabIndex = 9;
        // 
        // lblStock
        // 
        lblStock.Anchor = AnchorStyles.Left;
        lblStock.AutoSize = true;
        lblStock.Location = new Point(3, 118);
        lblStock.Name = "lblStock";
        lblStock.Size = new Size(39, 15);
        lblStock.TabIndex = 4;
        lblStock.Text = "Stock:";
        // 
        // flowStock
        // 
        flowStock.Dock = DockStyle.Fill;
        flowStock.FlowDirection = FlowDirection.LeftToRight;
        flowStock.Location = new Point(235, 114);
        flowStock.Name = "flowStock";
        flowStock.Size = new Size(538, 30);
        flowStock.TabIndex = 5;
        flowStock.Controls.Add(txtStock);
        flowStock.Controls.Add(chkAgregarStock);
        flowStock.Controls.Add(nudAgregarStock);
        // 
        // txtStock
        // 
        txtStock.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtStock.Location = new Point(3, 4);
        txtStock.MaxLength = 10;
        txtStock.Name = "txtStock";
        txtStock.Size = new Size(194, 23);
        txtStock.TabIndex = 0;
        // 
        // chkAgregarStock
        // 
        chkAgregarStock.Anchor = AnchorStyles.Left;
        chkAgregarStock.AutoSize = true;
        chkAgregarStock.Location = new Point(203, 7);
        chkAgregarStock.Name = "chkAgregarStock";
        chkAgregarStock.Size = new Size(103, 19);
        chkAgregarStock.TabIndex = 1;
        chkAgregarStock.Text = "Agregar cantidad";
        chkAgregarStock.UseVisualStyleBackColor = true;
        // 
        // nudAgregarStock
        // 
        nudAgregarStock.Anchor = AnchorStyles.Left;
        nudAgregarStock.Location = new Point(312, 4);
        nudAgregarStock.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        nudAgregarStock.Name = "nudAgregarStock";
        nudAgregarStock.Size = new Size(80, 23);
        nudAgregarStock.TabIndex = 2;
        nudAgregarStock.Value = new decimal(new int[] { 0, 0, 0, 0 });
        // 
        // lblCategoria
        // 
        lblCategoria.Anchor = AnchorStyles.Left;
        lblCategoria.AutoSize = true;
        lblCategoria.Location = new Point(3, 154);
        lblCategoria.Name = "lblCategoria";
        lblCategoria.Size = new Size(61, 15);
        lblCategoria.TabIndex = 13;
        lblCategoria.Text = "Categoría:";
        // 
        // cboCategoria
        // 
        cboCategoria.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
        cboCategoria.FormattingEnabled = true;
        cboCategoria.Location = new Point(235, 150);
        cboCategoria.Name = "cboCategoria";
        cboCategoria.Size = new Size(538, 23);
        cboCategoria.TabIndex = 14;
        // 
        // lblDescripcion
        // 
        lblDescripcion.Anchor = AnchorStyles.Left;
        lblDescripcion.AutoSize = true;
        lblDescripcion.Location = new Point(3, 208);
        lblDescripcion.Name = "lblDescripcion";
        lblDescripcion.Size = new Size(72, 15);
        lblDescripcion.TabIndex = 10;
        lblDescripcion.Text = "Descripción:";
        // 
        // txtDescripcion
        // 
        txtDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        txtDescripcion.Location = new Point(235, 183);
        txtDescripcion.MaxLength = 4000;
        txtDescripcion.Multiline = true;
        txtDescripcion.Name = "txtDescripcion";
        txtDescripcion.ScrollBars = ScrollBars.Vertical;
        txtDescripcion.Size = new Size(538, 66);
        txtDescripcion.TabIndex = 11;
        // 
        // lblEstado
        // 
        lblEstado.Location = new Point(3, 252);
        lblEstado.Name = "lblEstado";
        lblEstado.Size = new Size(100, 23);
        lblEstado.TabIndex = 15;
        // 
        // cboEstado
        // 
        cboEstado.Location = new Point(235, 255);
        cboEstado.Name = "cboEstado";
        cboEstado.Size = new Size(121, 23);
        cboEstado.TabIndex = 16;
        // 
        // panelAccionesFormulario
        // 
        panelAccionesFormulario.Controls.Add(btnCancelar);
        panelAccionesFormulario.Controls.Add(btnGuardar);
        panelAccionesFormulario.Dock = DockStyle.Fill;
        panelAccionesFormulario.Location = new Point(235, 291);
        panelAccionesFormulario.Name = "panelAccionesFormulario";
        panelAccionesFormulario.Padding = new Padding(0, 8, 0, 0);
        panelAccionesFormulario.Size = new Size(538, 50);
        panelAccionesFormulario.TabIndex = 12;
        // 
        // btnCancelar
        // 
        btnCancelar.Anchor = AnchorStyles.Left;
        btnCancelar.BackColor = Color.FromArgb(200, 80, 80);
        btnCancelar.FlatStyle = FlatStyle.Flat;
        btnCancelar.ForeColor = Color.White;
        btnCancelar.Location = new Point(126, -11);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(120, 30);
        btnCancelar.TabIndex = 1;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = false;
        // 
        // btnGuardar
        // 
        btnGuardar.Anchor = AnchorStyles.Left;
        btnGuardar.BackColor = Color.FromArgb(55, 130, 200);
        btnGuardar.FlatStyle = FlatStyle.Flat;
        btnGuardar.ForeColor = Color.White;
        btnGuardar.Location = new Point(0, -11);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Size = new Size(120, 30);
        btnGuardar.TabIndex = 0;
        btnGuardar.Text = "Guardar";
        btnGuardar.UseVisualStyleBackColor = false;
        // 
        // FrmControlStock
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.WhiteSmoke;
        ClientSize = new Size(800, 620);
        Controls.Add(grpFormulario);
        Controls.Add(panelAcciones);
        Controls.Add(dgv);
        Controls.Add(panelBusqueda);
        Name = "FrmControlStock";
        Text = "Control de Stock";
        panelBusqueda.ResumeLayout(false);
        panelBusqueda.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
        ((System.ComponentModel.ISupportInitialize)bindingSourceProductos).EndInit();
        panelAcciones.ResumeLayout(false);
        grpFormulario.ResumeLayout(false);
        tableLayoutFormulario.ResumeLayout(false);
        tableLayoutFormulario.PerformLayout();
        panelAccionesFormulario.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)nudAgregarStock).EndInit();
        ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.BindingSource bindingSourceProductos;
        private System.Windows.Forms.Panel panelAcciones;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.GroupBox grpFormulario;
        private System.Windows.Forms.TableLayoutPanel tableLayoutFormulario;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.TextBox txtProducto;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.Panel panelAccionesFormulario;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.ComboBox cboCategoria;
        private System.Windows.Forms.Panel panelBusqueda;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private FlowLayoutPanel flowStock;
    private CheckBox chkAgregarStock;
    private NumericUpDown nudAgregarStock;
}