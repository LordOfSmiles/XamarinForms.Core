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

        protected abstract Task UpdateTablesAsync(SQLiteAsyncConnection db, int dbVersion);

        #endregion

        #region Private Methdos

        private async Task InitRepositoryAsync(SQLiteAsyncConnection db)
        {
            if (_sqLite.IsFileExist())
            {
                await UpdateDbAsync(db).ConfigureAwait(false);
            }
            else
            {
                await CreateDbAsync(db).ConfigureAwait(false);
            }
        }

        private async Task CreateDbAsync(SQLiteAsyncConnection db)
        {
            await CreateTablesAsync(db).ConfigureAwait(false);

            await SetDbVersionAsync(db, CurrentDbVersion).ConfigureAwait(false);
        }

        private async Task UpdateDbAsync(SQLiteAsyncConnection db)
        {
            var dbVersion = await GetDbVersionAsync(db).ConfigureAwait(false);

            if (dbVersion != CurrentDbVersion)
            {
                await UpdateTablesAsync(db, dbVersion).ConfigureAwait(false);
            }
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

        protected SQLiteConnection GetConnection()
        {
            var db = _sqLite.GetConnection();

            if (!IsInit)
            {
                //нужно доделать
                IsInit = true;
            }

            return db;
        }

        protected async Task<SQLiteAsyncConnection> GetAsyncConnection()
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
