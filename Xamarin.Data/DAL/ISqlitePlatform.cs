namespace Xamarin.Data.DAL
{
    public interface ISqlitePlatform
    {
        SQLiteConnection GetConnection();
        bool IsFileExist();
        void OnDataChanged();

        event EventHandler DataChanged;
    }
}
