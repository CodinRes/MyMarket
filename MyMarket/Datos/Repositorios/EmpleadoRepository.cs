using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Infraestructura;
using MyMarket.Datos.Modelos;

namespace MyMarket.Datos.Repositorios;

/// <summary>
///     Acceso a datos para operaciones relacionadas con empleados y roles.
/// </summary>
public class EmpleadoRepository
{
    private readonly SqlConnectionFactory _connectionFactory;
    private string? _passwordColumnName;

    /// <summary>
    ///     Recibe la fábrica de conexiones necesaria para interactuar con la base de datos.
    /// </summary>
    public EmpleadoRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    /// <summary>
    ///     Verifica las credenciales de un empleado y devuelve su información si es válido.
    /// </summary>
    public EmpleadoDto? Autenticar(string cuilCuit, string contrasenia)
    {
        if (string.IsNullOrWhiteSpace(cuilCuit))
        {
            throw new ArgumentException("El CUIL no puede estar vacío.", nameof(cuilCuit));
        }

        if (string.IsNullOrEmpty(contrasenia))
        {
            throw new ArgumentException("La contraseña no puede estar vacía.", nameof(contrasenia));
        }

        using var connection = _connectionFactory.CreateOpenConnection();
        var passwordColumn = ResolverColumnaContrasena(connection);
        using var command = connection.CreateCommand();
        command.CommandText = $@"SELECT e.*, r.descripcion AS rol_descripcion
                                 FROM empleado e
                                 INNER JOIN rol r ON e.id_rol = r.id_rol
                                 WHERE e.activo = 1 AND e.cuil_cuit = @cuil AND e.[{passwordColumn}] = @password";

        // Parámetros parametrizados para evitar inyección SQL.
        command.Parameters.Add(new SqlParameter("@cuil", SqlDbType.VarChar, 13) { Value = cuilCuit });
        command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 100) { Value = contrasenia });

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        return MapearEmpleado(reader);
    }

    /// <summary>
    ///     Obtiene la lista completa de empleados con su rol asociado.
    /// </summary>
    public IReadOnlyList<EmpleadoDto> ObtenerEmpleados()
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT e.*, r.descripcion AS rol_descripcion
                                 FROM empleado e
                                 INNER JOIN rol r ON e.id_rol = r.id_rol
                                 ORDER BY e.id_empleado";

        using var reader = command.ExecuteReader();
        var empleados = new List<EmpleadoDto>();
        while (reader.Read())
        {
            empleados.Add(MapearEmpleado(reader));
        }

        return empleados;
    }

    /// <summary>
    ///     Devuelve los roles disponibles ordenados alfabéticamente.
    /// </summary>
    public IReadOnlyList<RolDto> ObtenerRoles()
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT id_rol, descripcion FROM rol ORDER BY descripcion";

        using var reader = command.ExecuteReader();
        var roles = new List<RolDto>();
        while (reader.Read())
        {
            roles.Add(new RolDto
            {
                IdRol = reader.GetInt32(0),
                Descripcion = reader.GetString(1)
            });
        }

        return roles;
    }

    /// <summary>
    ///     Inserta un nuevo empleado con la contraseña indicada.
    /// </summary>
    public EmpleadoDto CrearEmpleado(EmpleadoDto empleado, string contrasenia)
    {
        if (empleado is null)
        {
            throw new ArgumentNullException(nameof(empleado));
        }

        if (string.IsNullOrWhiteSpace(empleado.CuilCuit))
        {
            throw new ArgumentException("El CUIL/CUIT es obligatorio.", nameof(empleado));
        }

        if (string.IsNullOrWhiteSpace(empleado.Email))
        {
            throw new ArgumentException("El correo electrónico es obligatorio.", nameof(empleado));
        }

        if (string.IsNullOrWhiteSpace(empleado.Nombre))
        {
            throw new ArgumentException("El nombre es obligatorio.", nameof(empleado));
        }

        if (string.IsNullOrWhiteSpace(empleado.Apellido))
        {
            throw new ArgumentException("El apellido es obligatorio.", nameof(empleado));
        }

        if (string.IsNullOrWhiteSpace(contrasenia))
        {
            throw new ArgumentException("La contraseña no puede estar vacía.", nameof(contrasenia));
        }

        using var connection = _connectionFactory.CreateOpenConnection();
        var passwordColumn = ResolverColumnaContrasena(connection);

        using var command = connection.CreateCommand();
        command.CommandText = $@"INSERT INTO empleado (cuil_cuit, email, [{passwordColumn}], nombre, apellido, activo, id_rol)
                                 VALUES (@cuil, @correo, @password, @nombre, @apellido, @activo, @rol);
                                 SELECT CAST(SCOPE_IDENTITY() AS INT);";

        command.Parameters.Add(new SqlParameter("@cuil", SqlDbType.VarChar, 13) { Value = empleado.CuilCuit });
        command.Parameters.Add(new SqlParameter("@correo", SqlDbType.VarChar, 100) { Value = empleado.Email });
        command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 100) { Value = contrasenia });
        command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar, 100) { Value = empleado.Nombre });
        command.Parameters.Add(new SqlParameter("@apellido", SqlDbType.VarChar, 100) { Value = empleado.Apellido });
        command.Parameters.Add(new SqlParameter("@activo", SqlDbType.Bit) { Value = empleado.Activo });
        command.Parameters.Add(new SqlParameter("@rol", SqlDbType.Int) { Value = empleado.IdRol });

        // Ejecuta el comando y captura el identificador generado por SQL Server.
        var resultado = command.ExecuteScalar();
        var nuevoId = Convert.ToInt32(resultado);
        empleado.IdEmpleado = nuevoId;

        return empleado;
    }

    /// <summary>
    ///     Actualiza los datos de un empleado, incluyendo la contraseña si se provee un valor.
    /// </summary>
    public void ActualizarEmpleado(EmpleadoDto empleado, string? contrasenia)
    {
        if (empleado is null)
        {
            throw new ArgumentNullException(nameof(empleado));
        }

        if (empleado.IdEmpleado <= 0)
        {
            throw new ArgumentException("El empleado debe tener un identificador válido.", nameof(empleado));
        }

        if (string.IsNullOrWhiteSpace(empleado.Email))
        {
            throw new ArgumentException("El correo electrónico es obligatorio.", nameof(empleado));
        }

        if (string.IsNullOrWhiteSpace(empleado.Nombre))
        {
            throw new ArgumentException("El nombre es obligatorio.", nameof(empleado));
        }

        if (string.IsNullOrWhiteSpace(empleado.Apellido))
        {
            throw new ArgumentException("El apellido es obligatorio.", nameof(empleado));
        }

        using var connection = _connectionFactory.CreateOpenConnection();
        string? passwordColumn = null;
        if (!string.IsNullOrWhiteSpace(contrasenia))
        {
            passwordColumn = ResolverColumnaContrasena(connection);
        }

        using var command = connection.CreateCommand();
        if (passwordColumn is not null)
        {
            command.CommandText = $@"UPDATE empleado
                                     SET email = @correo,
                                         activo = @activo,
                                         id_rol = @rol,
                                         nombre = @nombre,
                                         apellido = @apellido,
                                         [{passwordColumn}] = @password
                                     WHERE id_empleado = @id";
            command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 100) { Value = contrasenia });
        }
        else
        {
            command.CommandText = @"UPDATE empleado
                                     SET email = @correo,
                                         activo = @activo,
                                         id_rol = @rol,
                                         nombre = @nombre,
                                         apellido = @apellido
                                     WHERE id_empleado = @id";
        }

        command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = empleado.IdEmpleado });
        command.Parameters.Add(new SqlParameter("@correo", SqlDbType.VarChar, 100) { Value = empleado.Email });
        command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar, 100) { Value = empleado.Nombre });
        command.Parameters.Add(new SqlParameter("@apellido", SqlDbType.VarChar, 100) { Value = empleado.Apellido });
        command.Parameters.Add(new SqlParameter("@activo", SqlDbType.Bit) { Value = empleado.Activo });
        command.Parameters.Add(new SqlParameter("@rol", SqlDbType.Int) { Value = empleado.IdRol });

        command.ExecuteNonQuery();
    }

    /// <summary>
    ///     Desactiva al empleado indicado.
    /// </summary>
    public void DesactivarEmpleado(int idEmpleado)
    {
        CambiarEstadoEmpleado(idEmpleado, false);
    }

    /// <summary>
    ///     Cambia el estado de actividad de un empleado.
    /// </summary>
    public void CambiarEstadoEmpleado(int idEmpleado, bool activo)
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "UPDATE empleado SET activo = @activo WHERE id_empleado = @id";
        command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = idEmpleado });
        command.Parameters.Add(new SqlParameter("@activo", SqlDbType.Bit) { Value = activo });

        command.ExecuteNonQuery();
    }

    /// <summary>
    ///     Detecta y almacena el nombre real de la columna de contraseña ya que puede contener tilde.
    /// </summary>
    private string ResolverColumnaContrasena(SqlConnection connection)
    {
        if (!string.IsNullOrEmpty(_passwordColumnName))
        {
            return _passwordColumnName!;
        }

        // Consulta los metadatos de la tabla para averiguar cómo está escrita la columna.
        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT COLUMN_NAME
                                 FROM INFORMATION_SCHEMA.COLUMNS
                                 WHERE TABLE_NAME = 'empleado' AND COLUMN_NAME IN (N'contraseña', N'contrasena')";

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            _passwordColumnName = reader.GetString(0);
            return _passwordColumnName!;
        }

        throw new InvalidOperationException("La tabla empleado no tiene una columna de contraseña configurada.");
    }

    /// <summary>
    ///     Construye la entidad de dominio a partir de un <see cref="SqlDataReader"/>.
    /// </summary>
    private EmpleadoDto MapearEmpleado(SqlDataReader reader)
    {
        return new EmpleadoDto
        {
            IdEmpleado = reader.GetInt32(reader.GetOrdinal("id_empleado")),
            CuilCuit = reader.GetString(reader.GetOrdinal("cuil_cuit")),
            Email = reader.GetString(reader.GetOrdinal("email")),
            Activo = reader.GetBoolean(reader.GetOrdinal("activo")),
            IdRol = reader.GetInt32(reader.GetOrdinal("id_rol")),
            RolDescripcion = reader.GetString(reader.GetOrdinal("rol_descripcion")),
            Nombre = TryGetString(reader, "nombre"),
            Apellido = TryGetString(reader, "apellido")
        };
    }

    /// <summary>
    ///     Intenta obtener una cadena de una columna que podría no existir o contener nulos.
    /// </summary>
    private static string? TryGetString(SqlDataReader reader, string columnName)
    {
        try
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }
        catch (IndexOutOfRangeException)
        {
            return null;
        }
    }
}
