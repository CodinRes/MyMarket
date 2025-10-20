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
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Text = "Control de Stock";
            this.Padding = new System.Windows.Forms.Padding(16);

            // Split container principal
            this.splitContainerPrincipal = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrincipal)).BeginInit();
            this.splitContainerPrincipal.Panel1.SuspendLayout();
            this.splitContainerPrincipal.Panel2.SuspendLayout();
            this.splitContainerPrincipal.SuspendLayout();

            this.splitContainerPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPrincipal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainerPrincipal.SplitterDistance = 400;

            // Panel superior - Productos
            this.panelProductos = new System.Windows.Forms.Panel();
            this.panelProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            
            this.lblProductos = new System.Windows.Forms.Label();
            this.lblProductos.Text = "Gestión de Productos";
            this.lblProductos.Font = new System.Drawing.Font(this.lblProductos.Font.FontFamily, 12, System.Drawing.FontStyle.Bold);
            this.lblProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProductos.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblProductos.AutoSize = true;

            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.dgvProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.panelBotonesProductos = new System.Windows.Forms.FlowLayoutPanel();
            this.panelBotonesProductos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotonesProductos.Height = 48;
            this.panelBotonesProductos.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelBotonesProductos.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);

            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.btnAgregarProducto.Text = "Agregar Producto";
            this.btnAgregarProducto.Width = 140;
            this.btnAgregarProducto.Height = 32;

            this.btnEditarProducto = new System.Windows.Forms.Button();
            this.btnEditarProducto.Text = "Editar Producto";
            this.btnEditarProducto.Width = 140;
            this.btnEditarProducto.Height = 32;

            this.btnActualizarStock = new System.Windows.Forms.Button();
            this.btnActualizarStock.Text = "Actualizar Stock";
            this.btnActualizarStock.Width = 140;
            this.btnActualizarStock.Height = 32;

            this.panelBotonesProductos.Controls.Add(this.btnAgregarProducto);
            this.panelBotonesProductos.Controls.Add(this.btnEditarProducto);
            this.panelBotonesProductos.Controls.Add(this.btnActualizarStock);

            this.panelProductos.Controls.Add(this.dgvProductos);
            this.panelProductos.Controls.Add(this.panelBotonesProductos);
            this.panelProductos.Controls.Add(this.lblProductos);

            // Panel inferior - Categorías
            this.panelCategorias = new System.Windows.Forms.Panel();
            this.panelCategorias.Dock = System.Windows.Forms.DockStyle.Fill;

            this.lblCategorias = new System.Windows.Forms.Label();
            this.lblCategorias.Text = "Gestión de Categorías";
            this.lblCategorias.Font = new System.Drawing.Font(this.lblCategorias.Font.FontFamily, 12, System.Drawing.FontStyle.Bold);
            this.lblCategorias.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCategorias.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.lblCategorias.AutoSize = true;

            this.dgvCategorias = new System.Windows.Forms.DataGridView();
            this.dgvCategorias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCategorias.AllowUserToAddRows = false;
            this.dgvCategorias.AllowUserToDeleteRows = false;
            this.dgvCategorias.ReadOnly = true;
            this.dgvCategorias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategorias.MultiSelect = false;
            this.dgvCategorias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.panelBotonesCategorias = new System.Windows.Forms.FlowLayoutPanel();
            this.panelBotonesCategorias.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotonesCategorias.Height = 48;
            this.panelBotonesCategorias.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelBotonesCategorias.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);

            this.btnAgregarCategoria = new System.Windows.Forms.Button();
            this.btnAgregarCategoria.Text = "Agregar Categoría";
            this.btnAgregarCategoria.Width = 140;
            this.btnAgregarCategoria.Height = 32;

            this.btnEliminarCategoria = new System.Windows.Forms.Button();
            this.btnEliminarCategoria.Text = "Eliminar Categoría";
            this.btnEliminarCategoria.Width = 140;
            this.btnEliminarCategoria.Height = 32;

            this.panelBotonesCategorias.Controls.Add(this.btnAgregarCategoria);
            this.panelBotonesCategorias.Controls.Add(this.btnEliminarCategoria);

            this.panelCategorias.Controls.Add(this.dgvCategorias);
            this.panelCategorias.Controls.Add(this.panelBotonesCategorias);
            this.panelCategorias.Controls.Add(this.lblCategorias);

            // Agregar paneles al split container
            this.splitContainerPrincipal.Panel1.Controls.Add(this.panelProductos);
            this.splitContainerPrincipal.Panel2.Controls.Add(this.panelCategorias);
            this.Controls.Add(this.splitContainerPrincipal);

            this.splitContainerPrincipal.Panel1.ResumeLayout(false);
            this.splitContainerPrincipal.Panel1.PerformLayout();
            this.splitContainerPrincipal.Panel2.ResumeLayout(false);
            this.splitContainerPrincipal.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrincipal)).EndInit();
            this.splitContainerPrincipal.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerPrincipal;
        private System.Windows.Forms.Panel panelProductos;
        private System.Windows.Forms.Label lblProductos;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.FlowLayoutPanel panelBotonesProductos;
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.Button btnEditarProducto;
        private System.Windows.Forms.Button btnActualizarStock;
        private System.Windows.Forms.Panel panelCategorias;
        private System.Windows.Forms.Label lblCategorias;
        private System.Windows.Forms.DataGridView dgvCategorias;
        private System.Windows.Forms.FlowLayoutPanel panelBotonesCategorias;
        private System.Windows.Forms.Button btnAgregarCategoria;
        private System.Windows.Forms.Button btnEliminarCategoria;
    }