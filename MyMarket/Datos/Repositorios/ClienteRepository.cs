using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using MyMarket.Datos.Infraestructura;
using MyMarket.Datos.Modelos;

namespace MyMarket.Datos.Repositorios;

/// <summary>
///     Proporciona operaciones CRUD básicas para la entidad <c>cliente</c>.
/// </summary>
public class ClienteRepository
{
    private readonly SqlConnectionFactory _connectionFactory;

    /// <summary>
    ///     Inicializa el repositorio con la fábrica de conexiones requerida.
    /// </summary>
    public ClienteRepository(SqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    /// <summary>
    ///     Obtiene todos los clientes ordenados alfabéticamente.
    /// </summary>
    public IReadOnlyList<ClienteDto> ObtenerClientes()
    {
        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"SELECT dni_cliente, nombre, apellido, direccion, fecha_registro, email, estado
                                 FROM cliente
                                 ORDER BY apellido, nombre";

        using var reader = command.ExecuteReader();
        var clientes = new List<ClienteDto>();
        while (reader.Read())
        {
            clientes.Add(new ClienteDto
            {
                Dni = reader.GetString(0),
                Nombre = reader.GetString(1),
                Apellido = reader.GetString(2),
                Direccion = reader.GetString(3),
                FechaRegistro = reader.GetDateTime(4),
                Email = reader.GetString(5),
                Activo = reader.GetBoolean(6)
            });
        }

        return clientes;
    }

    /// <summary>
    ///     Inserta un nuevo cliente en la base de datos.
    /// </summary>
    public void CrearCliente(ClienteDto cliente)
    {
        if (cliente is null)
        {
            throw new ArgumentNullException(nameof(cliente));
        }

        if (string.IsNullOrWhiteSpace(cliente.Dni))
        {
            throw new ArgumentException("El DNI es obligatorio.", nameof(cliente));
        }

        if (string.IsNullOrWhiteSpace(cliente.Nombre))
        {
            throw new ArgumentException("El nombre es obligatorio.", nameof(cliente));
        }

        if (string.IsNullOrWhiteSpace(cliente.Apellido))
        {
            throw new ArgumentException("El apellido es obligatorio.", nameof(cliente));
        }

        if (string.IsNullOrWhiteSpace(cliente.Direccion))
        {
            throw new ArgumentException("La dirección es obligatoria.", nameof(cliente));
        }

        if (string.IsNullOrWhiteSpace(cliente.Email))
        {
            throw new ArgumentException("El correo electrónico es obligatorio.", nameof(cliente));
        }

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO cliente (dni_cliente, nombre, apellido, direccion, email, estado)
                                 VALUES (@dni, @nombre, @apellido, @direccion, @correo, @estado);";

        command.Parameters.Add(new SqlParameter("@dni", SqlDbType.VarChar, 8) { Value = cliente.Dni });
        command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar, 100) { Value = cliente.Nombre });
        command.Parameters.Add(new SqlParameter("@apellido", SqlDbType.VarChar, 100) { Value = cliente.Apellido });
        command.Parameters.Add(new SqlParameter("@direccion", SqlDbType.VarChar, 200) { Value = cliente.Direccion });
        command.Parameters.Add(new SqlParameter("@correo", SqlDbType.VarChar, 100) { Value = cliente.Email });
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit) { Value = cliente.Activo });

        command.ExecuteNonQuery();
    }

    /// <summary>
    ///     Actualiza los datos de un cliente existente.
    /// </summary>
    public void ActualizarCliente(ClienteDto cliente, string dniOriginal)
    {
        if (cliente is null)
        {
            throw new ArgumentNullException(nameof(cliente));
        }

        if (string.IsNullOrWhiteSpace(dniOriginal))
        {
            throw new ArgumentException("Debe indicar el DNI original.", nameof(dniOriginal));
        }

        if (string.IsNullOrWhiteSpace(cliente.Dni))
        {
            throw new ArgumentException("El DNI es obligatorio.", nameof(cliente));
        }

        if (string.IsNullOrWhiteSpace(cliente.Nombre))
        {
            throw new ArgumentException("El nombre es obligatorio.", nameof(cliente));
        }

        if (string.IsNullOrWhiteSpace(cliente.Apellido))
        {
            throw new ArgumentException("El apellido es obligatorio.", nameof(cliente));
        }

        if (string.IsNullOrWhiteSpace(cliente.Direccion))
        {
            throw new ArgumentException("La dirección es obligatoria.", nameof(cliente));
        }

        if (string.IsNullOrWhiteSpace(cliente.Email))
        {
            throw new ArgumentException("El correo electrónico es obligatorio.", nameof(cliente));
        }

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = @"UPDATE cliente
                                 SET dni_cliente = @nuevoDni,
                                     nombre = @nombre,
                                     apellido = @apellido,
                                     direccion = @direccion,
                                     email = @correo,
                                     estado = @estado
                                 WHERE dni_cliente = @dniOriginal";

        command.Parameters.Add(new SqlParameter("@nuevoDni", SqlDbType.VarChar, 8) { Value = cliente.Dni });
        command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar, 100) { Value = cliente.Nombre });
        command.Parameters.Add(new SqlParameter("@apellido", SqlDbType.VarChar, 100) { Value = cliente.Apellido });
        command.Parameters.Add(new SqlParameter("@direccion", SqlDbType.VarChar, 200) { Value = cliente.Direccion });
        command.Parameters.Add(new SqlParameter("@correo", SqlDbType.VarChar, 100) { Value = cliente.Email });
        command.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit) { Value = cliente.Activo });
        command.Parameters.Add(new SqlParameter("@dniOriginal", SqlDbType.VarChar, 8) { Value = dniOriginal });

        var filasAfectadas = command.ExecuteNonQuery();
        if (filasAfectadas == 0)
        {
            throw new InvalidOperationException("No se encontró el cliente especificado.");
        }
    }

    /// <summary>
    ///     Verifica si existe un cliente registrado con el DNI indicado.
    /// </summary>
    public bool ExisteClientePorDni(string dni)
    {
        if (string.IsNullOrWhiteSpace(dni))
        {
            throw new ArgumentException("El DNI es obligatorio.", nameof(dni));
        }

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(1) FROM cliente WHERE dni_cliente = @dni";
        command.Parameters.Add(new SqlParameter("@dni", SqlDbType.VarChar, 8) { Value = dni });

        var resultado = command.ExecuteScalar();
        return Convert.ToInt32(resultado) > 0;
    }

    /// <summary>
    ///     Determina si el correo electrónico ya está asociado a otro cliente.
    /// </summary>
    public bool ExisteClientePorEmail(string email, string? dniExcluido)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("El correo electrónico es obligatorio.", nameof(email));
        }

        using var connection = _connectionFactory.CreateOpenConnection();
        using var command = connection.CreateCommand();
        if (string.IsNullOrWhiteSpace(dniExcluido))
        {
            command.CommandText = "SELECT COUNT(1) FROM cliente WHERE email = @correo";
        }
        else
        {
            command.CommandText = "SELECT COUNT(1) FROM cliente WHERE email = @correo AND dni_cliente <> @dniExcluido";
            command.Parameters.Add(new SqlParameter("@dniExcluido", SqlDbType.VarChar, 8) { Value = dniExcluido });
        }

        command.Parameters.Add(new SqlParameter("@correo", SqlDbType.VarChar, 100) { Value = email });

        var resultado = command.ExecuteScalar();
        return Convert.ToInt32(resultado) > 0;
    }
}
