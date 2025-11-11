namespace MyMarket.Formularios.Recibos;

partial class FrmNuevoMetodoPago
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
        this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
        this.lblIdentificacion = new System.Windows.Forms.Label();
        this.txtIdentificacion = new System.Windows.Forms.TextBox();
        this.lblProveedor = new System.Windows.Forms.Label();
        this.txtProveedor = new System.Windows.Forms.TextBox();
        this.lblComision = new System.Windows.Forms.Label();
        this.nudComision = new System.Windows.Forms.NumericUpDown();
        this.panelBotones = new System.Windows.Forms.FlowLayoutPanel();
        this.btnAceptar = new System.Windows.Forms.Button();
        this.btnCancelar = new System.Windows.Forms.Button();
        this.tableLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.nudComision)).BeginInit();
        this.panelBotones.SuspendLayout();
        this.SuspendLayout();
        // 
        // tableLayout
        // 
        this.tableLayout.ColumnCount = 2;
        this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
        this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.tableLayout.Controls.Add(this.lblIdentificacion, 0, 0);
        this.tableLayout.Controls.Add(this.txtIdentificacion, 1, 0);
        this.tableLayout.Controls.Add(this.lblProveedor, 0, 1);
        this.tableLayout.Controls.Add(this.txtProveedor, 1, 1);
        this.tableLayout.Controls.Add(this.lblComision, 0, 2);
        this.tableLayout.Controls.Add(this.nudComision, 1, 2);
        this.tableLayout.Controls.Add(this.panelBotones, 0, 3);
        this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tableLayout.Location = new System.Drawing.Point(16, 16);
        this.tableLayout.Name = "tableLayout";
        this.tableLayout.RowCount = 4;
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tableLayout.Size = new System.Drawing.Size(468, 168);
        this.tableLayout.TabIndex = 0;
        // 
        // lblIdentificacion
        // 
        this.lblIdentificacion.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.lblIdentificacion.AutoSize = true;
        this.lblIdentificacion.Location = new System.Drawing.Point(3, 7);
        this.lblIdentificacion.Name = "lblIdentificacion";
        this.lblIdentificacion.Size = new System.Drawing.Size(84, 15);
        this.lblIdentificacion.TabIndex = 0;
        this.lblIdentificacion.Text = "Identificación:";
        // 
        // txtIdentificacion
        // 
        this.txtIdentificacion.Dock = System.Windows.Forms.DockStyle.Fill;
        this.txtIdentificacion.Location = new System.Drawing.Point(143, 3);
        this.txtIdentificacion.Margin = new System.Windows.Forms.Padding(3, 3, 0, 12);
        this.txtIdentificacion.MaxLength = 18;
        this.txtIdentificacion.Name = "txtIdentificacion";
        this.txtIdentificacion.PlaceholderText = "Número de identificación (CBU, CVU, etc.)";
        this.txtIdentificacion.Size = new System.Drawing.Size(325, 23);
        this.txtIdentificacion.TabIndex = 1;
        // 
        // lblProveedor
        // 
        this.lblProveedor.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.lblProveedor.AutoSize = true;
        this.lblProveedor.Location = new System.Drawing.Point(3, 45);
        this.lblProveedor.Name = "lblProveedor";
        this.lblProveedor.Size = new System.Drawing.Size(64, 15);
        this.lblProveedor.TabIndex = 2;
        this.lblProveedor.Text = "Proveedor:";
        // 
        // txtProveedor
        // 
        this.txtProveedor.Dock = System.Windows.Forms.DockStyle.Fill;
        this.txtProveedor.Location = new System.Drawing.Point(143, 41);
        this.txtProveedor.Margin = new System.Windows.Forms.Padding(3, 3, 0, 12);
        this.txtProveedor.MaxLength = 100;
        this.txtProveedor.Name = "txtProveedor";
        this.txtProveedor.PlaceholderText = "Nombre del proveedor (ej: Visa, Mastercard, Efectivo)";
        this.txtProveedor.Size = new System.Drawing.Size(325, 23);
        this.txtProveedor.TabIndex = 3;
        // 
        // lblComision
        // 
        this.lblComision.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.lblComision.AutoSize = true;
        this.lblComision.Location = new System.Drawing.Point(3, 82);
        this.lblComision.Name = "lblComision";
        this.lblComision.Size = new System.Drawing.Size(83, 15);
        this.lblComision.TabIndex = 4;
        this.lblComision.Text = "Comisión (%):";
        // 
        // nudComision
        // 
        this.nudComision.DecimalPlaces = 2;
        this.nudComision.Dock = System.Windows.Forms.DockStyle.Left;
        this.nudComision.Location = new System.Drawing.Point(143, 79);
        this.nudComision.Margin = new System.Windows.Forms.Padding(3, 3, 0, 12);
        this.nudComision.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
        this.nudComision.Name = "nudComision";
        this.nudComision.Size = new System.Drawing.Size(120, 23);
        this.nudComision.TabIndex = 5;
        // 
        // panelBotones
        // 
        this.panelBotones.AutoSize = true;
        this.panelBotones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.tableLayout.SetColumnSpan(this.panelBotones, 2);
        this.panelBotones.Controls.Add(this.btnAceptar);
        this.panelBotones.Controls.Add(this.btnCancelar);
        this.panelBotones.Dock = System.Windows.Forms.DockStyle.Fill;
        this.panelBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
        this.panelBotones.Location = new System.Drawing.Point(0, 114);
        this.panelBotones.Margin = new System.Windows.Forms.Padding(0, 12, 0, 0);
        this.panelBotones.Name = "panelBotones";
        this.panelBotones.Size = new System.Drawing.Size(468, 54);
        this.panelBotones.TabIndex = 6;
        // 
        // btnAceptar
        // 
        this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
        this.btnAceptar.FlatAppearance.BorderSize = 0;
        this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnAceptar.ForeColor = System.Drawing.Color.White;
        this.btnAceptar.Location = new System.Drawing.Point(342, 6);
        this.btnAceptar.Margin = new System.Windows.Forms.Padding(6, 6, 0, 12);
        this.btnAceptar.Name = "btnAceptar";
        this.btnAceptar.Size = new System.Drawing.Size(120, 36);
        this.btnAceptar.TabIndex = 0;
        this.btnAceptar.Text = "Aceptar";
        this.btnAceptar.UseVisualStyleBackColor = false;
        // 
        // btnCancelar
        // 
        this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(100, 110, 120);
        this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btnCancelar.FlatAppearance.BorderSize = 0;
        this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnCancelar.ForeColor = System.Drawing.Color.White;
        this.btnCancelar.Location = new System.Drawing.Point(216, 6);
        this.btnCancelar.Margin = new System.Windows.Forms.Padding(6, 6, 0, 12);
        this.btnCancelar.Name = "btnCancelar";
        this.btnCancelar.Size = new System.Drawing.Size(120, 36);
        this.btnCancelar.TabIndex = 1;
        this.btnCancelar.Text = "Cancelar";
        this.btnCancelar.UseVisualStyleBackColor = false;
        // 
        // FrmNuevoMetodoPago
        // 
        this.AcceptButton = this.btnAceptar;
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.White;
        this.CancelButton = this.btnCancelar;
        this.ClientSize = new System.Drawing.Size(500, 200);
        this.Controls.Add(this.tableLayout);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "FrmNuevoMetodoPago";
        this.Padding = new System.Windows.Forms.Padding(16);
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Nuevo método de pago";
        this.tableLayout.ResumeLayout(false);
        this.tableLayout.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.nudComision)).EndInit();
        this.panelBotones.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayout;
    private System.Windows.Forms.Label lblIdentificacion;
    private System.Windows.Forms.TextBox txtIdentificacion;
    private System.Windows.Forms.Label lblProveedor;
    private System.Windows.Forms.TextBox txtProveedor;
    private System.Windows.Forms.Label lblComision;
    private System.Windows.Forms.NumericUpDown nudComision;
    private System.Windows.Forms.FlowLayoutPanel panelBotones;
    private System.Windows.Forms.Button btnAceptar;
    private System.Windows.Forms.Button btnCancelar;
}
