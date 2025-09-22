namespace MyMarket
{
    partial class FrmGestionUsuarios
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
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.bindingSourceUsuarios = new System.Windows.Forms.BindingSource(this.components);
            this.panelAcciones = new System.Windows.Forms.Panel();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.grpNuevoUsuario = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCuil = new System.Windows.Forms.Label();
            this.txtCuil = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblContrasenia = new System.Windows.Forms.Label();
            this.txtContrasenia = new System.Windows.Forms.TextBox();
            this.lblRol = new System.Windows.Forms.Label();
            this.cboRol = new System.Windows.Forms.ComboBox();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.panelBotonAgregar = new System.Windows.Forms.Panel();
            this.btnAgregar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUsuarios)).BeginInit();
            this.panelAcciones.SuspendLayout();
            this.grpNuevoUsuario.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelBotonAgregar.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvUsuarios
            // 
            this.dgvUsuarios.AllowUserToAddRows = false;
            this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.AutoGenerateColumns = false;
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            new System.Windows.Forms.DataGridViewTextBoxColumn {
                DataPropertyName = "IdEmpleado",
                HeaderText = "ID",
                MinimumWidth = 50,
                FillWeight = 40,
                ReadOnly = true
            },
            new System.Windows.Forms.DataGridViewTextBoxColumn {
                DataPropertyName = "NombreParaMostrar",
                HeaderText = "Nombre",
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
                DataPropertyName = "CuilCuit",
                HeaderText = "CUIL/CUIT",
                MinimumWidth = 120,
                ReadOnly = true
            },
            new System.Windows.Forms.DataGridViewTextBoxColumn {
                DataPropertyName = "RolDescripcion",
                HeaderText = "Rol",
                MinimumWidth = 100,
                ReadOnly = true
            },
            new System.Windows.Forms.DataGridViewCheckBoxColumn {
                DataPropertyName = "Activo",
                HeaderText = "Activo",
                MinimumWidth = 70,
                ReadOnly = true
            }});
            this.dgvUsuarios.DataSource = this.bindingSourceUsuarios;
            this.dgvUsuarios.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvUsuarios.Location = new System.Drawing.Point(0, 0);
            this.dgvUsuarios.MultiSelect = false;
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            this.dgvUsuarios.RowHeadersVisible = false;
            this.dgvUsuarios.RowTemplate.Height = 25;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.Size = new System.Drawing.Size(784, 260);
            this.dgvUsuarios.TabIndex = 0;
            // 
            // panelAcciones
            // 
            this.panelAcciones.Controls.Add(this.btnEliminar);
            this.panelAcciones.Controls.Add(this.btnRefrescar);
            this.panelAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAcciones.Location = new System.Drawing.Point(0, 260);
            this.panelAcciones.Name = "panelAcciones";
            this.panelAcciones.Padding = new System.Windows.Forms.Padding(12);
            this.panelAcciones.Size = new System.Drawing.Size(784, 56);
            this.panelAcciones.TabIndex = 1;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnEliminar.BackColor = System.Drawing.Color.Firebrick;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(646, 15);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(120, 30);
            this.btnEliminar.TabIndex = 1;
            this.btnEliminar.Text = "Quitar usuario";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.BtnEliminar_Click);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.ForeColor = System.Drawing.Color.White;
            this.btnRefrescar.Location = new System.Drawing.Point(12, 15);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(120, 30);
            this.btnRefrescar.TabIndex = 0;
            this.btnRefrescar.Text = "Actualizar lista";
            this.btnRefrescar.UseVisualStyleBackColor = false;
            this.btnRefrescar.Click += new System.EventHandler(this.BtnRefrescar_Click);
            // 
            // grpNuevoUsuario
            // 
            this.grpNuevoUsuario.Controls.Add(this.tableLayoutPanel1);
            this.grpNuevoUsuario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpNuevoUsuario.Location = new System.Drawing.Point(0, 316);
            this.grpNuevoUsuario.Name = "grpNuevoUsuario";
            this.grpNuevoUsuario.Padding = new System.Windows.Forms.Padding(12, 16, 12, 12);
            this.grpNuevoUsuario.Size = new System.Drawing.Size(784, 245);
            this.grpNuevoUsuario.TabIndex = 2;
            this.grpNuevoUsuario.TabStop = false;
            this.grpNuevoUsuario.Text = "Nuevo usuario";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.lblCuil, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtCuil, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblEmail, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtEmail, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblContrasenia, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtContrasenia, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblRol, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cboRol, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.chkActivo, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.panelBotonAgregar, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 32);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(760, 201);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblCuil
            // 
            this.lblCuil.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCuil.AutoSize = true;
            this.lblCuil.Location = new System.Drawing.Point(3, 10);
            this.lblCuil.Name = "lblCuil";
            this.lblCuil.Size = new System.Drawing.Size(73, 15);
            this.lblCuil.TabIndex = 0;
            this.lblCuil.Text = "CUIL / CUIT:";
            // 
            // txtCuil
            // 
            this.txtCuil.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
            this.txtCuil.Location = new System.Drawing.Point(231, 7);
            this.txtCuil.MaxLength = 13;
            this.txtCuil.Name = "txtCuil";
            this.txtCuil.Size = new System.Drawing.Size(526, 23);
            this.txtCuil.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(3, 46);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(44, 15);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
            this.txtEmail.Location = new System.Drawing.Point(231, 43);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(526, 23);
            this.txtEmail.TabIndex = 3;
            // 
            // lblContrasenia
            // 
            this.lblContrasenia.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblContrasenia.AutoSize = true;
            this.lblContrasenia.Location = new System.Drawing.Point(3, 82);
            this.lblContrasenia.Name = "lblContrasenia";
            this.lblContrasenia.Size = new System.Drawing.Size(69, 15);
            this.lblContrasenia.TabIndex = 4;
            this.lblContrasenia.Text = "Contraseña:";
            // 
            // txtContrasenia
            // 
            this.txtContrasenia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
            this.txtContrasenia.Location = new System.Drawing.Point(231, 79);
            this.txtContrasenia.MaxLength = 100;
            this.txtContrasenia.Name = "txtContrasenia";
            this.txtContrasenia.Size = new System.Drawing.Size(526, 23);
            this.txtContrasenia.TabIndex = 5;
            this.txtContrasenia.UseSystemPasswordChar = true;
            // 
            // lblRol
            // 
            this.lblRol.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRol.AutoSize = true;
            this.lblRol.Location = new System.Drawing.Point(3, 118);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(26, 15);
            this.lblRol.TabIndex = 6;
            this.lblRol.Text = "Rol:";
            // 
            // cboRol
            // 
            this.cboRol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right))));
            this.cboRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRol.FormattingEnabled = true;
            this.cboRol.Location = new System.Drawing.Point(231, 114);
            this.cboRol.Name = "cboRol";
            this.cboRol.Size = new System.Drawing.Size(526, 23);
            this.cboRol.TabIndex = 7;
            // 
            // chkActivo
            // 
            this.chkActivo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkActivo.AutoSize = true;
            this.chkActivo.Checked = true;
            this.chkActivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActivo.Location = new System.Drawing.Point(231, 151);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(61, 19);
            this.chkActivo.TabIndex = 8;
            this.chkActivo.Text = "Activo";
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // panelBotonAgregar
            // 
            this.panelBotonAgregar.Controls.Add(this.btnAgregar);
            this.panelBotonAgregar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBotonAgregar.Location = new System.Drawing.Point(231, 183);
            this.panelBotonAgregar.Name = "panelBotonAgregar";
            this.panelBotonAgregar.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelBotonAgregar.Size = new System.Drawing.Size(526, 42);
            this.panelBotonAgregar.TabIndex = 9;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(0, 8);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(140, 30);
            this.btnAgregar.TabIndex = 0;
            this.btnAgregar.Text = "Agregar usuario";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.BtnAgregar_Click);
            // 
            // FrmGestionUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.grpNuevoUsuario);
            this.Controls.Add(this.panelAcciones);
            this.Controls.Add(this.dgvUsuarios);
            this.Name = "FrmGestionUsuarios";
            this.Text = "Gestión de usuarios";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceUsuarios)).EndInit();
            this.panelAcciones.ResumeLayout(false);
            this.grpNuevoUsuario.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panelBotonAgregar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.BindingSource bindingSourceUsuarios;
        private System.Windows.Forms.Panel panelAcciones;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.GroupBox grpNuevoUsuario;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblCuil;
        private System.Windows.Forms.TextBox txtCuil;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblContrasenia;
        private System.Windows.Forms.TextBox txtContrasenia;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.ComboBox cboRol;
        private System.Windows.Forms.CheckBox chkActivo;
        private System.Windows.Forms.Panel panelBotonAgregar;
        private System.Windows.Forms.Button btnAgregar;
    }
}
