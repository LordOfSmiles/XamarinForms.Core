using SQLite;

namespace NewXamarinForms.Data.DAL
{
    public interface ISqlite
    {
        SQLiteAsyncConnection GetAsyncConnection();
        SQLiteConnection GetConnection();

        bool IsFileExist();
    }
}
