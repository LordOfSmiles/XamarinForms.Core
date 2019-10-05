﻿using System.Globalization;
using System.Threading.Tasks;
using SQLite;

namespace Xamarin.Data.Standard.DAL
{
    public abstract class BaseRepository
    {
        #region Fields

        protected readonly object LockObject = new object();

        #endregion

        #region Dependencies

        protected readonly ISqlite SqLite;

        #endregion

        protected BaseRepository(ISqlite sqlite)
        {
            SqLite = sqlite;
        }

        #region Properties

        protected abstract int CurrentDbVersion { get; }

        private bool IsInit { get; set; }

        #endregion

        #region Virtual and abstract Methods

        protected abstract void CreateTables(SQLiteConnection db);

        protected abstract void MigrateTables(SQLiteConnection db, int dbVersion);

        #endregion

        #region Private Methdos

        private void InitRepository(SQLiteConnection db)
        {
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

        private void CreateDb(SQLiteConnection db)
        {
            CreateTables(db);

            var dbVersion = GetDbVersion(db);

            if (dbVersion == 0)
                SetDbVersion(db, dbVersion);
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

        protected SQLiteConnection GetConnection()
        {
            var db = SqLite.GetConnection();

            if (!IsInit)
            {
                InitRepository(db);
                IsInit = true;
            }

            return db;
        }

        #endregion
    }
}
