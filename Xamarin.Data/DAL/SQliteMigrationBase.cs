namespace Xamarin.Data.DAL;

public abstract class SQliteMigrationBase
{
    public abstract int DbVersion { get; }

    public abstract void Execute(SQLiteConnection db, int oldVersion, int newVersion);
}