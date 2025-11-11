-- Create a new database called 'TiendaDB'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'TiendaDB'
)
CREATE DATABASE TiendaDB
GO

USE TiendaDB
GO
-- 1. Tablas independientes (sin dependencias)
IF OBJECT_ID('dbo.categoria', 'U') IS NULL
BEGIN
CREATE TABLE [dbo].[categoria] (
    [id_categoria]     INT           IDENTITY (1, 1) NOT NULL,
    [nombre_categoria] VARCHAR (100) NOT NULL,
    [estado]           BIT           CONSTRAINT [DF_categoria_estado] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_categoria] PRIMARY KEY CLUSTERED ([id_categoria] ASC),
    CONSTRAINT [CK_categoria_estado] CHECK ([estado]=(1) OR [estado]=(0))
);
END;

GO
IF OBJECT_ID('dbo.rol', 'U') IS NULL
BEGIN
CREATE TABLE [dbo].[rol] (
    [id_rol]      INT           NOT NULL, --fijado manualmente
    [descripcion] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_rol] PRIMARY KEY CLUSTERED ([id_rol] ASC),
    CONSTRAINT [UQ_rol_descripcion] UNIQUE NONCLUSTERED ([descripcion] ASC)
);
END;

GO
IF OBJECT_ID('dbo.cliente', 'U') IS NULL
BEGIN
CREATE TABLE [dbo].[cliente] (
    [id_cliente]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [dni_cliente]    VARCHAR (8)   NOT NULL,
    [nombre]         VARCHAR (100) NOT NULL,
    [apellido]       VARCHAR (100) NOT NULL,
    [direccion]      VARCHAR (200) NOT NULL,
    [fecha_registro] DATE          CONSTRAINT [DF_cliente_fecha_registro] DEFAULT (getdate()) NOT NULL, --en base a esto se calcula el descuento del cliente registrado
    [email]          VARCHAR (100) NOT NULL,
    [estado]         BIT           CONSTRAINT [DF_cliente_estado] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED ([id_cliente] ASC),
    CONSTRAINT [UQ_cliente_email] UNIQUE NONCLUSTERED ([email] ASC),
    CONSTRAINT [UQ_cliente_dni_cliente] UNIQUE NONCLUSTERED ([dni_cliente] ASC),
    CONSTRAINT [CK_cliente_estado] CHECK ([estado]=(1) OR [estado]=(0))
);
END;

GO
IF OBJECT_ID('dbo.metodo_pago_detalle', 'U') IS NULL
BEGIN
CREATE TABLE [dbo].[metodo_pago_detalle] (
    [id_metodo_pago] BIGINT         IDENTITY (1, 1) NOT NULL,
    [identificacion_pago] BIGINT         NOT NULL, --es el identificador, como un CBU/CVU
    [proveedor_pago]      VARCHAR (100)  NOT NULL, --nombre del proveedor, ej: PayPal, MercadoPago, etc.
    [comision_proveedor]  DECIMAL (5, 2) NOT NULL,
    [estado]              BIT            NOT NULL CONSTRAINT [DF_metodo_pago_detalle_estado] DEFAULT ((1)),
    CONSTRAINT [PK_metodo_pago_detalle] PRIMARY KEY CLUSTERED ([id_metodo_pago] ASC),
    CONSTRAINT [UQ_metodo_pago_detalle_identificacion_pago] UNIQUE NONCLUSTERED ([identificacion_pago] ASC),
    CONSTRAINT [CK_metodo_pago_detalle_comision_proveedor] CHECK ([comision_proveedor]>=(0) AND [comision_proveedor]<=(100))
);
END;

GO
-- 2. Tablas que dependen de las anteriores
IF OBJECT_ID('dbo.producto', 'U') IS NULL
BEGIN
CREATE TABLE [dbo].[producto] (
    [id_producto] BIGINT         IDENTITY (1, 1) NOT NULL,
    [codigo_producto] BIGINT      NOT NULL, --a diferencia de id producto, usado para identificar el producto en la base de datos, este codigo es el que se usa para identificar el producto en busqueda de producto, es como el codigo de barras en la realidad
    [precio_unitario] DECIMAL (10, 2) NOT NULL,
    [nombre_producto] VARCHAR (100)   NOT NULL,
    [descripcion]     VARCHAR (200)   NULL,
    [stock]           SMALLINT        NOT NULL,
    [estado]          BIT             CONSTRAINT [DEFAULT_PRODUCTO_estado] DEFAULT ((1)) NOT NULL,
    [id_categoria]    INT             NOT NULL,
    CONSTRAINT [PK_producto] PRIMARY KEY CLUSTERED ([id_producto] ASC),
    CONSTRAINT [UQ_producto_nombre_producto] UNIQUE NONCLUSTERED ([nombre_producto] ASC),
    CONSTRAINT [UQ_producto_codigo_producto] UNIQUE NONCLUSTERED ([codigo_producto] ASC),
    CONSTRAINT [FK_producto_categoria] FOREIGN KEY ([id_categoria]) REFERENCES [dbo].[categoria] ([id_categoria]),
    CONSTRAINT [CK_producto_stock] CHECK ([stock]>=(0)),
    CONSTRAINT [CK_producto_precio] CHECK ([precio_unitario]>=(0))
);
END;

GO
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_productos_id_categoria'
      AND object_id = OBJECT_ID('dbo.producto')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_productos_id_categoria
        ON dbo.producto (id_categoria ASC);
END;
GO
IF OBJECT_ID('dbo.empleado', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.empleado (
        id_empleado   INT           IDENTITY (1,1) NOT NULL,
        cuil_cuit     VARCHAR(13)   NOT NULL,
        email         VARCHAR(100)  NOT NULL,
        contraseña    VARCHAR(100)     NOT NULL,
        nombre        VARCHAR(100)  NOT NULL,
        apellido      VARCHAR(100)  NOT NULL,
        activo        BIT           CONSTRAINT DF_empleado_activo DEFAULT (1) NOT NULL,
        id_rol        INT           NOT NULL,
        CONSTRAINT PK_empleado PRIMARY KEY CLUSTERED (id_empleado ASC),
        CONSTRAINT UQ_empleado_email UNIQUE (email),
        CONSTRAINT UQ_empleado_cuil UNIQUE (cuil_cuit),
        CONSTRAINT FK_empleado_rol FOREIGN KEY (id_rol) REFERENCES dbo.rol (id_rol),
        CONSTRAINT CK_empleado_activo CHECK (activo IN (0,1))
    );
END;;
GO
-- 3. Tablas que dependen de cliente, empleado, metodo_pago
IF OBJECT_ID('dbo.factura', 'U') IS NULL
BEGIN
CREATE TABLE [dbo].[factura] (
    [id_factura]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [fecha_emision]        DATETIME        CONSTRAINT [DF_factura_fecha_emision] DEFAULT (getdate()) NOT NULL,
    [descuento]            DECIMAL (18, 2) NOT NULL CONSTRAINT [DEFAULT_FACTURA_descuento] DEFAULT ((0)),
    [estado_venta]         VARCHAR (20)    CONSTRAINT [DEFAULT_FACTURA_estado_venta] DEFAULT ('pendiente') NOT NULL,
    [porcentaje_impuestos] TINYINT         CONSTRAINT [DEFAULT_FACTURA_procentaje_impuestos] DEFAULT ((21)) NOT NULL,
    [subtotal]             DECIMAL (18, 2) NOT NULL CONSTRAINT [DEFAULT_FACTURA_subtotal] DEFAULT ((0)),
    [id_empleado]          INT             NOT NULL,
    [id_cliente]          BIGINT          NULL,
    [id_metodo_pago]  BIGINT          NULL,
    CONSTRAINT [PK_factura] PRIMARY KEY CLUSTERED ([id_factura] ASC),
    CONSTRAINT [FK_factura_cliente] FOREIGN KEY ([id_cliente]) REFERENCES [dbo].[cliente] ([id_cliente]),
    CONSTRAINT [FK_factura_metodo_pago_detalle] FOREIGN KEY ([id_metodo_pago]) REFERENCES [dbo].[metodo_pago_detalle] ([id_metodo_pago]),
    CONSTRAINT [FK_factura_empleado] FOREIGN KEY ([id_empleado]) REFERENCES [dbo].[empleado] ([id_empleado]),
    CONSTRAINT [CK_factura_porcentaje_impuestos] CHECK ([porcentaje_impuestos]>=(0) AND [porcentaje_impuestos]<=(100)),
    CONSTRAINT [CK_factura_estado_venta] CHECK ([estado_venta]='cancelada' OR [estado_venta]='pagada' OR [estado_venta]='pendiente'),
    CONSTRAINT [CK_factura_subtotal] CHECK ([subtotal]>=(0))
);
END;

GO
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_facturas_fecha_emision'
      AND object_id = OBJECT_ID('dbo.factura')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_facturas_fecha_emision
        ON dbo.factura (fecha_emision ASC);
END;
GO
-- 4. Tablas dependientes de producto o factura
IF OBJECT_ID('dbo.detalle_factura', 'U') IS NULL
BEGIN
CREATE TABLE [dbo].[detalle_factura] (
    [id_producto]       BIGINT   NOT NULL,
    [id_factura]    BIGINT   NOT NULL,
    [cantidad_producto] SMALLINT NOT NULL,
    CONSTRAINT [PK_detalle_factura] PRIMARY KEY CLUSTERED ([id_factura] ASC, [id_producto] ASC),
    CONSTRAINT [FK_detalle_factura_factura] FOREIGN KEY ([id_factura]) REFERENCES [dbo].[factura] ([id_factura]) ON DELETE CASCADE,
    CONSTRAINT [FK_detalle_factura_producto] FOREIGN KEY ([id_producto]) REFERENCES [dbo].[producto] ([id_producto]) ON DELETE CASCADE,
    CONSTRAINT CK_cantidad_producto CHECK ([cantidad_producto]>(0))
);
END;

GO
IF NOT EXISTS (
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_detalles_factura_id_producto'
      AND object_id = OBJECT_ID('dbo.detalle_factura')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_detalles_factura_id_producto
        ON dbo.detalle_factura (id_producto ASC);
END;
GO

