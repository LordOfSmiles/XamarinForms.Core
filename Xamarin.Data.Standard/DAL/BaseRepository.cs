using System.Globalization;
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

        private readonly ISqlite _sqLite;

        #endregion

        protected BaseRepository(ISqlite sqlite)
        {
            _sqLite = sqlite;
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

        //private async Task InitRepositoryAsync(SQLiteAsyncConnection db)
        //{
        //    await CreateDbAsync(db).ConfigureAwait(false);

        //    if (CurrentDbVersion > 0)
        //    {
        //        var dbVersion = await GetDbVersionAsync(db).ConfigureAwait(false);

        //        if (dbVersion != CurrentDbVersion)
        //        {
        //            await MigrateTablesAsync(db, dbVersion).ConfigureAwait(false);

        //            await SetDbVersionAsync(db, CurrentDbVersion).ConfigureAwait(false);
        //        }
        //    }
        //}

        //private async Task CreateDbAsync(SQLiteAsyncConnection db)
        //{
        //    await CreateTablesAsync(db).ConfigureAwait(false);

        //    var dbVersion = await GetDbVersionAsync(db).ConfigureAwait(false);

        //    if (dbVersion == 0)
        //        await SetDbVersionAsync(db, dbVersion).ConfigureAwait(false);
        //}

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

        //protected static Task SetDbVersionAsync(SQLiteAsyncConnection db, int version)
        //{
        //    return db.ExecuteAsync($"PRAGMA user_version={version.ToString(CultureInfo.InvariantCulture)}");
        //}

        protected static int GetDbVersionAfterUpdate(SQLiteConnection db, int version)
        {
            SetDbVersion(db, version);
            return GetDbVersion(db);
        }

        //protected static async Task<int> GetDbVersionAfterUpdateAsync(SQLiteAsyncConnection db, int version)
        //{
        //    await SetDbVersionAsync(db, version).ConfigureAwait(false);
        //    return await GetDbVersionAsync(db).ConfigureAwait(false);
        //}

        private static int GetDbVersion(SQLiteConnection db)
        {
            return db.ExecuteScalar<int>("PRAGMA user_version");
        }

        protected SQLiteConnection GetConnection()
        {
            var db = _sqLite.GetConnection();

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
