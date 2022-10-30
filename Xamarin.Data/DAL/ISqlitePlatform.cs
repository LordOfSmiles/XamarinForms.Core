namespace Xamarin.Data.DAL;

public interface ISqlitePlatform
{
    SQLiteConnection GetConnection(string dbName);
    bool IsFileExist(string dbName);
    void OnDataChanged();

    event EventHandler DataChanged;
}