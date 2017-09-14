using System.Globalization;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace XamarinForms.Data.DAL
{
    public abstract class BaseRepository
    {
        #region Dependencies

        private readonly ISqlite _sqLite;

        #endregion

        protected BaseRepository()
        {
            _sqLite = DependencyService.Get<ISqlite>();
        }

        #region Properties

        protected abstract int CurrentDbVersion { get; }

        private static bool IsInit { get; set; }

        #endregion

        #region Virtual and abstract Methods

        protected abstract Task CreateTablesAsync(SQLiteAsyncConnection db);

        protected abstract Task MigrateTablesAsync(SQLiteAsyncConnection db, int dbVersion);

        #endregion

        #region Private Methdos

        private async Task InitRepositoryAsync(SQLiteAsyncConnection db)
        {
            await CreateDbAsync(db).ConfigureAwait(false);

            if (CurrentDbVersion > 1)
            {
                var dbVersion = await GetDbVersionAsync(db).ConfigureAwait(false);

                if (dbVersion != CurrentDbVersion)
                {
                    await MigrateTablesAsync(db, dbVersion).ConfigureAwait(false);
                }
            }
        }


        private async Task CreateDbAsync(SQLiteAsyncConnection db)
        {
            await CreateTablesAsync(db).ConfigureAwait(false);

            await SetDbVersionAsync(db, CurrentDbVersion).ConfigureAwait(false);
        }

        protected static Task SetDbVersionAsync(SQLiteAsyncConnection db, int version)
        {
            return db.ExecuteAsync($"PRAGMA user_version={version.ToString(CultureInfo.InvariantCulture)}");
        }

        protected static async Task<int> GetDbVersionAfterUpdateAsync(SQLiteAsyncConnection db, int version)
        {
            await SetDbVersionAsync(db, version).ConfigureAwait(false);
            return await GetDbVersionAsync(db).ConfigureAwait(false);
        }

        private static Task<int> GetDbVersionAsync(SQLiteAsyncConnection db)
        {
            return db.ExecuteScalarAsync<int>("PRAGMA user_version");
        }

        protected async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            var db = _sqLite.GetAsyncConnection();

            if (!IsInit)
            {
                await InitRepositoryAsync(db).ConfigureAwait(false);
                IsInit = true;
            }

            return db;
        }

        #endregion
    }
}
