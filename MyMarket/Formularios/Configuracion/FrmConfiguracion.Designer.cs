namespace MyMarket.Formularios.Configuracion;

partial class FrmConfiguracion
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
        this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
        this.grpImpuestos = new System.Windows.Forms.GroupBox();
        this.tableLayoutImpuestos = new System.Windows.Forms.TableLayoutPanel();
        this.lblPorcentajeImpuestos = new System.Windows.Forms.Label();
        this.numPorcentajeImpuestos = new System.Windows.Forms.NumericUpDown();
        this.lblInfoImpuestos = new System.Windows.Forms.Label();
        this.grpDescuentoAntiguedad = new System.Windows.Forms.GroupBox();
        this.tableLayoutDescuento = new System.Windows.Forms.TableLayoutPanel();
        this.lblDiasAntiguedad = new System.Windows.Forms.Label();
        this.numDiasAntiguedad = new System.Windows.Forms.NumericUpDown();
        this.lblPorcentajeDescuento = new System.Windows.Forms.Label();
        this.numPorcentajeDescuento = new System.Windows.Forms.NumericUpDown();
        this.lblInfoDescuento = new System.Windows.Forms.Label();
        this.panelBotones = new System.Windows.Forms.FlowLayoutPanel();
        this.btnCancelar = new System.Windows.Forms.Button();
        this.btnGuardar = new System.Windows.Forms.Button();
        this.mainLayout.SuspendLayout();
        this.grpImpuestos.SuspendLayout();
        this.tableLayoutImpuestos.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numPorcentajeImpuestos)).BeginInit();
        this.grpDescuentoAntiguedad.SuspendLayout();
        this.tableLayoutDescuento.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numDiasAntiguedad)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.numPorcentajeDescuento)).BeginInit();
        this.panelBotones.SuspendLayout();
        this.SuspendLayout();
        // 
        // mainLayout
        // 
        this.mainLayout.ColumnCount = 1;
        this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.mainLayout.Controls.Add(this.grpImpuestos, 0, 0);
        this.mainLayout.Controls.Add(this.grpDescuentoAntiguedad, 0, 1);
        this.mainLayout.Controls.Add(this.panelBotones, 0, 2);
        this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        this.mainLayout.Location = new System.Drawing.Point(16, 16);
        this.mainLayout.Name = "mainLayout";
        this.mainLayout.RowCount = 3;
        this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.mainLayout.Size = new System.Drawing.Size(568, 368);
        this.mainLayout.TabIndex = 0;
        // 
        // grpImpuestos
        // 
        this.grpImpuestos.Controls.Add(this.tableLayoutImpuestos);
        this.grpImpuestos.Dock = System.Windows.Forms.DockStyle.Fill;
        this.grpImpuestos.Location = new System.Drawing.Point(3, 3);
        this.grpImpuestos.Name = "grpImpuestos";
        this.grpImpuestos.Padding = new System.Windows.Forms.Padding(12, 16, 12, 12);
        this.grpImpuestos.Size = new System.Drawing.Size(562, 125);
        this.grpImpuestos.TabIndex = 0;
        this.grpImpuestos.TabStop = false;
        this.grpImpuestos.Text = "Impuestos";
        // 
        // tableLayoutImpuestos
        // 
        this.tableLayoutImpuestos.ColumnCount = 2;
        this.tableLayoutImpuestos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tableLayoutImpuestos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tableLayoutImpuestos.Controls.Add(this.lblPorcentajeImpuestos, 0, 0);
        this.tableLayoutImpuestos.Controls.Add(this.numPorcentajeImpuestos, 1, 0);
        this.tableLayoutImpuestos.Controls.Add(this.lblInfoImpuestos, 0, 1);
        this.tableLayoutImpuestos.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tableLayoutImpuestos.Location = new System.Drawing.Point(12, 32);
        this.tableLayoutImpuestos.Name = "tableLayoutImpuestos";
        this.tableLayoutImpuestos.RowCount = 2;
        this.tableLayoutImpuestos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tableLayoutImpuestos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tableLayoutImpuestos.Size = new System.Drawing.Size(538, 81);
        this.tableLayoutImpuestos.TabIndex = 0;
        // 
        // lblPorcentajeImpuestos
        // 
        this.lblPorcentajeImpuestos.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.lblPorcentajeImpuestos.AutoSize = true;
        this.lblPorcentajeImpuestos.Location = new System.Drawing.Point(3, 7);
        this.lblPorcentajeImpuestos.Name = "lblPorcentajeImpuestos";
        this.lblPorcentajeImpuestos.Size = new System.Drawing.Size(140, 15);
        this.lblPorcentajeImpuestos.TabIndex = 0;
        this.lblPorcentajeImpuestos.Text = "Porcentaje de impuestos:";
        // 
        // numPorcentajeImpuestos
        // 
        this.numPorcentajeImpuestos.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.numPorcentajeImpuestos.Location = new System.Drawing.Point(272, 3);
        this.numPorcentajeImpuestos.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
        this.numPorcentajeImpuestos.Name = "numPorcentajeImpuestos";
        this.numPorcentajeImpuestos.Size = new System.Drawing.Size(120, 23);
        this.numPorcentajeImpuestos.TabIndex = 1;
        this.numPorcentajeImpuestos.Value = new decimal(new int[] { 21, 0, 0, 0 });
        // 
        // lblInfoImpuestos
        // 
        this.lblInfoImpuestos.AutoSize = true;
        this.tableLayoutImpuestos.SetColumnSpan(this.lblInfoImpuestos, 2);
        this.lblInfoImpuestos.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
        this.lblInfoImpuestos.Location = new System.Drawing.Point(3, 29);
        this.lblInfoImpuestos.Name = "lblInfoImpuestos";
        this.lblInfoImpuestos.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
        this.lblInfoImpuestos.Size = new System.Drawing.Size(442, 23);
        this.lblInfoImpuestos.TabIndex = 2;
        this.lblInfoImpuestos.Text = "Este porcentaje se aplicará a todas las ventas. Por ejemplo, 21 para IVA del 21%.";
        // 
        // grpDescuentoAntiguedad
        // 
        this.grpDescuentoAntiguedad.Controls.Add(this.tableLayoutDescuento);
        this.grpDescuentoAntiguedad.Dock = System.Windows.Forms.DockStyle.Fill;
        this.grpDescuentoAntiguedad.Location = new System.Drawing.Point(3, 134);
        this.grpDescuentoAntiguedad.Name = "grpDescuentoAntiguedad";
        this.grpDescuentoAntiguedad.Padding = new System.Windows.Forms.Padding(12, 16, 12, 12);
        this.grpDescuentoAntiguedad.Size = new System.Drawing.Size(562, 175);
        this.grpDescuentoAntiguedad.TabIndex = 1;
        this.grpDescuentoAntiguedad.TabStop = false;
        this.grpDescuentoAntiguedad.Text = "Descuento por antigüedad de cliente";
        // 
        // tableLayoutDescuento
        // 
        this.tableLayoutDescuento.ColumnCount = 2;
        this.tableLayoutDescuento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tableLayoutDescuento.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        this.tableLayoutDescuento.Controls.Add(this.lblDiasAntiguedad, 0, 0);
        this.tableLayoutDescuento.Controls.Add(this.numDiasAntiguedad, 1, 0);
        this.tableLayoutDescuento.Controls.Add(this.lblPorcentajeDescuento, 0, 1);
        this.tableLayoutDescuento.Controls.Add(this.numPorcentajeDescuento, 1, 1);
        this.tableLayoutDescuento.Controls.Add(this.lblInfoDescuento, 0, 2);
        this.tableLayoutDescuento.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tableLayoutDescuento.Location = new System.Drawing.Point(12, 32);
        this.tableLayoutDescuento.Name = "tableLayoutDescuento";
        this.tableLayoutDescuento.RowCount = 3;
        this.tableLayoutDescuento.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tableLayoutDescuento.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tableLayoutDescuento.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.tableLayoutDescuento.Size = new System.Drawing.Size(538, 131);
        this.tableLayoutDescuento.TabIndex = 0;
        // 
        // lblDiasAntiguedad
        // 
        this.lblDiasAntiguedad.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.lblDiasAntiguedad.AutoSize = true;
        this.lblDiasAntiguedad.Location = new System.Drawing.Point(3, 7);
        this.lblDiasAntiguedad.Name = "lblDiasAntiguedad";
        this.lblDiasAntiguedad.Size = new System.Drawing.Size(164, 15);
        this.lblDiasAntiguedad.TabIndex = 0;
        this.lblDiasAntiguedad.Text = "Días mínimos de antigüedad:";
        // 
        // numDiasAntiguedad
        // 
        this.numDiasAntiguedad.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.numDiasAntiguedad.Location = new System.Drawing.Point(272, 3);
        this.numDiasAntiguedad.Maximum = new decimal(new int[] { 3650, 0, 0, 0 });
        this.numDiasAntiguedad.Name = "numDiasAntiguedad";
        this.numDiasAntiguedad.Size = new System.Drawing.Size(120, 23);
        this.numDiasAntiguedad.TabIndex = 1;
        this.numDiasAntiguedad.Value = new decimal(new int[] { 365, 0, 0, 0 });
        // 
        // lblPorcentajeDescuento
        // 
        this.lblPorcentajeDescuento.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.lblPorcentajeDescuento.AutoSize = true;
        this.lblPorcentajeDescuento.Location = new System.Drawing.Point(3, 36);
        this.lblPorcentajeDescuento.Name = "lblPorcentajeDescuento";
        this.lblPorcentajeDescuento.Size = new System.Drawing.Size(139, 15);
        this.lblPorcentajeDescuento.TabIndex = 2;
        this.lblPorcentajeDescuento.Text = "Porcentaje de descuento:";
        // 
        // numPorcentajeDescuento
        // 
        this.numPorcentajeDescuento.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.numPorcentajeDescuento.DecimalPlaces = 2;
        this.numPorcentajeDescuento.Location = new System.Drawing.Point(272, 32);
        this.numPorcentajeDescuento.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
        this.numPorcentajeDescuento.Name = "numPorcentajeDescuento";
        this.numPorcentajeDescuento.Size = new System.Drawing.Size(120, 23);
        this.numPorcentajeDescuento.TabIndex = 3;
        this.numPorcentajeDescuento.Value = new decimal(new int[] { 5, 0, 0, 0 });
        // 
        // lblInfoDescuento
        // 
        this.lblInfoDescuento.AutoSize = true;
        this.tableLayoutDescuento.SetColumnSpan(this.lblInfoDescuento, 2);
        this.lblInfoDescuento.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
        this.lblInfoDescuento.Location = new System.Drawing.Point(3, 58);
        this.lblInfoDescuento.Name = "lblInfoDescuento";
        this.lblInfoDescuento.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
        this.lblInfoDescuento.Size = new System.Drawing.Size(518, 38);
        this.lblInfoDescuento.TabIndex = 4;
        this.lblInfoDescuento.Text = "Los clientes con una antigüedad igual o mayor a los días especificados recibirán automáticamente\r\nel descuento indicado en sus compras.";
        // 
        // panelBotones
        // 
        this.panelBotones.AutoSize = true;
        this.panelBotones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.panelBotones.Controls.Add(this.btnCancelar);
        this.panelBotones.Controls.Add(this.btnGuardar);
        this.panelBotones.Dock = System.Windows.Forms.DockStyle.Fill;
        this.panelBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
        this.panelBotones.Location = new System.Drawing.Point(3, 315);
        this.panelBotones.Name = "panelBotones";
        this.panelBotones.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
        this.panelBotones.Size = new System.Drawing.Size(562, 50);
        this.panelBotones.TabIndex = 2;
        // 
        // btnCancelar
        // 
        this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(100, 110, 120);
        this.btnCancelar.FlatAppearance.BorderSize = 0;
        this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnCancelar.ForeColor = System.Drawing.Color.White;
        this.btnCancelar.Location = new System.Drawing.Point(436, 16);
        this.btnCancelar.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
        this.btnCancelar.Name = "btnCancelar";
        this.btnCancelar.Size = new System.Drawing.Size(120, 34);
        this.btnCancelar.TabIndex = 1;
        this.btnCancelar.Text = "Cancelar";
        this.btnCancelar.UseVisualStyleBackColor = false;
        // 
        // btnGuardar
        // 
        this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
        this.btnGuardar.FlatAppearance.BorderSize = 0;
        this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnGuardar.ForeColor = System.Drawing.Color.White;
        this.btnGuardar.Location = new System.Drawing.Point(310, 16);
        this.btnGuardar.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
        this.btnGuardar.Name = "btnGuardar";
        this.btnGuardar.Size = new System.Drawing.Size(120, 34);
        this.btnGuardar.TabIndex = 0;
        this.btnGuardar.Text = "Guardar";
        this.btnGuardar.UseVisualStyleBackColor = false;
        // 
        // FrmConfiguracion
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.White;
        this.ClientSize = new System.Drawing.Size(600, 400);
        this.Controls.Add(this.mainLayout);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "FrmConfiguracion";
        this.Padding = new System.Windows.Forms.Padding(16);
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Configuración";
        this.mainLayout.ResumeLayout(false);
        this.mainLayout.PerformLayout();
        this.grpImpuestos.ResumeLayout(false);
        this.tableLayoutImpuestos.ResumeLayout(false);
        this.tableLayoutImpuestos.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numPorcentajeImpuestos)).EndInit();
        this.grpDescuentoAntiguedad.ResumeLayout(false);
        this.tableLayoutDescuento.ResumeLayout(false);
        this.tableLayoutDescuento.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numDiasAntiguedad)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.numPorcentajeDescuento)).EndInit();
        this.panelBotones.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel mainLayout;
    private System.Windows.Forms.GroupBox grpImpuestos;
    private System.Windows.Forms.TableLayoutPanel tableLayoutImpuestos;
    private System.Windows.Forms.Label lblPorcentajeImpuestos;
    private System.Windows.Forms.NumericUpDown numPorcentajeImpuestos;
    private System.Windows.Forms.Label lblInfoImpuestos;
    private System.Windows.Forms.GroupBox grpDescuentoAntiguedad;
    private System.Windows.Forms.TableLayoutPanel tableLayoutDescuento;
    private System.Windows.Forms.Label lblDiasAntiguedad;
    private System.Windows.Forms.NumericUpDown numDiasAntiguedad;
    private System.Windows.Forms.Label lblPorcentajeDescuento;
    private System.Windows.Forms.NumericUpDown numPorcentajeDescuento;
    private System.Windows.Forms.Label lblInfoDescuento;
    private System.Windows.Forms.FlowLayoutPanel panelBotones;
    private System.Windows.Forms.Button btnCancelar;
    private System.Windows.Forms.Button btnGuardar;
}
