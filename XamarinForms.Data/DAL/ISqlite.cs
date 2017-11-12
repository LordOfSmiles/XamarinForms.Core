using SQLite;

namespace Xamarin.Data.DAL
{
    public interface ISqlite
    {
        SQLiteAsyncConnection GetAsyncConnection();
        SQLiteConnection GetConnection();

        bool IsFileExist();
    }
}
