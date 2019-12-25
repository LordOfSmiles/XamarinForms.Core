using System.Globalization;
using SQLite;

namespace Xamarin.Data.DAL
{
    public abstract class BaseRepository
    {
        protected SQLiteConnection Connection => _parent.Db;
        
        #region Fields

        protected readonly object LockObject = new object();

        #endregion

        #region Dependencies

        private readonly BaseRepositoryContainer _parent;

        #endregion

        protected BaseRepository(BaseRepositoryContainer parent)
        {
            _parent = parent;

            parent.AddRepository(this);
        }

        #region Virtual and abstract Methods

        public abstract void CreateTables(SQLiteConnection db);

        public abstract void MigrateTables(SQLiteConnection db, int dbVersion);

        #endregion

        protected void OnDataChanged()
        {
            _parent.OnDataChanged();
        }
    }
}
