using Microsoft.Data.Sqlite;

namespace MyAPP.Drivers.SQLite.Tables;

/// <summary>
/// Класс проверки и инициализации жизненно важных таблиц базы данных SQLite.
/// Не использует списки и одномерные массивы: порядок и логика проверки строго заданы внутри функции.
/// </summary>
public class DatabaseInitializer : IDatabaseInitializer
{
    // SQL-запросы создания жизненно важных таблиц в виде строковых констант
    private const string SqlUnitsMeasure = """
        CREATE TABLE IF NOT EXISTS units_measure (
            unit_id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            symbol TEXT NOT NULL
        );
        """;

    private const string SqlCounterparties = """
        CREATE TABLE IF NOT EXISTS counterparties (
            counterparty_id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            type TEXT NOT NULL
        );
        """;

    private const string SqlMaterials = """
        CREATE TABLE IF NOT EXISTS materials (
            catalog_id INTEGER PRIMARY KEY AUTOINCREMENT,
            catalog_number TEXT NOT NULL,
            name TEXT NOT NULL,
            grade TEXT NOT NULL,
            profile_size TEXT NOT NULL,
            unit_id INTEGER NOT NULL,
            stock_norm NUMERIC NOT NULL DEFAULT 0
        );
        """;

    private const string SqlDocuments = """
        CREATE TABLE IF NOT EXISTS documents (
            document_id INTEGER PRIMARY KEY AUTOINCREMENT,
            document_number TEXT NOT NULL,
            record_date TEXT NOT NULL DEFAULT (datetime('now')),
            counterparty_id INTEGER NOT NULL,
            document_type INTEGER NOT NULL
        );
        """;

    private const string SqlMaterialMovements = """
        CREATE TABLE IF NOT EXISTS material_movements (
            record_id INTEGER PRIMARY KEY AUTOINCREMENT,
            document_id INTEGER NOT NULL,
            catalog_id INTEGER NOT NULL,
            document_price NUMERIC NOT NULL DEFAULT 0,
            income NUMERIC NOT NULL DEFAULT 0,
            expense NUMERIC NOT NULL DEFAULT 0
        );
        """;

    /// <summary>
    /// Проверяет наличие таблицы в базе данных SQLite через системную таблицу sqlite_master.
    /// </summary>
    public bool TableExists(SqliteConnection connection, string tableName)
    {
        const string query = "SELECT COUNT(1) FROM sqlite_master WHERE type = 'table' AND name = @name;";
        using var cmd = connection.CreateCommand();
        cmd.CommandText = query;
        cmd.Parameters.AddWithValue("@name", tableName);
        var result = Convert.ToInt32(cmd.ExecuteScalar());
        return result > 0;
    }

    /// <summary>
    /// Выполняет создание таблицы по SQL-запросу.
    /// </summary>
    private static void ExecuteCreateTable(SqliteConnection connection, string sql, string tableName)
    {
        try
        {
            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Не удалось создать таблицу {tableName}", ex);
        }
    }

    /// <summary>
    /// Функция, которая проверяет жизненно важные таблицы БД на их наличие в строгом порядке зависимостей.
    /// В случае если таблица не найдена, она её досоздаёт SQL-запросом.
    /// </summary>
    public void EnsureVitalTablesExist(SqliteConnection connection)
    {
        if (connection.State != System.Data.ConnectionState.Open)
        {
            connection.Open();
        }

        using (var pragmaCmd = connection.CreateCommand())
        {
            pragmaCmd.CommandText = "PRAGMA foreign_keys = ON;";
            pragmaCmd.ExecuteNonQuery();
        }

        using var transaction = connection.BeginTransaction();
        try
        {
            // 1. units_measure - справочник единиц измерения (не зависит от других таблиц)
            if (!TableExists(connection, "units_measure"))
            {
                ExecuteCreateTable(connection, SqlUnitsMeasure, "units_measure");
            }

            // 2. counterparties - справочник контрагентов (не зависит от других таблиц)
            if (!TableExists(connection, "counterparties"))
            {
                ExecuteCreateTable(connection, SqlCounterparties, "counterparties");
            }

            // 3. materials - материалы (ссылаются на units_measure)
            if (!TableExists(connection, "materials"))
            {
                ExecuteCreateTable(connection, SqlMaterials, "materials");
            }

            // 4. documents - документы (ссылаются на counterparties)
            if (!TableExists(connection, "documents"))
            {
                ExecuteCreateTable(connection, SqlDocuments, "documents");
            }

            // 5. material_movements - движения материалов (ссылаются на documents и materials)
            if (!TableExists(connection, "material_movements"))
            {
                ExecuteCreateTable(connection, SqlMaterialMovements, "material_movements");
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void Initialize(SqliteConnection connection)
    {
        EnsureVitalTablesExist(connection);
    }
}
