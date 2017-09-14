using SQLite;

namespace XamarinForms.Data.DAL
{
    public interface ISqlite
    {
        SQLiteAsyncConnection GetAsyncConnection();
        SQLiteConnection GetConnection();

        bool IsFileExist();
    }
}
