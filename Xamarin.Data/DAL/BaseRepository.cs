using System.Globalization;
using SQLite;

namespace Xamarin.Data.DAL
{
    public abstract class BaseRepository
    {
        #region Protected And Public Methods

        protected void InitRepository()
        {
            var db = _sqLite.GetConnection();
            
            CreateDb(db);

            if (CurrentDbVersion > 0)
            {
                var dbVersion = GetDbVersion(db);

                if (dbVersion != CurrentDbVersion)
                {
                    MigrateTables(db, dbVersion);

                    SetDbVersion(db, CurrentDbVersion);
                }
            }
        }

        #endregion

        #region Fields

        protected readonly object LockObject = new object();

        #endregion

        #region Dependencies

        private readonly ISqlite _sqLite;

        #endregion

        protected BaseRepository(ISqlite sqlite)
        {
            _sqLite = sqlite;
        }

        #region Properties

        protected abstract int CurrentDbVersion { get; }

        #endregion

        #region Virtual and abstract Methods

        protected abstract void CreateTables(SQLiteConnection db);

        protected abstract void MigrateTables(SQLiteConnection db, int dbVersion);

        #endregion

        #region Private Methdos
        
        private void CreateDb(SQLiteConnection db)
        {
            CreateTables(db);

            var dbVersion = GetDbVersion(db);

            if (dbVersion == 0)
            {
                SetDbVersion(db, dbVersion);
            }
        }

        protected static void SetDbVersion(SQLiteConnection db, int version)
        {
            db.Execute($"PRAGMA user_version={version.ToString(CultureInfo.InvariantCulture)}");
        }

        protected static int GetDbVersionAfterUpdate(SQLiteConnection db, int version)
        {
            SetDbVersion(db, version);
            
            return GetDbVersion(db);
        }

        private static int GetDbVersion(SQLiteConnection db)
        {
            return db.ExecuteScalar<int>("PRAGMA user_version");
        }

        protected SQLiteConnection GetConnection() => _sqLite.GetConnection();

        protected void OnDataChanged() => _sqLite.OnDataChanged();

        #endregion
    }
}
