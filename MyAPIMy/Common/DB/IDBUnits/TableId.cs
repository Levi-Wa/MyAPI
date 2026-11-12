namespace MyAPP.Common.DB.IDBUnits;

public enum TableId
{
    UnitsMeasureTable,
    CounterpartiesTable,
    MaterialsTable,
    DocumentsTable,
    MaterialMovementsTable
}

public static class TableOrder
{
    public static IReadOnlyList<TableId> Creation { get; } =
    [
        TableId.UnitsMeasureTable,
        TableId.CounterpartiesTable,
        TableId.MaterialsTable,
        TableId.DocumentsTable,
        TableId.MaterialMovementsTable
    ];
}
