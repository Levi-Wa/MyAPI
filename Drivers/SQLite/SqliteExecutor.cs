using Microsoft.Data.Sqlite;

namespace MyAPP.Drivers.SQLite;

/// <summary>
/// Выполняет SQL-запросы к SQLite для сущностей типа <typeparamref name="T"/>.
/// Все операции последовательно синхронизируются общим Mutex.
/// </summary>
public class SqliteExecutor<T>
{
    private readonly string _connectionString;
    private readonly Mutex _mutex;
    private readonly Action? _beginOperation;
    private readonly Action? _endOperation;

    public SqliteExecutor(string connectionString, Mutex mutex)
        : this(connectionString, mutex, null, null)
    {
    }

    internal SqliteExecutor(
        string connectionString,
        Mutex mutex,
        Action? beginOperation,
        Action? endOperation)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Строка подключения SQLite не может быть пустой.", nameof(connectionString));

        _connectionString = connectionString;
        _mutex = mutex ?? throw new ArgumentNullException(nameof(mutex));
        _beginOperation = beginOperation;
        _endOperation = endOperation;
    }

    public List<T> ExecuteReadList(
        string sql,
        Func<SqliteDataReader, T> mapper,
        List<SqliteParameter>? parameters = null)
    {
        ArgumentNullException.ThrowIfNull(mapper);

        var result = new List<T>();

        _beginOperation?.Invoke();
        try
        {
            _mutex.WaitOne();
            try
            {
                using var connection = OpenConnection();
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
                _mutex.ReleaseMutex();
            }
        }
        finally
        {
            _endOperation?.Invoke();
        }
    }

    public T? ExecuteReadSingle(
        string sql,
        Func<SqliteDataReader, T> mapper,
        List<SqliteParameter>? parameters = null)
    {
        ArgumentNullException.ThrowIfNull(mapper);

        _beginOperation?.Invoke();
        try
        {
            _mutex.WaitOne();
            try
            {
                using var connection = OpenConnection();
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
                _mutex.ReleaseMutex();
            }
        }
        finally
        {
            _endOperation?.Invoke();
        }
    }

    public int ExecuteWrite(string sql, List<SqliteParameter>? parameters = null)
    {
        _beginOperation?.Invoke();
        try
        {
            _mutex.WaitOne();
            try
            {
                using var connection = OpenConnection();
                using var command = CreateCommand(connection, sql, parameters);
                return command.ExecuteNonQuery();
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }
        finally
        {
            _endOperation?.Invoke();
        }
    }

    public TResult ExecuteScalar<TResult>(
        string sql,
        Func<object?, TResult> converter,
        List<SqliteParameter>? parameters = null)
    {
        ArgumentNullException.ThrowIfNull(converter);

        _beginOperation?.Invoke();
        try
        {
            _mutex.WaitOne();
            try
            {
                using var connection = OpenConnection();
                using var command = CreateCommand(connection, sql, parameters);
                return converter(command.ExecuteScalar());
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }
        finally
        {
            _endOperation?.Invoke();
        }
    }

    private SqliteConnection OpenConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        try
        {
            connection.Open();

            using var pragma = connection.CreateCommand();
            pragma.CommandText = "PRAGMA foreign_keys = ON; PRAGMA busy_timeout = 5000;";
            pragma.ExecuteNonQuery();

            return connection;
        }
        catch
        {
            connection.Dispose();
            throw;
        }
    }

    private static SqliteCommand CreateCommand(
        SqliteConnection connection,
        string sql,
        List<SqliteParameter>? parameters)
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
