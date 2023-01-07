namespace Xamarin.Data.DAL;

public interface ISqlitePlatform
{
    SQLiteConnection GetConnection(string dbName);

    void OnDataChanged(object sender);

    string GetPath(string dbName);

    event EventHandler DataChanged;
}