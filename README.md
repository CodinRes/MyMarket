# MyMarket

Aplicación Windows Forms orientada a la gestión diaria de un minimercado. El proyecto incluye pantallas para emitir y consultar recibos, controlar el stock, analizar datos y administrar usuarios, junto con una capa de acceso a datos y utilidades para persistir el estado de la sesión.

## Arquitectura del proyecto

La solución sigue una estructura por capas para separar responsabilidades:

- **Program.cs** inicializa la aplicación, configura los estilos visuales y crea la instancia principal (`FrmPrincipal`).
- **Forms/** agrupa todas las ventanas de la interfaz.
  - `Forms/Main` contiene el `FrmPrincipal` (contenedor principal y menú) y el `FrmBienvenida`.
  - `Forms/Authentication`, `Forms/Users`, `Forms/Receipts`, `Forms/Inventory` y `Forms/Analytics` albergan los formularios especializados de cada módulo.
- **Data/** implementa la interacción con SQL Server.
  - `Infrastructure/SqlConnectionFactory` centraliza la creación de conexiones a partir del `App.config`.
  - `Repositories/` expone repositorios para empleados, productos y facturas.
  - `Models/` define objetos de transferencia (`Dto`) y contratos para registrar ventas (`FacturaCabecera`, `FacturaDetalle`).
- **Services/State/** contiene la lógica para persistir información del usuario autenticado y del estado de la ventana en disco (`AppStateStorage`, `AppState`, `EmpleadoState`).

## Flujo principal y formularios

1. **Inicio de la aplicación**: `Program.cs` crea la fábrica de conexiones y abre `FrmPrincipal` dentro de un bloque `try/catch` para capturar fallos críticos.
2. **FrmPrincipal**: actúa como contenedor de módulos. Gestiona el menú lateral, controla el acceso a cada formulario según el rol del usuario y persiste/restaura la sesión mediante `AppStateStorage`.
3. **Autenticación**: `FrmLogin` consume `EmpleadoRepository` para validar credenciales, mostrar mensajes de error específicos y devolver el empleado autenticado.
4. **Gestión de usuarios**: `FrmGestionUsuarios` muestra la grilla de empleados, habilita acciones según el rol y permite crear, activar o desactivar usuarios.
5. **Ventas y reportes**:
   - `FrmEmitirRecibo` construye la interfaz dinámicamente, calcula subtotales y total del recibo en memoria.
   - `FrmRecibosEmitidos` muestra recibos simulados y valida la selección antes de mostrar el detalle.
   - `FrmControlStock` simula el ajuste de inventario desde la interfaz.
   - `FrmAnalisisDatos` sirve como base para visualizar gráficos con `System.Windows.Forms.DataVisualization`.

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
