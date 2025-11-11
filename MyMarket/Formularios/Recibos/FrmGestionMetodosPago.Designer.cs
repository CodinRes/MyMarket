namespace MyMarket.Formularios.Recibos;

partial class FrmGestionMetodosPago
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
        this.grpMetodos = new System.Windows.Forms.GroupBox();
        this.dgvMetodosPago = new System.Windows.Forms.DataGridView();
        this.panelBotones = new System.Windows.Forms.FlowLayoutPanel();
        this.btnCerrar = new System.Windows.Forms.Button();
        this.btnActivarDesactivar = new System.Windows.Forms.Button();
        this.btnNuevo = new System.Windows.Forms.Button();
        this.mainLayout.SuspendLayout();
        this.grpMetodos.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.dgvMetodosPago)).BeginInit();
        this.panelBotones.SuspendLayout();
        this.SuspendLayout();
        // 
        // mainLayout
        // 
        this.mainLayout.ColumnCount = 1;
        this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.mainLayout.Controls.Add(this.grpMetodos, 0, 0);
        this.mainLayout.Controls.Add(this.panelBotones, 0, 1);
        this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        this.mainLayout.Location = new System.Drawing.Point(16, 16);
        this.mainLayout.Name = "mainLayout";
        this.mainLayout.RowCount = 2;
        this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.mainLayout.Size = new System.Drawing.Size(768, 484);
        this.mainLayout.TabIndex = 0;
        // 
        // grpMetodos
        // 
        this.grpMetodos.Controls.Add(this.dgvMetodosPago);
        this.grpMetodos.Dock = System.Windows.Forms.DockStyle.Fill;
        this.grpMetodos.Location = new System.Drawing.Point(0, 0);
        this.grpMetodos.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
        this.grpMetodos.Name = "grpMetodos";
        this.grpMetodos.Padding = new System.Windows.Forms.Padding(12, 16, 12, 12);
        this.grpMetodos.Size = new System.Drawing.Size(768, 424);
        this.grpMetodos.TabIndex = 0;
        this.grpMetodos.TabStop = false;
        this.grpMetodos.Text = "Métodos de pago registrados";
        // 
        // dgvMetodosPago
        // 
        this.dgvMetodosPago.AllowUserToAddRows = false;
        this.dgvMetodosPago.AllowUserToDeleteRows = false;
        this.dgvMetodosPago.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
        this.dgvMetodosPago.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        this.dgvMetodosPago.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dgvMetodosPago.BackgroundColor = System.Drawing.Color.White;
        this.dgvMetodosPago.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.dgvMetodosPago.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
        this.dgvMetodosPago.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
        dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(45, 50, 55);
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
        dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dgvMetodosPago.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        this.dgvMetodosPago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
        dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(45, 45, 45);
        dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(230, 240, 250);
        dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        this.dgvMetodosPago.DefaultCellStyle = dataGridViewCellStyle3;
        this.dgvMetodosPago.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvMetodosPago.EnableHeadersVisualStyles = false;
        this.dgvMetodosPago.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);
        this.dgvMetodosPago.Location = new System.Drawing.Point(12, 32);
        this.dgvMetodosPago.MultiSelect = false;
        this.dgvMetodosPago.Name = "dgvMetodosPago";
        this.dgvMetodosPago.ReadOnly = true;
        this.dgvMetodosPago.RowHeadersVisible = false;
        this.dgvMetodosPago.RowTemplate.Height = 28;
        this.dgvMetodosPago.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvMetodosPago.Size = new System.Drawing.Size(744, 380);
        this.dgvMetodosPago.TabIndex = 0;
        // 
        // panelBotones
        // 
        this.panelBotones.AutoSize = true;
        this.panelBotones.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.panelBotones.Controls.Add(this.btnCerrar);
        this.panelBotones.Controls.Add(this.btnActivarDesactivar);
        this.panelBotones.Controls.Add(this.btnNuevo);
        this.panelBotones.Dock = System.Windows.Forms.DockStyle.Fill;
        this.panelBotones.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
        this.panelBotones.Location = new System.Drawing.Point(0, 440);
        this.panelBotones.Margin = new System.Windows.Forms.Padding(0);
        this.panelBotones.Name = "panelBotones";
        this.panelBotones.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
        this.panelBotones.Size = new System.Drawing.Size(768, 44);
        this.panelBotones.TabIndex = 1;
        // 
        // btnCerrar
        // 
        this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(100, 110, 120);
        this.btnCerrar.FlatAppearance.BorderSize = 0;
        this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnCerrar.ForeColor = System.Drawing.Color.White;
        this.btnCerrar.Location = new System.Drawing.Point(642, 8);
        this.btnCerrar.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
        this.btnCerrar.Name = "btnCerrar";
        this.btnCerrar.Size = new System.Drawing.Size(120, 36);
        this.btnCerrar.TabIndex = 2;
        this.btnCerrar.Text = "Cerrar";
        this.btnCerrar.UseVisualStyleBackColor = false;
        // 
        // btnActivarDesactivar
        // 
        this.btnActivarDesactivar.BackColor = System.Drawing.Color.FromArgb(200, 130, 55);
        this.btnActivarDesactivar.FlatAppearance.BorderSize = 0;
        this.btnActivarDesactivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnActivarDesactivar.ForeColor = System.Drawing.Color.White;
        this.btnActivarDesactivar.Location = new System.Drawing.Point(492, 8);
        this.btnActivarDesactivar.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
        this.btnActivarDesactivar.Name = "btnActivarDesactivar";
        this.btnActivarDesactivar.Size = new System.Drawing.Size(144, 36);
        this.btnActivarDesactivar.TabIndex = 1;
        this.btnActivarDesactivar.Text = "Activar/Desactivar";
        this.btnActivarDesactivar.UseVisualStyleBackColor = false;
        // 
        // btnNuevo
        // 
        this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
        this.btnNuevo.FlatAppearance.BorderSize = 0;
        this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.btnNuevo.ForeColor = System.Drawing.Color.White;
        this.btnNuevo.Location = new System.Drawing.Point(366, 8);
        this.btnNuevo.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
        this.btnNuevo.Name = "btnNuevo";
        this.btnNuevo.Size = new System.Drawing.Size(120, 36);
        this.btnNuevo.TabIndex = 0;
        this.btnNuevo.Text = "Nuevo";
        this.btnNuevo.UseVisualStyleBackColor = false;
        // 
        // FrmGestionMetodosPago
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.White;
        this.ClientSize = new System.Drawing.Size(800, 516);
        this.Controls.Add(this.mainLayout);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "FrmGestionMetodosPago";
        this.Padding = new System.Windows.Forms.Padding(16);
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Gestión de métodos de pago";
        this.mainLayout.ResumeLayout(false);
        this.mainLayout.PerformLayout();
        this.grpMetodos.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.dgvMetodosPago)).EndInit();
        this.panelBotones.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel mainLayout;
    private System.Windows.Forms.GroupBox grpMetodos;
    private System.Windows.Forms.DataGridView dgvMetodosPago;
    private System.Windows.Forms.FlowLayoutPanel panelBotones;
    private System.Windows.Forms.Button btnCerrar;
    private System.Windows.Forms.Button btnActivarDesactivar;
    private System.Windows.Forms.Button btnNuevo;
}
