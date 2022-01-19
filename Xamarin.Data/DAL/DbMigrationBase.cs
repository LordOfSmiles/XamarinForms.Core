using System;
using System.Linq;
using SQLite;

namespace Xamarin.Data.DAL
{
    public abstract class DbMigrationBase
    {
        public abstract int DbVersion { get; }

        public virtual int[] VersionsForDataModifications { get; } = Array.Empty<int>();

        public void Execute(SQLiteConnection db, int currentDbVersion, int newVersion)
        {
            if (currentDbVersion < DbVersion)
            {
                if (VersionsForDataModifications.Any() && VersionsForDataModifications.Contains(currentDbVersion))
                {
                    ModifyOldData(db, currentDbVersion);
                }

                AddNewData(db, currentDbVersion);
            }
        }

        public abstract void AddNewData(SQLiteConnection db, int currentDbVersion);

        public virtual void ModifyOldData(SQLiteConnection db, int currentDbVersion)
        {
        }
    }
}