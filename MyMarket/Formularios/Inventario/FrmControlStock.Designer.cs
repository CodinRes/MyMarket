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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 520);
            this.Text = "Control de Stock";
            this.Padding = new System.Windows.Forms.Padding(12);

            this.panelTop = new System.Windows.Forms.FlowLayoutPanel();
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 48;
            this.panelTop.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelTop.Padding = new System.Windows.Forms.Padding(0,6,0,0);

            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();

            this.btnAgregar.Text = "Agregar";
            this.btnEditar.Text = "Editar";
            this.btnEliminar.Text = "Eliminar";
            this.btnRefrescar.Text = "Refrescar";

            this.btnAgregar.AutoSize = true;
            this.btnEditar.AutoSize = true;
            this.btnEliminar.AutoSize = true;
            this.btnRefrescar.AutoSize = true;

            this.panelTop.Controls.Add(this.btnAgregar);
            this.panelTop.Controls.Add(this.btnEditar);
            this.panelTop.Controls.Add(this.btnEliminar);
            this.panelTop.Controls.Add(this.btnRefrescar);

            this.dgv = new System.Windows.Forms.DataGridView();
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.MultiSelect = false;
            this.dgv.ReadOnly = true;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv.RowTemplate.Height = 26;

            // Columns
            this.dgv.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Codigo", HeaderText = "Código", DataPropertyName = "CodigoProducto", ReadOnly = true });
            this.dgv.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Nombre", HeaderText = "Nombre", DataPropertyName = "Nombre", ReadOnly = true, Width = 220 });
            this.dgv.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Descripcion", HeaderText = "Descripción", DataPropertyName = "Descripcion", ReadOnly = true, Width = 250 });
            this.dgv.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Precio", HeaderText = "Precio", DataPropertyName = "Precio", ReadOnly = true, DefaultCellStyle = { Format = "C2" } });
            this.dgv.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "Stock", HeaderText = "Stock", DataPropertyName = "Stock", ReadOnly = true });
            this.dgv.Columns.Add(new System.Windows.Forms.DataGridViewTextBoxColumn { Name = "IdCategoria", HeaderText = "IdCategoria", DataPropertyName = "IdCategoria", ReadOnly = true });
            this.dgv.Columns.Add(new System.Windows.Forms.DataGridViewCheckBoxColumn { Name = "Estado", HeaderText = "Activo", DataPropertyName = "Estado", ReadOnly = true });

            // Layout
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.panelTop);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.FlowLayoutPanel panelTop;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnRefrescar;
    }