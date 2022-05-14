namespace Xamarin.Data.DAL
{
    public abstract class DbMigrationBase
    {
        public abstract int DbVersion { get; }

        public void Execute(SQLiteConnection db, int currentDbVersion, int newVersion)
        {
            if (currentDbVersion < DbVersion)
            {
                ModifyOldData(db, currentDbVersion);
                AddNewData(db, currentDbVersion);
            }
        }

        protected abstract void AddNewData(SQLiteConnection db, int currentDbVersion);

        protected virtual void ModifyOldData(SQLiteConnection db, int currentDbVersion)
        {
        }
    }
}