-- 1. Tablas independientes (sin dependencias)
CREATE TABLE [dbo].[categoria] (
    [id_categoria]     INT           IDENTITY (1, 1) NOT NULL,
    [nombre_categoria] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_categoria] PRIMARY KEY CLUSTERED ([id_categoria] ASC)
);

CREATE TABLE [dbo].[rol] (
    [id_rol]      INT           NOT NULL,
    [descripcion] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_rol] PRIMARY KEY CLUSTERED ([id_rol] ASC),
    CONSTRAINT [UQ_rol_descripcion] UNIQUE NONCLUSTERED ([descripcion] ASC)
);

CREATE TABLE [dbo].[cliente] (
    [dni_cliente]    VARCHAR (8)   NOT NULL,
    [nombre]         VARCHAR (100) NOT NULL,
    [apellido]       VARCHAR (100) NOT NULL,
    [direccion]      VARCHAR (200) NOT NULL,
    [fecha_registro] DATE          CONSTRAINT [DF_cliente_fecha_registro] DEFAULT (getdate()) NOT NULL,
    [email]          VARCHAR (100) NOT NULL,
    [estado]         BIT           CONSTRAINT [DF_cliente_estado] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED ([dni_cliente] ASC),
    CONSTRAINT [UQ_cliente_email] UNIQUE NONCLUSTERED ([email] ASC),
    CONSTRAINT [CK_cliente_estado] CHECK ([estado]=(1) OR [estado]=(0))
);

CREATE TABLE [dbo].[metodo_pago_detalle] (
    [identificacion_pago] BIGINT         NOT NULL,
    [proveedor_pago]      VARCHAR (100)  NOT NULL,
    [comision_proveedor]  DECIMAL (5, 2) NOT NULL,
    [estado]              BIT            NOT NULL,
    CONSTRAINT [PK_metodo_pago_detalle] PRIMARY KEY CLUSTERED ([identificacion_pago] ASC)
);

-- 2. Tablas que dependen de las anteriores
CREATE TABLE [dbo].[producto] (
    [codigo_producto] BIGINT          NOT NULL,
    [precio_unitario] DECIMAL (10, 2) NOT NULL,
    [nombre_producto] VARCHAR (100)   NOT NULL,
    [descripcion]     VARCHAR (200)   NULL,
    [stock]           SMALLINT        NOT NULL,
    [estado]          BIT             CONSTRAINT [DEFAULT_PRODUCTO_estado] DEFAULT ((1)) NOT NULL,
    [id_categoria]    INT             NOT NULL,
    CONSTRAINT [PK_producto] PRIMARY KEY CLUSTERED ([codigo_producto] ASC),
    CONSTRAINT [UQ_producto_nombre] UNIQUE NONCLUSTERED ([nombre_producto] ASC),
    CONSTRAINT [FK_producto__categoria] FOREIGN KEY ([id_categoria]) REFERENCES [dbo].[categoria] ([id_categoria]),
    CONSTRAINT [CK_producto_stock] CHECK ([stock]>=(0)),
    CONSTRAINT [CK_producto_precio] CHECK ([precio_unitario]>=(0))
);

CREATE NONCLUSTERED INDEX [IX_productos_id_categoria]
    ON [dbo].[producto]([id_categoria] ASC);

CREATE TABLE [dbo].[empleado] (
    [id_empleado] INT           IDENTITY (1, 1) NOT NULL,
    [cuil_cuit]   VARCHAR (13)  NOT NULL,
    [email]       VARCHAR (100) NOT NULL,
    [contraseña]  VARCHAR (100) NOT NULL,
    [nombre]      VARCHAR (100) NOT NULL,
    [apellido]    VARCHAR (100) NOT NULL,
    [activo]      BIT           CONSTRAINT [DF_empleado_activo] DEFAULT ((1)) NOT NULL,
    [id_rol]      INT           NOT NULL,
    CONSTRAINT [PK_empleado] PRIMARY KEY CLUSTERED ([id_empleado] ASC),
    CONSTRAINT [UQ_empleado_email] UNIQUE NONCLUSTERED ([email] ASC),
    CONSTRAINT [UQ_empleado_cuil] UNIQUE NONCLUSTERED ([cuil_cuit] ASC),
    CONSTRAINT [FK_empleado__rol] FOREIGN KEY ([id_rol]) REFERENCES [dbo].[rol] ([id_rol]),
    CONSTRAINT [CK_empleado_activo] CHECK ([activo]=(1) OR [activo]=(0))
);

-- 3. Tablas que dependen de cliente, empleado, metodo_pago
CREATE TABLE [dbo].[factura] (
    [codigo_factura]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [fecha_emision]        DATETIME        CONSTRAINT [DF_factura_fecha_emision] DEFAULT (getdate()) NOT NULL,
    [descuento]            DECIMAL (18, 2) NOT NULL,
    [estado_venta]         VARCHAR (20)    CONSTRAINT [DEFAULT_FACTURA_estado_venta] DEFAULT ('pendiente') NOT NULL,
    [porcentaje_impuestos] TINYINT         CONSTRAINT [DEFAULT_FACTURA_procentaje_impuestos] DEFAULT ((21)) NOT NULL,
    [subtotal]             DECIMAL (18, 2) NOT NULL,
    [id_empleado]          INT             NOT NULL,
    [dni_cliente]          VARCHAR (8)     NULL,
    [identificacion_pago]  BIGINT          NOT NULL,
    CONSTRAINT [PK_factura] PRIMARY KEY CLUSTERED ([codigo_factura] ASC),
    CONSTRAINT [FK_factura_cliente] FOREIGN KEY ([dni_cliente]) REFERENCES [dbo].[cliente] ([dni_cliente]),
    CONSTRAINT [FK_factura_metodo_pago_detalle] FOREIGN KEY ([identificacion_pago]) REFERENCES [dbo].[metodo_pago_detalle] ([identificacion_pago]),
    CONSTRAINT [FK_factura_empleado] FOREIGN KEY ([id_empleado]) REFERENCES [dbo].[empleado] ([id_empleado]),
    CONSTRAINT [CK_factura_porcentaje_impuestos] CHECK ([porcentaje_impuestos]>=(0) AND [porcentaje_impuestos]<=(100)),
    CONSTRAINT [CK_factura_estado_venta] CHECK ([estado_venta]='cancelada' OR [estado_venta]='pagada' OR [estado_venta]='pendiente')
);

CREATE NONCLUSTERED INDEX [IX_facturas_fecha_emision]
    ON [dbo].[factura]([fecha_emision] ASC);

-- 4. Tablas dependientes de producto o factura
CREATE TABLE [dbo].[detalle_factura] (
    [codigo_factura]    BIGINT   NOT NULL,
    [codigo_producto]   BIGINT   NOT NULL,
    [cantidad_producto] SMALLINT NOT NULL,
    CONSTRAINT [PK_detalle_factura] PRIMARY KEY CLUSTERED ([codigo_factura] ASC, [codigo_producto] ASC),
    CONSTRAINT [FK_detalle_factura_factura] FOREIGN KEY ([codigo_factura]) REFERENCES [dbo].[factura] ([codigo_factura]) ON DELETE CASCADE,
    CONSTRAINT [FK_detalle_factura_producto] FOREIGN KEY ([codigo_producto]) REFERENCES [dbo].[producto] ([codigo_producto]) ON DELETE CASCADE
);

CREATE NONCLUSTERED INDEX [IX_detalles_factura_codigo_producto]
    ON [dbo].[detalle_factura]([codigo_producto] ASC);

CREATE TABLE [dbo].[lote] (
    [numero_lote]       INT    IDENTITY (1, 1) NOT NULL,
    [fecha_compra]      DATE   CONSTRAINT [DF_lote_fecha_compra] DEFAULT (getdate()) NOT NULL,
    [fecha_vencimiento] DATE   NOT NULL,
    [codigo_producto]   BIGINT NOT NULL,
    CONSTRAINT [PK_lote] PRIMARY KEY CLUSTERED ([numero_lote] ASC),
    CONSTRAINT [FK_lote__producto] FOREIGN KEY ([codigo_producto]) REFERENCES [dbo].[producto] ([codigo_producto]),
    CONSTRAINT [CK_lote_fechas] CHECK ([fecha_vencimiento]>[fecha_compra])
);

CREATE NONCLUSTERED INDEX [IX_lotes_fecha_vencimiento]
    ON [dbo].[lote]([fecha_vencimiento] ASC);
