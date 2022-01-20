using System;
using System.Linq;
using SQLite;

namespace Xamarin.Data.DAL
{
    public abstract class DbMigrationBase
    {
        public abstract int DbVersion { get; }

        protected abstract int[] VersionsForDataModifications { get; }

        public void Execute(SQLiteConnection db, int currentDbVersion, int newVersion)
        {
            // if (newVersion != DbVersion)
            //     return;

            if (currentDbVersion < DbVersion)
            {
                if (VersionsForDataModifications!=null && VersionsForDataModifications.Contains(currentDbVersion))
                {
                    ModifyOldData(db, currentDbVersion);
                }

                AddNewData(db, currentDbVersion);
            }
        }

        protected abstract void AddNewData(SQLiteConnection db, int currentDbVersion);

        protected virtual void ModifyOldData(SQLiteConnection db, int currentDbVersion)
        {
        }
    }
}