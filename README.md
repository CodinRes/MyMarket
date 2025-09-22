# MyMarket

Aplicación Windows Forms orientada a la gestión diaria de un minimercado. El proyecto incluye pantallas para emitir y consultar recibos, controlar el stock, analizar datos y administrar usuarios, junto con una capa de acceso a datos y utilidades para persistir el estado de la sesión.

## Arquitectura del proyecto

La solución sigue una estructura por capas para separar responsabilidades:

- **Program.cs** inicializa la aplicación, configura los estilos visuales y crea la instancia principal (`FrmPrincipal`).
- **Formularios/** agrupa todas las ventanas de la interfaz.
  - `Formularios/Principal` contiene el `FrmPrincipal` (contenedor principal y menú) y el `FrmBienvenida`.
  - `Formularios/Autenticacion`, `Formularios/Usuarios`, `Formularios/Recibos`, `Formularios/Inventario` y `Formularios/Analitica` albergan los formularios especializados de cada módulo.
- **Datos/** implementa la interacción con SQL Server.
  - `Infraestructura/SqlConnectionFactory` centraliza la creación de conexiones a partir del `App.config`.
  - `Repositorios/` expone repositorios para empleados, productos y facturas.
  - `Modelos/` define objetos de transferencia (`Dto`) y contratos para registrar ventas (`FacturaCabecera`, `FacturaDetalle`).
- **Servicios/Estado/** contiene la lógica para persistir información del usuario autenticado y del estado de la ventana en disco (`AlmacenEstadoAplicacion`, `EstadoAplicacion`, `EstadoEmpleado`).

## Flujo principal y formularios

1. **Inicio de la aplicación**: `Program.cs` crea la fábrica de conexiones y abre `FrmPrincipal` dentro de un bloque `try/catch` para capturar fallos críticos.
2. **FrmPrincipal**: actúa como contenedor de módulos. Gestiona el menú lateral, controla el acceso a cada formulario según el rol del usuario y persiste/restaura la sesión mediante `AlmacenEstadoAplicacion`.
3. **Autenticación**: `FrmLogin` consume `EmpleadoRepository` para validar credenciales, mostrar mensajes de error específicos y devolver el empleado autenticado.
4. **Gestión de usuarios**: `FrmGestionUsuarios` muestra la grilla de empleados, habilita acciones según el rol y permite crear, activar o desactivar usuarios.
5. **Ventas y reportes**:
   - `FrmEmitirRecibo` prepara controles y totales de ejemplo, dejando mensajes aclaratorios para la lógica pendiente.
   - `FrmRecibosEmitidos` muestra recibos simulados y valida la selección antes de mostrar el detalle.
   - `FrmControlStock` simula el ajuste de inventario desde la interfaz.
   - `FrmAnalisisDatos` sirve como base para visualizar gráficos con `System.Windows.Forms.DataVisualization`.

## Documentación detallada del código

### Capa de datos (`Datos/`)

- **Infraestructura/SqlConnectionFactory**: encapsula la lectura de la cadena de conexión `TiendaDb` desde `App.config` y crea conexiones `SqlConnection` abiertas listas para usar. Lanza excepciones descriptivas si la configuración es incorrecta.
- **Repositorios**:
  - `EmpleadoRepository` expone operaciones de autenticación, consulta y administración de empleados. Resuelve dinámicamente la columna de contraseña permitiendo bases con `contraseña` o `contrasena`.
  - `ProductoRepository` ofrece métodos para obtener productos activos y actualizar el stock.
  - `FacturaRepository` inserta cabeceras y detalles de facturas en transacción, con validaciones de entrada y manejo de conversiones.
- **Modelos**: los `Dto` (`EmpleadoDto`, `ProductoDto`, `FacturaDto`, etc.) representan datos planos que circulan entre UI y repositorios. `FacturaCabecera` y `FacturaDetalle` modelan los datos requeridos para generar una venta completa.

### Capa de interfaz (`Formularios/`)

- **Principal**: `FrmPrincipal` organiza el menú lateral, orquesta la carga de formularios hijos en el panel central y administra la sesión activa. `FrmBienvenida` muestra un mensaje inicial cuando no hay módulos abiertos.
- **Autenticacion**: `FrmLogin` valida credenciales, muestra mensajes de ayuda y expone la propiedad `EmpleadoAutenticado` para devolver el resultado.
- **Usuarios**: `FrmGestionUsuarios` muestra la grilla de empleados, habilita acciones según el rol y coordina la creación o desactivación con `EmpleadoRepository`.
- **Recibos**: `FrmEmitirRecibo` inicializa la interfaz y comunica qué acciones están pendientes de implementación, mientras que `FrmRecibosEmitidos` despliega un historial simulado.
- **Inventario**: `FrmControlStock` permite ajustar el stock usando botones de incremento/decremento sobre una grilla cargada con datos de ejemplo.
- **Analitica**: `FrmAnalisisDatos` contiene un gráfico base listo para conectar con datos reales.

### Gestión de estado (`Servicios/Estado/`)

- **AlmacenEstadoAplicacion** persiste un objeto `EstadoAplicacion` en `%AppData%/MyMarket/appstate.json`. Ofrece métodos `Cargar` y `Guardar` que manejan errores de forma silenciosa para no afectar la experiencia del usuario.
- **EstadoAplicacion** conserva el último tamaño/estado de la ventana y la sesión activa, representada por **EstadoEmpleado**, una versión serializable de `EmpleadoDto`.
- En `FrmPrincipal`, los métodos `RestaurarEstadoAnterior` y `GuardarEstadoActual` sincronizan la sesión visual con la persistencia, permitiendo que al reabrir la aplicación se restablezca el usuario y el estado de la ventana.

## Configuración y ejecución

1. **Requisitos**
   - .NET 8 SDK.
   - Windows para ejecutar la interfaz Windows Forms.
   - SQL Server (o LocalDB) accesible con la cadena definida en `App.config`.

2. **Configurar la cadena de conexión**
   - Edita `MyMarket/App.config` y ajusta el valor `connectionString` del nodo `TiendaDb` según tu entorno.

3. **Compilación**
   ```bash
   dotnet build
   ```

4. **Ejecución**
   ```bash
   dotnet run --project MyMarket/MyMarket.csproj
   ```

## Notas adicionales

- El estado de la sesión se serializa en `%AppData%/MyMarket/appstate.json`. Puedes eliminar el archivo para reiniciar la sesión guardada.
- Los repositorios usan `SqlConnectionFactory` para abrir conexiones y exponen métodos con validaciones explícitas (por ejemplo, verificación de CUIL y contraseñas).
- Las pantallas de ventas incluyen datos simulados para fines de prototipo y pueden reemplazarse por consultas reales a la base cuando se implemente la lógica correspondiente.
- Todo el código cuenta con comentarios XML e inline que describen la finalidad de cada clase, método y bloque relevante para facilitar su mantenimiento.
