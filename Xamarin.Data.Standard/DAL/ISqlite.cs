using SQLite;

namespace Xamarin.Data.Standard.DAL
{
    public interface ISqlite
    {
        SQLiteConnection GetConnection();

        bool IsFileExist();
    }
}
