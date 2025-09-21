using System;
using System.Data;
using Microsoft.Data.SqlClient;
using MyMarket.Data.Models;

namespace MyMarket.Data;

public class EmpleadoRepository
{
    private readonly SqlConnectionFactory _connectionFactory;
    private string? _passwordColumnName;

    public EmpleadoRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

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

        command.Parameters.Add(new SqlParameter("@cuil", SqlDbType.VarChar, 13) { Value = cuilCuit });
        command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar, 100) { Value = contrasenia });

        using var reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return null;
        }

        var empleado = new EmpleadoDto
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

        return empleado;
    }

    private string ResolverColumnaContrasena(SqlConnection connection)
    {
        if (!string.IsNullOrEmpty(_passwordColumnName))
        {
            return _passwordColumnName!;
        }

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
