using System.Globalization;
using SQLite;

namespace Xamarin.Data.DAL
{
   public abstract class BaseDbContext
    {
        #region Fields

        protected readonly object LockObject = new object();

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
                    _connection = _sqLitePlatform.GetConnection();
                    var oldVersion = GetDbVersion(_connection);
                    
                    CreateTables(_connection);
                    MigrateData(_connection, oldVersion, DbVersion);

                    if (oldVersion != DbVersion)
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

        /// <summary>
        /// миграция "на лету". Например, изменеие схемы
        /// </summary>
        /// <param name="db"></param>
        protected abstract void CreateTables(SQLiteConnection db);

        /// <summary>
        /// миграция данных
        /// </summary>
        /// <param name="db"></param>
        /// <param name="oldVersion"></param>
        /// <param name="newVersion"></param>
        protected abstract void MigrateData(SQLiteConnection db, int oldVersion, int newVersion);

        #endregion
        
        protected static void SetDbVersion(SQLiteConnection db, int version)
        {
            db.Execute($"PRAGMA user_version={version.ToString(CultureInfo.InvariantCulture)}");
        }

        protected static int GetDbVersionAfterUpdate(SQLiteConnection db, int version)
        {
            SetDbVersion(db, version);
            
            return GetDbVersion(db);
        }

        protected static int GetDbVersion(SQLiteConnection db)
        {
            return db.ExecuteScalar<int>("PRAGMA user_version");
        }

        public void OnDataChanged() => _sqLitePlatform.OnDataChanged();
    }
}
