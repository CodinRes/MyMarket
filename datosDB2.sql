-- =========================
-- ROLES
-- =========================
INSERT INTO [dbo].[rol] (id_rol, descripcion)
VALUES (1, 'Gerente'),
       (2, 'Administrador'),
       (3, 'Cajero');

-- =========================
-- CATEGORÍAS
-- =========================
INSERT INTO [dbo].[categoria] (nombre_categoria)
VALUES ('Bebidas'),
       ('Snacks'),
       ('Limpieza');

-- =========================
-- CLIENTES
-- =========================
INSERT INTO [dbo].[cliente] (dni_cliente, nombre, apellido, direccion, email, estado)
VALUES ('12345678', 'Juan', 'Pérez', 'Calle Falsa 123', 'juan.perez@mail.com', 1),
       ('87654321', 'María', 'Gómez', 'Av. Siempreviva 742', 'maria.gomez@mail.com', 1),
       ('45678912', 'Pedro', 'López', 'San Martín 55', 'pedro.lopez@mail.com', 0);

-- =========================
-- EMPLEADOS
-- =========================
INSERT INTO [dbo].[empleado] (cuil_cuit, email, contraseña, nombre, apellido, id_rol, activo)
VALUES ('20-12345678-9', 'gerente@mail.com', 'pass123', 'Laura', 'Fernández', 1, 1),
       ('20-98765432-1', 'admin@mail.com', 'admin456', 'Carlos', 'Ruiz', 2, 1),
       ('27-11223344-5', 'cajero@mail.com', 'cash789', 'Lucía', 'Martínez', 3, 1);

-- =========================
-- MÉTODO DE PAGO DETALLE
-- =========================
INSERT INTO [dbo].[metodo_pago_detalle] (identificacion_pago, proveedor_pago, comision_proveedor, estado)
VALUES (1001, 'Visa', 2.50, 1),
       (1002, 'Mastercard', 2.75, 1),
       (1003, 'MercadoPago', 3.00, 1);

-- =========================
-- PRODUCTOS
-- =========================
INSERT INTO [dbo].[producto] (codigo_producto, precio_unitario, nombre_producto, descripcion, stock, id_categoria)
VALUES (2001, 150.00, 'Coca Cola 1.5L', 'Gaseosa sabor cola', 50, 1),
       (2002, 80.00, 'Papas Fritas Lays', 'Snacks de papa 120g', 100, 2),
       (2003, 300.00, 'Detergente Magistral 750ml', 'Líquido para vajilla', 30, 3);

-- =========================
-- LOTES
-- =========================
INSERT INTO [dbo].[lote] (fecha_compra, fecha_vencimiento, codigo_producto)
VALUES ('2025-01-10', '2026-01-10', 2001),
       ('2025-02-01', '2025-08-01', 2002),
       ('2025-03-15', '2026-03-15', 2003);

-- =========================
-- FACTURAS
-- =========================
INSERT INTO [dbo].[factura] (descuento, subtotal, id_empleado, dni_cliente, identificacion_pago, estado_venta)
VALUES (0.00, 230.00, 3, '12345678', 1001, 'pagada'),
       (10.00, 380.00, 2, '87654321', 1002, 'pendiente'),
       (0.00, 150.00, 3, '45678912', 1003, 'cancelada');

-- =========================
-- DETALLE FACTURA
-- =========================
INSERT INTO [dbo].[detalle_factura] (codigo_factura, codigo_producto, cantidad_producto)
VALUES (1, 2001, 1),  -- Juan compra 1 Coca Cola
       (1, 2002, 1),  -- Juan compra 1 Papas
       (2, 2003, 2),  -- María compra 2 Detergentes
       (3, 2001, 1);  -- Pedro intenta comprar 1 Coca Cola (pero factura cancelada)
