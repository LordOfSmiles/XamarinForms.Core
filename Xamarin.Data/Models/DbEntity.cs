using SQLite;

namespace Xamarin.Data.Models
{
    public abstract class DbEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}