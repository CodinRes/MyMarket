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
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Text = "Control de Stock";

            // Split container principal
            this.splitContainerPrincipal = new System.Windows.Forms.SplitContainer();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.dgvCategorias = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrincipal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).BeginInit();
            this.splitContainerPrincipal.Panel1.SuspendLayout();
            this.splitContainerPrincipal.Panel2.SuspendLayout();
            this.splitContainerPrincipal.SuspendLayout();

            this.splitContainerPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPrincipal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // Elevar un poco la sección de categorías
            this.splitContainerPrincipal.SplitterDistance = 320;

            // Panel superior - Productos
            this.panelProductos = new System.Windows.Forms.Panel();
            this.panelProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProductos.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            
            this.lblProductos = new System.Windows.Forms.Label();
            this.lblProductos.Text = "Gestión de Productos";
            this.lblProductos.Font = new System.Drawing.Font(this.lblProductos.Font.FontFamily, 12, System.Drawing.FontStyle.Bold);
            this.lblProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProductos.Padding = new System.Windows.Forms.Padding(12, 12, 0, 8);
            this.lblProductos.AutoSize = true;

            // Panel de búsqueda de productos
            this.panelBusquedaProductos = new System.Windows.Forms.Panel();
            this.panelBusquedaProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBusquedaProductos.Height = 50;
            this.panelBusquedaProductos.Padding = new System.Windows.Forms.Padding(12, 0, 12, 8);

            this.lblBuscarProducto = new System.Windows.Forms.Label();
            this.lblBuscarProducto.Text = "Buscar producto:";
            this.lblBuscarProducto.Location = new System.Drawing.Point(12, 15);
            this.lblBuscarProducto.AutoSize = true;

            this.txtBuscarProducto = new System.Windows.Forms.TextBox();
            this.txtBuscarProducto.Location = new System.Drawing.Point(120, 12);
            this.txtBuscarProducto.Width = 250;

            this.btnBuscarProducto = new System.Windows.Forms.Button();
            this.btnBuscarProducto.Text = "Buscar";
            this.btnBuscarProducto.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
            this.btnBuscarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarProducto.ForeColor = System.Drawing.Color.White;
            this.btnBuscarProducto.Location = new System.Drawing.Point(376, 10);
            this.btnBuscarProducto.Size = new System.Drawing.Size(90, 28);

            this.btnLimpiarProducto = new System.Windows.Forms.Button();
            this.btnLimpiarProducto.Text = "Limpiar";
            this.btnLimpiarProducto.BackColor = System.Drawing.Color.FromArgb(100, 110, 120);
            this.btnLimpiarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarProducto.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarProducto.Location = new System.Drawing.Point(472, 10);
            this.btnLimpiarProducto.Size = new System.Drawing.Size(90, 28);

            // Checkbox para mostrar inactivos (productos)
            this.chkMostrarInactivosProductos = new System.Windows.Forms.CheckBox();
            this.chkMostrarInactivosProductos.Text = "Mostrar inactivos";
            this.chkMostrarInactivosProductos.AutoSize = true;
            this.chkMostrarInactivosProductos.Location = new System.Drawing.Point(580, 14);

            this.panelBusquedaProductos.Controls.Add(this.lblBuscarProducto);
            this.panelBusquedaProductos.Controls.Add(this.txtBuscarProducto);
            this.panelBusquedaProductos.Controls.Add(this.btnBuscarProducto);
            this.panelBusquedaProductos.Controls.Add(this.btnLimpiarProducto);
            this.panelBusquedaProductos.Controls.Add(this.chkMostrarInactivosProductos);

            // this.dgvProductos ya está instanciado arriba
            // Expandir la grilla de productos para ocupar el espacio disponible
            this.dgvProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductos.BackgroundColor = System.Drawing.Color.White;
            this.dgvProductos.RowHeadersVisible = false;

            this.panelBotonesProductos = new System.Windows.Forms.FlowLayoutPanel();
            // Anclar los botones abajo para que la grilla crezca hacia el centro
            this.panelBotonesProductos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotonesProductos.Height = 48;
            this.panelBotonesProductos.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelBotonesProductos.Padding = new System.Windows.Forms.Padding(12, 8, 0, 0);

            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.btnAgregarProducto.Text = "Agregar Producto";
            this.btnAgregarProducto.Width = 140;
            this.btnAgregarProducto.Height = 30;
            this.btnAgregarProducto.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
            this.btnAgregarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarProducto.ForeColor = System.Drawing.Color.White;
            this.btnAgregarProducto.Margin = new System.Windows.Forms.Padding(12, 0, 8, 8);

            this.btnEditarProducto = new System.Windows.Forms.Button();
            this.btnEditarProducto.Text = "Editar Producto";
            this.btnEditarProducto.Width = 140;
            this.btnEditarProducto.Height = 30;
            this.btnEditarProducto.BackColor = System.Drawing.Color.FromArgb(100, 110, 120);
            this.btnEditarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditarProducto.ForeColor = System.Drawing.Color.White;
            this.btnEditarProducto.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);

            this.btnActualizarStock = new System.Windows.Forms.Button();
            this.btnActualizarStock.Text = "Actualizar Stock";
            this.btnActualizarStock.Width = 140;
            this.btnActualizarStock.Height = 30;
            this.btnActualizarStock.BackColor = System.Drawing.Color.FromArgb(100, 110, 120);
            this.btnActualizarStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarStock.ForeColor = System.Drawing.Color.White;
            this.btnActualizarStock.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);

            // Botón activar/desactivar producto
            this.btnToggleEstadoProducto = new System.Windows.Forms.Button();
            this.btnToggleEstadoProducto.Text = "Activar/Desactivar";
            this.btnToggleEstadoProducto.Width = 150;
            this.btnToggleEstadoProducto.Height = 30;
            this.btnToggleEstadoProducto.BackColor = System.Drawing.Color.FromArgb(100, 110, 120);
            this.btnToggleEstadoProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleEstadoProducto.ForeColor = System.Drawing.Color.White;
            this.btnToggleEstadoProducto.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);

            this.panelBotonesProductos.Controls.Add(this.btnAgregarProducto);
            this.panelBotonesProductos.Controls.Add(this.btnEditarProducto);
            this.panelBotonesProductos.Controls.Add(this.btnActualizarStock);
            this.panelBotonesProductos.Controls.Add(this.btnToggleEstadoProducto);

            this.panelProductos.Controls.Add(this.dgvProductos);
            this.panelProductos.Controls.Add(this.panelBotonesProductos);
            this.panelProductos.Controls.Add(this.panelBusquedaProductos);
            this.panelProductos.Controls.Add(this.lblProductos);

            // Panel inferior - Categorías
            this.panelCategorias = new System.Windows.Forms.Panel();
            this.panelCategorias.Dock = System.Windows.Forms.DockStyle.Fill;
            // Agregar un pequeño padding inferior para que no quede al límite de la ventana
            this.panelCategorias.Padding = new System.Windows.Forms.Padding(0, 0, 0, 12);

            this.lblCategorias = new System.Windows.Forms.Label();
            this.lblCategorias.Text = "Gestión de Categorías";
            this.lblCategorias.Font = new System.Drawing.Font(this.lblCategorias.Font.FontFamily, 12, System.Drawing.FontStyle.Bold);
            this.lblCategorias.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCategorias.Padding = new System.Windows.Forms.Padding(12, 12, 0, 8);
            this.lblCategorias.AutoSize = true;

            // Panel de búsqueda de categorías
            this.panelBusquedaCategorias = new System.Windows.Forms.Panel();
            this.panelBusquedaCategorias.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBusquedaCategorias.Height = 50;
            this.panelBusquedaCategorias.Padding = new System.Windows.Forms.Padding(12, 0, 12, 8);

            this.lblBuscarCategoria = new System.Windows.Forms.Label();
            this.lblBuscarCategoria.Text = "Buscar categoría:";
            this.lblBuscarCategoria.Location = new System.Drawing.Point(12, 15);
            this.lblBuscarCategoria.AutoSize = true;

            this.txtBuscarCategoria = new System.Windows.Forms.TextBox();
            this.txtBuscarCategoria.Location = new System.Drawing.Point(125, 12);
            this.txtBuscarCategoria.Width = 250;

            this.btnBuscarCategoria = new System.Windows.Forms.Button();
            this.btnBuscarCategoria.Text = "Buscar";
            this.btnBuscarCategoria.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
            this.btnBuscarCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarCategoria.ForeColor = System.Drawing.Color.White;
            this.btnBuscarCategoria.Location = new System.Drawing.Point(381, 10);
            this.btnBuscarCategoria.Size = new System.Drawing.Size(90, 28);

            this.btnLimpiarCategoria = new System.Windows.Forms.Button();
            this.btnLimpiarCategoria.Text = "Limpiar";
            this.btnLimpiarCategoria.BackColor = System.Drawing.Color.FromArgb(100, 110, 120);
            this.btnLimpiarCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarCategoria.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarCategoria.Location = new System.Drawing.Point(477, 10);
            this.btnLimpiarCategoria.Size = new System.Drawing.Size(90, 28);

            // Checkbox para mostrar inactivas (categorías)
            this.chkMostrarInactivasCategorias = new System.Windows.Forms.CheckBox();
            this.chkMostrarInactivasCategorias.Text = "Mostrar inactivas";
            this.chkMostrarInactivasCategorias.AutoSize = true;
            this.chkMostrarInactivasCategorias.Location = new System.Drawing.Point(585, 14);

            this.panelBusquedaCategorias.Controls.Add(this.lblBuscarCategoria);
            this.panelBusquedaCategorias.Controls.Add(this.txtBuscarCategoria);
            this.panelBusquedaCategorias.Controls.Add(this.btnBuscarCategoria);
            this.panelBusquedaCategorias.Controls.Add(this.btnLimpiarCategoria);
            this.panelBusquedaCategorias.Controls.Add(this.chkMostrarInactivasCategorias);

            // this.dgvCategorias ya está instanciado arriba
            // Hacer que la grilla de categorías use el espacio disponible entre la búsqueda y los botones
            this.dgvCategorias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCategorias.AllowUserToAddRows = false;
            this.dgvCategorias.AllowUserToDeleteRows = false;
            this.dgvCategorias.ReadOnly = true;
            this.dgvCategorias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategorias.MultiSelect = false;
            this.dgvCategorias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategorias.BackgroundColor = System.Drawing.Color.White;
            this.dgvCategorias.RowHeadersVisible = false;

            this.panelBotonesCategorias = new System.Windows.Forms.FlowLayoutPanel();
            // Anclar los botones abajo para separarlos del borde inferior
            this.panelBotonesCategorias.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotonesCategorias.Height = 48;
            this.panelBotonesCategorias.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.panelBotonesCategorias.Padding = new System.Windows.Forms.Padding(12, 8, 0, 0);

            this.btnAgregarCategoria = new System.Windows.Forms.Button();
            this.btnAgregarCategoria.Text = "Agregar Categoría";
            this.btnAgregarCategoria.Width = 140;
            this.btnAgregarCategoria.Height = 30;
            this.btnAgregarCategoria.BackColor = System.Drawing.Color.FromArgb(55, 130, 200);
            this.btnAgregarCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarCategoria.ForeColor = System.Drawing.Color.White;
            this.btnAgregarCategoria.Margin = new System.Windows.Forms.Padding(12, 0, 8, 8);

            // Botón activar/desactivar categoría
            this.btnToggleEstadoCategoria = new System.Windows.Forms.Button();
            this.btnToggleEstadoCategoria.Text = "Activar/Desactivar";
            this.btnToggleEstadoCategoria.Width = 150;
            this.btnToggleEstadoCategoria.Height = 30;
            this.btnToggleEstadoCategoria.BackColor = System.Drawing.Color.FromArgb(100, 110, 120);
            this.btnToggleEstadoCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleEstadoCategoria.ForeColor = System.Drawing.Color.White;
            this.btnToggleEstadoCategoria.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);

            this.panelBotonesCategorias.Controls.Add(this.btnAgregarCategoria);
            this.panelBotonesCategorias.Controls.Add(this.btnToggleEstadoCategoria);

            this.panelCategorias.Controls.Add(this.dgvCategorias);
            this.panelCategorias.Controls.Add(this.panelBotonesCategorias);
            this.panelCategorias.Controls.Add(this.panelBusquedaCategorias);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).EndInit();
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerPrincipal;
        private System.Windows.Forms.Panel panelProductos;
        private System.Windows.Forms.Label lblProductos;
        private System.Windows.Forms.Panel panelBusquedaProductos;
        private System.Windows.Forms.Label lblBuscarProducto;
        private System.Windows.Forms.TextBox txtBuscarProducto;
        private System.Windows.Forms.Button btnBuscarProducto;
        private System.Windows.Forms.Button btnLimpiarProducto;
        private System.Windows.Forms.CheckBox chkMostrarInactivosProductos;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.FlowLayoutPanel panelBotonesProductos;
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.Button btnEditarProducto;
        private System.Windows.Forms.Button btnActualizarStock;
        private System.Windows.Forms.Button btnToggleEstadoProducto;
        private System.Windows.Forms.Panel panelCategorias;
        private System.Windows.Forms.Label lblCategorias;
        private System.Windows.Forms.Panel panelBusquedaCategorias;
        private System.Windows.Forms.Label lblBuscarCategoria;
        private System.Windows.Forms.TextBox txtBuscarCategoria;
        private System.Windows.Forms.Button btnBuscarCategoria;
        private System.Windows.Forms.Button btnLimpiarCategoria;
        private System.Windows.Forms.CheckBox chkMostrarInactivasCategorias;
        private System.Windows.Forms.DataGridView dgvCategorias;
        private System.Windows.Forms.FlowLayoutPanel panelBotonesCategorias;
        private System.Windows.Forms.Button btnAgregarCategoria;
        private System.Windows.Forms.Button btnToggleEstadoCategoria;
    }