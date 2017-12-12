using SQLite;

namespace Xamarin.Data.Standard.DAL
{
    public interface ISqlite
    {
        SQLiteAsyncConnection GetAsyncConnection();
        SQLiteConnection GetConnection();

        bool IsFileExist();
    }
}
