namespace MyAPP.Common.DB.IDBUnits;

// Главный интерфейс контекста базы данных
public interface IDatabase : IDisposable
{
    ICounterparties Counterparties { get; }
    IDocuments Documents { get; }
    IMaterialMovements MaterialMovements { get; }
    IMaterials Materials { get; }
    IUnitsMeasure UnitsMeasure { get; }

    // Проверка доступности подключения к базе данных
    bool TestConnection();

    // Инициализация структуры таблиц базы данных
    void Initialize();

    // Освобождение ресурсов и ожидание завершения активных операций
    void Release();
}
