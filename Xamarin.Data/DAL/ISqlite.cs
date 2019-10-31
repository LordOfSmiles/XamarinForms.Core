using System;
using SQLite;

namespace Xamarin.Data.DAL
{
    public interface ISqlite
    {
        SQLiteConnection GetConnection();

        bool IsFileExist();

        void OnDataChanged();

        event EventHandler DataChanged;
    }
}
