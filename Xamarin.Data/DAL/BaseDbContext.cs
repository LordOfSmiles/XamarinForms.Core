using System.Globalization;
using System.Linq;

namespace Xamarin.Data.DAL;

public abstract class BaseDbContext
{
    #region Public Methods

    public void CloseConnection()
    {
        if (_connection != null)
        {
            _connection.Dispose();
            _connection = null;
        }
    }
    
    #endregion
    
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
                _connection = _sqLitePlatform.GetConnection(DbFileName ?? "localDb");
                var currentDbVersion = GetDbVersion(_connection);

                CreateTables(_connection);

                try
                {
                    ExecuteMigrations(_connection, currentDbVersion, DbVersion);
                }
                catch (Exception ex)
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

    public string DbFileName { get; set; }
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

        for (var i = currentDbVersion + 1; i <= newDbVersion; i++)
        {
            var existMigration = Migrations.FirstOrDefault(x => x.DbVersion == i);
            existMigration?.Execute(db, currentDbVersion, newDbVersion);
        }
    }

    #endregion

    public void ResetConnection()
    {
        _connection = null;
    }

    protected static void SetDbVersion(SQLiteConnection db, int version)
    {
        db.Execute($"PRAGMA user_version={version.ToString(CultureInfo.InvariantCulture)}");
    }

    protected static int GetDbVersion(SQLiteConnection db)
    {
        return db.ExecuteScalar<int>("PRAGMA user_version");
    }

    public void OnDataChanged(object sender) => _sqLitePlatform.OnDataChanged(sender);
}