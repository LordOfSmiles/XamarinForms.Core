using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SQLite;

namespace Xamarin.Data.DAL
{
    public abstract class BaseRepositoryContainer
    {
        public T GetRepository<T>()
            where T : BaseRepository
        {
            if (Repositories.ContainsKey(typeof(T)))
            {
                return Repositories[typeof(T)] as T;
            }
            else
            {
                return default(T);
            }
        }

        #region Fields

        protected readonly Dictionary<Type, BaseRepository> Repositories = new Dictionary<Type, BaseRepository>();
        
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

        public SQLiteConnection Connection
        {
            get
            {
                if (_db == null)
                {
                    _db = _sqLite.GetConnection();
                    var dbVersion = GetDbVersion(_db);

                    var currentVersion = 0;
                    if (Repositories.Any())
                        currentVersion = Repositories.Sum(x => x.Value.CurrentVersion);

                    var needCreateTable = dbVersion == 0 || dbVersion < currentVersion;
                    var needMigrateTable = dbVersion < currentVersion;

                    if (needCreateTable)
                    {
                        foreach (var repository in Repositories)
                        {
                            repository.Value.CreateTables(_db);

                            if (needMigrateTable)
                            {
                                repository.Value.MigrateTables(_db, repository.Value.CurrentVersion);
                            }
                        }

                        SetDbVersion(_db, currentVersion);
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

        public void OnDataChanged() => _sqLite.OnDataChanged();
    }
}