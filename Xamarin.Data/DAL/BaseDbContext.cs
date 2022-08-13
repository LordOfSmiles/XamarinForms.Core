using System.Globalization;
using System.Linq;

namespace Xamarin.Data.DAL;

public abstract class BaseDbContext
{
    #region Dependencies

    private readonly ISqlitePlatform _sqLitePlatform;

    #endregion

    protected BaseDbContext(ISqlitePlatform sqLitePlatform)
    {
        _sqLitePlatform = sqLitePlatform;
    }

    public SQLiteConnection Connection
    {
        get
        {
            if (_connection == null)
            {
                _connection = _sqLitePlatform.GetConnection();
                var currentDbVersion = GetDbVersion(_connection);

                CreateTables(_connection);

                try
                {
                    ExecuteMigrations(_connection, currentDbVersion, DbVersion);
                }
                catch (Exception e)
                {
                   //
                }

                if (currentDbVersion != DbVersion)
                {
                    SetDbVersion(_connection, DbVersion);
                }
            }

            return _connection;
        }
    }
    private SQLiteConnection _connection;

    #region Virtual and abstract Methods

    protected abstract int DbVersion { get; }
    protected DbMigrationBase[] Migrations { get; set; } = Array.Empty<DbMigrationBase>();

    /// <summary>
    /// миграция "на лету". Например, изменеие схемы
    /// </summary>
    /// <param name="db"></param>
    protected abstract void CreateTables(SQLiteConnection db);

    /// <summary>
    /// миграция данных
    /// </summary>
    /// <param name="db"></param>
    /// <param name="currentDbVersion"></param>
    /// <param name="newDbVersion"></param>
    private void ExecuteMigrations(SQLiteConnection db, int currentDbVersion, int newDbVersion)
    {
        if (Migrations == null)
            return;

        if (currentDbVersion >= newDbVersion)
            return;

        // var orderedMigrations = Migrations.OrderBy(x => x.DbVersion);
        // foreach (var migration in orderedMigrations)
        // {
        //     if (migration.DbVersion <= newVersion)
        //     {
        //         migration.Execute(db, oldDbVersion, newVersion);
        //     }
        // }

        for (var i = currentDbVersion; i < newDbVersion; i++)
        {
            var existMigration = Migrations.FirstOrDefault(x => x.DbVersion == i);
            existMigration?.Execute(db, currentDbVersion, newDbVersion);
        }
    }

    #endregion

    protected static void SetDbVersion(SQLiteConnection db, int version)
    {
        db.Execute($"PRAGMA user_version={version.ToString(CultureInfo.InvariantCulture)}");
    }

    protected static int GetDbVersion(SQLiteConnection db)
    {
        return db.ExecuteScalar<int>("PRAGMA user_version");
    }

    public void OnDataChanged() => _sqLitePlatform.OnDataChanged();
}