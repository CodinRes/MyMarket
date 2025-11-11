-- Seed de roles
INSERT INTO dbo.rol (id_rol, descripcion)
VALUES (1, 'Gerente'),
       (2, 'Administrador'),
       (3, 'Cajero');
GO

-- Seed de categorías
INSERT INTO dbo.categoria (nombre_categoria)
VALUES ('Bebidas'),
       ('Snacks'),
       ('Limpieza');
GO

-- Seed de clientes
INSERT INTO dbo.cliente (dni_cliente, nombre, apellido, direccion, email)
VALUES ('30111222', 'Lucía', 'Gómez', 'Av. Siempre Viva 123', 'lucia.gomez@example.com'),
       ('32123123', 'Martín', 'Pérez', 'Calle Falsa 456', 'martin.perez@example.com'),
       ('33199887', 'Ana', 'Ríos', 'Ruta 9 KM 15', 'ana.rios@example.com');
GO

-- Seed de métodos de pago
INSERT INTO dbo.metodo_pago_detalle (identificacion_pago, proveedor_pago, comision_proveedor, estado)
VALUES (1234567890123456, 'Mercado Pago', 2.50, 1),
       (6543210987654321, 'Transferencia Bancaria', 0.00, 1);
GO

-- Seed de empleados
INSERT INTO dbo.empleado (cuil_cuit, email, contraseña, nombre, apellido, activo, id_rol)
VALUES ('20-12345678-3', 'admin@tienda.com', 'Admin123', 'Sofía', 'Suárez', 1, 2),
       ('27-87654321-4', 'caja1@tienda.com', 'Caja123', 'Diego', 'López', 1, 3),
       ('20123456789', 'gerente@tienda.com', 'gerente123', 'Omar', 'González', 1, 1);
GO

-- Seed de productos
INSERT INTO dbo.producto (codigo_producto, precio_unitario, nombre_producto, descripcion, stock, id_categoria)
VALUES (1001001001, 250.00, 'Gaseosa Cola 2L', 'Bebida gaseosa sabor cola', 120, 1),
       (1001002002, 150.00, 'Papas Fritas 200g', 'Bolsa de papas fritas sabor clásico', 80, 2),
       (1001003003, 320.00, 'Detergente Líquido 900ml', 'Detergente para vajilla', 60, 3);
GO

-- Seed de facturas
INSERT INTO dbo.factura (descuento, estado_venta, porcentaje_impuestos, subtotal, id_empleado, id_cliente, id_metodo_pago)
VALUES (0.00, 'pagada', 21, 820.00, 1, 1, 1),
       (50.00, 'pendiente', 21, 470.00, 2, 2, 2);
GO

-- Seed de detalles de factura
INSERT INTO dbo.detalle_factura (id_factura, id_producto, cantidad_producto)
VALUES (1, 1, 2),
       (1, 2, 1),
       (2, 2, 2),
       (2, 3, 1);
GO
