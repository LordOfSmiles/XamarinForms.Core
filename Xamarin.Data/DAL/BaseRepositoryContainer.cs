using System.Collections.Generic;
using System.Globalization;
using SQLite;

namespace Xamarin.Data.DAL
{
    public abstract class BaseRepositoryContainer
    {
        public void AddRepository(BaseRepository repository)
        {
            _repositories.Add(repository);
        }
        
        #region Fields

        private readonly List<BaseRepository> _repositories = new List<BaseRepository>();
        
        #endregion
        
        #region Dependencies

        private readonly ISqlite _sqLite;
       
        
        #endregion
        
        #region Constructor

        protected BaseRepositoryContainer(ISqlite sqLite)
        {
            _sqLite = sqLite;
        }

        #endregion

        public SQLiteConnection Db
        {
            get
            {
                if (_db == null)
                {
                    _db = _sqLite.GetConnection();
                    var dbVersion = GetDbVersion(_db);

                    var needCreateTable = dbVersion == 0 || dbVersion < CurrentDbVersion;
                    var needMigrateTable = dbVersion < CurrentDbVersion;

                    if (needCreateTable)
                    {
                        foreach (var repository in _repositories)
                        {
                            repository.CreateTables(_db);

                            if (needMigrateTable)
                            {
                                repository.MigrateTables(_db, CurrentDbVersion);
                            }
                        }

                        SetDbVersion(_db, CurrentDbVersion);
                    }
                }

                return _db;
            }
        }
        private SQLiteConnection _db;
        
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
        
        protected abstract int CurrentDbVersion { get;}

        public void OnDataChanged() => _sqLite.OnDataChanged();
    }
}