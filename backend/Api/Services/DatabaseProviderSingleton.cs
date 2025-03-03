using Infrastructure.Interfaces;
using Infrastructure.Values;

namespace Api.Services;

public class DatabaseProviderSingleton : IDatabaseProviderSingleton
{
    public DatabaseProviderSingletonValue Value { get; }
    public bool IsSQLite { get; }
    public bool IsMSSQL { get; }
    public bool IsMySQL { get; }

    public DatabaseProviderSingleton(DatabaseProviderSingletonValue value)
    {
        Value = value;
        IsSQLite = value == DatabaseProviderSingletonValue.Sqlite;
        IsMSSQL = value == DatabaseProviderSingletonValue.SqlServer;
        IsMySQL = value == DatabaseProviderSingletonValue.MySQL;

        if ((IsSQLite || IsMSSQL || IsMySQL) is false)
        {
            throw new Exception($"\"{value}\" is not a valid database provider name.");
        }
    }
}