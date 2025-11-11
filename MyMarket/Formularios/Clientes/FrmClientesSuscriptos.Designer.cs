namespace MyMarket.Formularios.Clientes;

partial class FrmClientesSuscriptos
{
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelBusqueda = new System.Windows.Forms.Panel();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.bindingSourceClientes = new System.Windows.Forms.BindingSource(this.components);
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnToggleEstado = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.grpFormulario = new System.Windows.Forms.GroupBox();
            this.tableLayoutFormulario = new System.Windows.Forms.TableLayoutPanel();
            this.lblDni = new System.Windows.Forms.Label();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cboEstado = new System.Windows.Forms.ComboBox();
            this.panelAccionesFormulario = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.panelBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceClientes)).BeginInit();
            this.panelAcciones.SuspendLayout();
            this.grpFormulario.SuspendLayout();
            this.tableLayoutFormulario.SuspendLayout();
            this.panelAccionesFormulario.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.BackColor = System.Drawing.Color.White;
            this.panelBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBusqueda.Location = new System.Drawing.Point(0, 0);
            this.panelBusqueda.Name = "panelBusqueda";
            this.panelBusqueda.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.panelBusqueda.Size = new System.Drawing.Size(900, 50);
            this.panelBusqueda.TabIndex = 0;
            // 
            // dgvClientes
            // 
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.AutoGenerateColumns = false;
            this.dgvClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            new System.Windows.Forms.DataGridViewTextBoxColumn {
                DataPropertyName = "Dni",
                HeaderText = "DNI",
                MinimumWidth = 80,
                FillWeight = 60,
                ReadOnly = true
            },
            new System.Windows.Forms.DataGridViewTextBoxColumn {
                DataPropertyName = "Nombre",
                HeaderText = "Nombre",
                MinimumWidth = 120,
                ReadOnly = true
            },
            new System.Windows.Forms.DataGridViewTextBoxColumn {
                DataPropertyName = "Apellido",
                HeaderText = "Apellido",
                MinimumWidth = 120,
                ReadOnly = true
            },
            new System.Windows.Forms.DataGridViewTextBoxColumn {
                DataPropertyName = "Email",
                HeaderText = "Email",
                MinimumWidth = 150,
                ReadOnly = true
            },
            new System.Windows.Forms.DataGridViewTextBoxColumn {
                DataPropertyName = "Direccion",
                HeaderText = "Dirección",
                MinimumWidth = 160,
                ReadOnly = true
            },
            new System.Windows.Forms.DataGridViewTextBoxColumn {
                DataPropertyName = "FechaRegistro",
                HeaderText = "Fecha Suscripción",
                MinimumWidth = 120,
                FillWeight = 80,
                ReadOnly = true,
                DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle {
                    Format = "dd/MM/yyyy"
                }
            },
            new System.Windows.Forms.DataGridViewTextBoxColumn {
                DataPropertyName = "Estado",
                HeaderText = "Estado",
                MinimumWidth = 90,
                FillWeight = 80,
                ReadOnly = true
            }});
            this.dgvClientes.DataSource = this.bindingSourceClientes;
            this.dgvClientes.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvClientes.Location = new System.Drawing.Point(0, 50);
            this.dgvClientes.MultiSelect = false;
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.RowHeadersVisible = false;
            this.dgvClientes.RowTemplate.Height = 25;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.Size = new System.Drawing.Size(900, 310);
            this.dgvClientes.TabIndex = 1;
            // 
            // panelAcciones
            // 
            this.panelAcciones.Controls.Add(this.btnActualizar);
            this.panelAcciones.Controls.Add(this.btnToggleEstado);
            this.panelAcciones.Controls.Add(this.btnEditar);
            this.panelAcciones.Controls.Add(this.btnNuevo);
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAcciones.Location = new System.Drawing.Point(0, 360);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Padding = new System.Windows.Forms.Padding(12);
            this.panelAcciones.Size = new System.Drawing.Size(900, 56);
            this.panelAcciones.TabIndex = 2;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(45, 50, 55);
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.Location = new System.Drawing.Point(768, 13);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(120, 30);
            this.btnActualizar.TabIndex = 3;
            this.btnActualizar.Text = "Actualizar lista";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.BtnActualizar_Click);
            // 
            // btnToggleEstado
            // 
            this.btnToggleEstado.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnToggleEstado.BackColor = System.Drawing.Color.FromArgb(200, 80, 80);
            this.btnToggleEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleEstado.ForeColor = System.Drawing.Color.White;
            this.btnToggleEstado.Location = new System.Drawing.Point(636, 13);
            this.btnToggleEstado.Name = "btnToggleEstado";
            this.btnToggleEstado.Size = new System.Drawing.Size(126, 30);
            this.btnToggleEstado.TabIndex = 2;
            this.btnToggleEstado.Text = "Cambiar estado";
            this.btnToggleEstado.UseVisualStyleBackColor = false;
            this.btnToggleEstado.Click += new System.EventHandler(this.BtnToggleEstado_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(100, 110, 120);
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(144, 13);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(120, 30);
            this.btnEditar.TabIndex = 1;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.BtnEditar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(12, 13);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(120, 30);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.BtnNuevo_Click);
            // 
            // grpFormulario
            // 
            this.grpFormulario.Controls.Add(this.tableLayoutFormulario);
            this.grpFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFormulario.Location = new System.Drawing.Point(0, 416);
            this.grpFormulario.Name = "grpFormulario";
            this.grpFormulario.Padding = new System.Windows.Forms.Padding(12, 16, 12, 12);
            this.grpFormulario.Size = new System.Drawing.Size(900, 204);
            this.grpFormulario.TabIndex = 3;
            this.grpFormulario.TabStop = false;
            this.grpFormulario.Text = "Detalle de cliente";
            // 
            // tableLayoutFormulario
            // 
            this.tableLayoutFormulario.ColumnCount = 2;
            this.tableLayoutFormulario.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutFormulario.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutFormulario.Controls.Add(this.lblDni, 0, 0);
            this.tableLayoutFormulario.Controls.Add(this.txtDni, 1, 0);
            this.tableLayoutFormulario.Controls.Add(this.lblNombre, 0, 1);
            this.tableLayoutFormulario.Controls.Add(this.txtNombre, 1, 1);
            this.tableLayoutFormulario.Controls.Add(this.lblApellido, 0, 2);
            this.tableLayoutFormulario.Controls.Add(this.txtApellido, 1, 2);
            this.tableLayoutFormulario.Controls.Add(this.lblDireccion, 0, 3);
            this.tableLayoutFormulario.Controls.Add(this.txtDireccion, 1, 3);
            this.tableLayoutFormulario.Controls.Add(this.lblEmail, 0, 4);
            this.tableLayoutFormulario.Controls.Add(this.txtEmail, 1, 4);
            this.tableLayoutFormulario.Controls.Add(this.lblEstado, 0, 5);
            this.tableLayoutFormulario.Controls.Add(this.cboEstado, 1, 5);
            this.tableLayoutFormulario.Controls.Add(this.panelAccionesFormulario, 1, 6);
            this.tableLayoutFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutFormulario.Location = new System.Drawing.Point(12, 32);
            this.tableLayoutFormulario.Name = "tableLayoutFormulario";
            this.tableLayoutFormulario.RowCount = 7;
            this.tableLayoutFormulario.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutFormulario.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutFormulario.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutFormulario.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutFormulario.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutFormulario.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutFormulario.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutFormulario.Size = new System.Drawing.Size(876, 160);
            this.tableLayoutFormulario.TabIndex = 0;
            // 
            // lblDni
            // 
            this.lblDni.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDni.AutoSize = true;
            this.lblDni.Location = new System.Drawing.Point(3, 10);
            this.lblDni.Name = "lblDni";
            this.lblDni.Size = new System.Drawing.Size(30, 15);
            this.lblDni.TabIndex = 0;
            this.lblDni.Text = "DNI:";
            // 
            // txtDni
            // 
            this.txtDni.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
            this.txtDni.Location = new System.Drawing.Point(265, 7);
            this.txtDni.MaxLength = 8;
            this.txtDni.Name = "txtDni";
            this.txtDni.Size = new System.Drawing.Size(608, 23);
            this.txtDni.TabIndex = 1;
            // 
            // lblNombre
            // 
            this.lblNombre.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(3, 46);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(54, 15);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
            this.txtNombre.Location = new System.Drawing.Point(265, 43);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(608, 23);
            this.txtNombre.TabIndex = 3;
            // 
            // lblApellido
            // 
            this.lblApellido.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblApellido.AutoSize = true;
            this.lblApellido.Location = new System.Drawing.Point(3, 82);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(54, 15);
            this.lblApellido.TabIndex = 4;
            this.lblApellido.Text = "Apellido:";
            // 
            // txtApellido
            // 
            this.txtApellido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
            this.txtApellido.Location = new System.Drawing.Point(265, 79);
            this.txtApellido.MaxLength = 100;
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(608, 23);
            this.txtApellido.TabIndex = 5;
            // 
            // lblDireccion
            // 
            this.lblDireccion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Location = new System.Drawing.Point(3, 118);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(59, 15);
            this.lblDireccion.TabIndex = 6;
            this.lblDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
            this.txtDireccion.Location = new System.Drawing.Point(265, 115);
            this.txtDireccion.MaxLength = 200;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(608, 23);
            this.txtDireccion.TabIndex = 7;
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(3, 154);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(44, 15);
            this.lblEmail.TabIndex = 8;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
            this.txtEmail.Location = new System.Drawing.Point(265, 151);
            this.txtEmail.MaxLength = 150;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(608, 23);
            this.txtEmail.TabIndex = 9;
            // 
            // lblEstado
            // 
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(3, 190);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(45, 15);
            this.lblEstado.TabIndex = 10;
            this.lblEstado.Text = "Estado:";
            // 
            // cboEstado
            // 
            this.cboEstado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
            this.cboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEstado.FormattingEnabled = true;
            this.cboEstado.Location = new System.Drawing.Point(265, 186);
            this.cboEstado.Name = "cboEstado";
            this.cboEstado.Size = new System.Drawing.Size(608, 23);
            this.cboEstado.TabIndex = 11;
            // 
            // panelAccionesFormulario
            // 
            this.panelAccionesFormulario.Controls.Add(this.btnCancelar);
            this.panelAccionesFormulario.Controls.Add(this.btnGuardar);
            this.panelAccionesFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAccionesFormulario.Location = new System.Drawing.Point(265, 219);
            this.panelAccionesFormulario.Name = "panelAccionesFormulario";
            this.panelAccionesFormulario.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelAccionesFormulario.Size = new System.Drawing.Size(608, 50);
            this.panelAccionesFormulario.TabIndex = 12;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(200, 80, 80);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(126, 11);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 30);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(0, 11);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 30);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.BtnGuardar_Click);
            // 
            // FrmClientesSuscriptos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(900, 620);
            this.Controls.Add(this.grpFormulario);
            this.Controls.Add(this.panelAcciones);
            this.Controls.Add(this.dgvClientes);
            this.Controls.Add(this.panelBusqueda);
            this.Name = "FrmClientesSuscriptos";
            this.Text = "Clientes suscriptos";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmClientesSuscriptos_FormClosed);
            this.Load += new System.EventHandler(this.FrmClientesSuscriptos_Load);
            this.panelBusqueda.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceClientes)).EndInit();
            this.panelAcciones.ResumeLayout(false);
            this.grpFormulario.ResumeLayout(false);
            this.tableLayoutFormulario.ResumeLayout(false);
            this.tableLayoutFormulario.PerformLayout();
            this.panelAccionesFormulario.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBusqueda;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.BindingSource bindingSourceClientes;
        private System.Windows.Forms.Panel panelAcciones;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnToggleEstado;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.GroupBox grpFormulario;
        private System.Windows.Forms.TableLayoutPanel tableLayoutFormulario;
        private System.Windows.Forms.Label lblDni;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cboEstado;
        private System.Windows.Forms.Panel panelAccionesFormulario;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
}
