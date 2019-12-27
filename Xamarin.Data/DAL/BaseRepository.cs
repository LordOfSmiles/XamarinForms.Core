using System.Globalization;
using SQLite;

namespace Xamarin.Data.DAL
{
    public abstract class BaseRepository
    {
        protected SQLiteConnection Connection => _container.Connection;
        
        #region Fields

        protected readonly object LockObject = new object();

        #endregion

        #region Dependencies

        private readonly BaseRepositoryContainer _container;

        #endregion

        protected BaseRepository(BaseRepositoryContainer container)
        {
            _container = container;
        }

        #region Virtual and abstract Methods
        
        public abstract int CurrentVersion { get; }

        public abstract void CreateTables(SQLiteConnection db);

        public abstract void MigrateTables(SQLiteConnection db, int dbVersion);

        #endregion

        protected void OnDataChanged()
        {
            _container.OnDataChanged();
        }
    }
}
