using MySqlConnector;

namespace MyAPP.Drivers.MariaDB;

/// <summary>
/// Выполняет SQL-запросы к MariaDB для сущностей типа <typeparamref name="T"/>.
/// Для каждой операции клонирует эталонное соединение (без Mutex).
/// </summary>
public class MariaDbExecutor<T>
{
    private readonly MySqlConnection _prototypeConnection;
    private readonly Action? _beginOperation;
    private readonly Action? _endOperation;

    public MariaDbExecutor(MySqlConnection prototypeConnection)
        : this(prototypeConnection, null, null)
    {
    }

    internal MariaDbExecutor(
        MySqlConnection prototypeConnection,
        Action? beginOperation,
        Action? endOperation)
    {
        _prototypeConnection = prototypeConnection ?? throw new ArgumentNullException(nameof(prototypeConnection));
        _beginOperation = beginOperation;
        _endOperation = endOperation;
    }

    public List<T> ExecuteReadList(
        string sql,
        Func<MySqlDataReader, T> mapper,
        List<MySqlParameter>? parameters = null)
    {
        ArgumentNullException.ThrowIfNull(mapper);

        var result = new List<T>();

        _beginOperation?.Invoke();
        try
        {
            using var connection = OpenClonedConnection();
            using var command = CreateCommand(connection, sql, parameters);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                result.Add(mapper(reader));
            }

            return result;
        }
        finally
        {
            _endOperation?.Invoke();
        }
    }

    public T? ExecuteReadSingle(
        string sql,
        Func<MySqlDataReader, T> mapper,
        List<MySqlParameter>? parameters = null)
    {
        ArgumentNullException.ThrowIfNull(mapper);

        _beginOperation?.Invoke();
        try
        {
            using var connection = OpenClonedConnection();
            using var command = CreateCommand(connection, sql, parameters);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return mapper(reader);
            }

            return default;
        }
        finally
        {
            _endOperation?.Invoke();
        }
    }

    public int ExecuteWrite(string sql, List<MySqlParameter>? parameters = null)
    {
        _beginOperation?.Invoke();
        try
        {
            using var connection = OpenClonedConnection();
            using var command = CreateCommand(connection, sql, parameters);
            return command.ExecuteNonQuery();
        }
        finally
        {
            _endOperation?.Invoke();
        }
    }

    public TResult ExecuteScalar<TResult>(
        string sql,
        Func<object?, TResult> converter,
        List<MySqlParameter>? parameters = null)
    {
        ArgumentNullException.ThrowIfNull(converter);

        _beginOperation?.Invoke();
        try
        {
            using var connection = OpenClonedConnection();
            using var command = CreateCommand(connection, sql, parameters);
            return converter(command.ExecuteScalar());
        }
        finally
        {
            _endOperation?.Invoke();
        }
    }

    private MySqlConnection OpenClonedConnection()
    {
        var connection = _prototypeConnection.Clone();
        try
        {
            connection.Open();
            return connection;
        }
        catch
        {
            connection.Dispose();
            throw;
        }
    }

    private static MySqlCommand CreateCommand(
        MySqlConnection connection,
        string sql,
        List<MySqlParameter>? parameters)
    {
        var command = connection.CreateCommand();
        command.CommandText = sql;

        if (parameters != null)
        {
            foreach (var parameter in parameters)
            {
                if (parameter.Value is null)
                {
                    parameter.Value = DBNull.Value;
                }
                command.Parameters.Add(parameter);
            }
        }

        return command;
    }
}
