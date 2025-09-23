INSERT INTO [dbo].[rol] ([id_rol], [descripcion]) VALUES (1, N'gerente')
INSERT INTO [dbo].[rol] ([id_rol], [descripcion]) VALUES (2, N'administrador')
INSERT INTO [dbo].[rol] ([id_rol], [descripcion]) VALUES (3, N'vendedor')


INSERT INTO categoria (id_categoria, nombre_categoria) VALUES
(1, 'Electrodomésticos'),
(2, 'Ropa'),
(3, 'Alimentos');

INSERT INTO [dbo].[cliente] ([dni_cliente], [nombre], [apellido], [direccion], [fecha_registro], [email], [estado]) VALUES (N'11223344', N'Carlos', N'Gómez', N'Ruta 9 Km 50', N'2025-09-21', N'carlos.gomez@mail.com', 0)
INSERT INTO [dbo].[cliente] ([dni_cliente], [nombre], [apellido], [direccion], [fecha_registro], [email], [estado]) VALUES (N'12345678', N'Juan', N'Pérez', N'Av. Siempre Viva 123', N'2025-09-21', N'juan.perez@mail.com', 1)
INSERT INTO [dbo].[cliente] ([dni_cliente], [nombre], [apellido], [direccion], [fecha_registro], [email], [estado]) VALUES (N'87654321', N'María', N'López', N'Calle Falsa 456', N'2025-09-21', N'maria.lopez@mail.com', 1)

SET IDENTITY_INSERT [dbo].[empleado] ON
INSERT INTO [dbo].[empleado] ([id_empleado], [cuil_cuit], [email], [contraseña], [nombre], [apellido], [activo], [id_rol]) VALUES (1, N'12345678910', N'1234@gmail.com', N'1234', N'Ana', N'Pérez', 1, 1)
INSERT INTO [dbo].[empleado] ([id_empleado], [cuil_cuit], [email], [contraseña], [nombre], [apellido], [activo], [id_rol]) VALUES (5, N'20123456789', N'admin@tienda.com', N'admin123', N'Carlos', N'Fernández', 1, 2)
INSERT INTO [dbo].[empleado] ([id_empleado], [cuil_cuit], [email], [contraseña], [nombre], [apellido], [activo], [id_rol]) VALUES (6, N'20987654321', N'vendedor@tienda.com', N'venta456', N'Lucía', N'Gómez', 1, 3)
INSERT INTO [dbo].[empleado] ([id_empleado], [cuil_cuit], [email], [contraseña], [nombre], [apellido], [activo], [id_rol]) VALUES (7, N'20991234567', N'cajero@tienda.com', N'caja789', N'Pedro', N'López', 1, 3)
SET IDENTITY_INSERT [dbo].[empleado] OFF


INSERT INTO [dbo].[producto] ([codigo_producto], [precio], [nombre], [descripcion], [stock], [estado], [id_categoria]) VALUES (1001, CAST(1500.00 AS Decimal(10, 2)), N'Licuadora', N'Licuadora 500W', 20, 1, 1)
INSERT INTO [dbo].[producto] ([codigo_producto], [precio], [nombre], [descripcion], [stock], [estado], [id_categoria]) VALUES (1002, CAST(3500.00 AS Decimal(10, 2)), N'Microondas', N'Microondas digital 20L', 15, 1, 1)
INSERT INTO [dbo].[producto] ([codigo_producto], [precio], [nombre], [descripcion], [stock], [estado], [id_categoria]) VALUES (2001, CAST(800.00 AS Decimal(10, 2)), N'Camisa', N'Camisa manga larga', 30, 1, 2)
INSERT INTO [dbo].[producto] ([codigo_producto], [precio], [nombre], [descripcion], [stock], [estado], [id_categoria]) VALUES (3001, CAST(50.00 AS Decimal(10, 2)), N'Arroz 1kg', N'Bolsa de arroz', 100, 1, 3)

INSERT INTO metodo_pago_detalle (identificacion_pago, proveedor_pago, comision_proveedor, estado) VALUES
(1, 'Visa', 2.50, 1),
(2, 'MasterCard', 2.70, 1),
(3, 'MercadoPago', 3.00, 1);

INSERT INTO lote (fecha_compra, fecha_vencimiento, codigo_producto) VALUES
(GETDATE(), DATEADD(MONTH, 12, GETDATE()), 1001),
(GETDATE(), DATEADD(MONTH, 12, GETDATE()), 3001),
(GETDATE(), DATEADD(MONTH, 6, GETDATE()), 2001);

SET IDENTITY_INSERT [dbo].[factura] ON
INSERT INTO [dbo].[factura] ([codigo_factura], [fecha_emision], [descuento], [estado_venta], [porcentaje_impuestos], [subtotal], [id_empleado], [dni_cliente], [identificacion_pago]) VALUES (12, N'2025-09-21 19:46:37', CAST(0.00 AS Decimal(18, 2)), N'pendiente', 21, CAST(1550.00 AS Decimal(18, 2)), 1, N'12345678', 3)
INSERT INTO [dbo].[factura] ([codigo_factura], [fecha_emision], [descuento], [estado_venta], [porcentaje_impuestos], [subtotal], [id_empleado], [dni_cliente], [identificacion_pago]) VALUES (13, N'2025-09-21 19:46:37', CAST(100.00 AS Decimal(18, 2)), N'pagada', 21, CAST(3500.00 AS Decimal(18, 2)), 5, N'87654321', 1)
SET IDENTITY_INSERT [dbo].[factura] OFF

INSERT INTO [dbo].[detalle_factura] ([codigo_factura], [codigo_producto], [cantidad_producto]) VALUES (12, 1001, 2)
INSERT INTO [dbo].[detalle_factura] ([codigo_factura], [codigo_producto], [cantidad_producto]) VALUES (12, 3001, 30)
INSERT INTO [dbo].[detalle_factura] ([codigo_factura], [codigo_producto], [cantidad_producto]) VALUES (13, 1002, 1)
