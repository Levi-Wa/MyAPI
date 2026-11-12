using Microsoft.Data.Sqlite;

namespace MyAPP.Drivers.SQLite.Tables;

/// <summary>
/// Интерфейс инициализатора базы данных для проверки и досоздания таблиц.
/// </summary>
public interface IDatabaseInitializer
{
    /// <summary>
    /// Проверяет наличие таблицы в базе данных.
    /// </summary>
    bool TableExists(SqliteConnection connection, string tableName);

    /// <summary>
    /// Проверяет жизненно важные таблицы БД на их наличие и досоздаёт отсутствующие.
    /// </summary>
    void EnsureVitalTablesExist(SqliteConnection connection);

    /// <summary>
    /// Метод инициализации базы данных.
    /// </summary>
    void Initialize(SqliteConnection connection);
}
